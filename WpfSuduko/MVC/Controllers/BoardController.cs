using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using WpfSuduko.Extensions;
using WpfSuduko.MVC.Models;

namespace WpfSuduko.MVC.Controllers
{
    sealed class BoardController
    {
        private static BoardController instance = null;
        private readonly FileController fileController;

        public static BoardController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BoardController();
                    }
                    return instance;
                }
            }
        }
        private static readonly object padlock = new object();

        private readonly Random _rand;
        public Board SudukoBoard { get; private set; }
        private Board SudukoBoardSolved;
        public BoardController()
        {
            _rand = new Random();
            fileController =new FileController();
            ResetBoard();
        }
        public bool IsSolved() 
        {
            return this.SudukoBoard.Equals(SudukoBoardSolved);
        }
        public void ResetBoard(int difficulty = Board._easy) 
        {
            SudukoBoard = GetNewBoard(difficulty);
        }
        private Board GetNewBoard(int difficulty = Board._easy)
        {
            Board newValidBoard = GenerateBoard();
            SudukoBoardSolved = new Board(newValidBoard);
            fileController.AppendBoardToFile(newValidBoard, "Completed Board: \n");
            PullRandomSpots(newValidBoard, difficulty);
            return newValidBoard;
        }
        private void PullRandomSpots(Board board, int difficulty = Board._easy) 
        {
            Queue<Square> positionList = CreateRandomizedPositionList(board);
            Square nextSquare;
            while (positionList.Count > (81 - difficulty)) 
            {
                nextSquare = positionList.Dequeue();
                nextSquare.StoredValue = Square.DefaultStoredValue;
            }
        }
        private void CreateUniquePuzzle(Board board) 
        {
            Queue<Square> positionList = CreateRandomizedPositionList(board);
            int oldStoredValue;
            Square nextSquare;
            while (positionList.Count > 50) 
            {
                nextSquare = positionList.Dequeue();
                oldStoredValue = nextSquare.StoredValue;
                nextSquare.StoredValue = Square.DefaultStoredValue;
                if (!BoardIsUnique(board))
                {
                    //Has more than one solution undo last removal
                    nextSquare.StoredValue = oldStoredValue;
                }
            }
        }

        //Checks to see if the Board has more than one solution.
        private bool BoardIsUnique(Board board)
        {
            Board boardSolved = Solve(board);

            Board secondBoardSolved = Solve(board, boardSolved.FirstLocSet.StoredValue + 1);
            // if the second solved board is incomplete then return true because there is only one solution
            return !secondBoardSolved.BoardIsComplete();
        }
        public Board SolveSudukoBT(Board boardToSolve,ref Square lastPositionChanged) 
        {
            Board solvedBoard = new Board(boardToSolve);  
            Square nextEmpty = null;
            foreach (Square s in solvedBoard.AllSquares)
            {
                if (!s.IsSet())
                {
                    nextEmpty = s;
                    nextEmpty.StoredValue = 1;
                    while (nextEmpty.StoredValue < 10) 
                    {
                        if (LocationIsValid(boardToSolve, nextEmpty.XGridPosition, nextEmpty.YGridPosition)) 
                        {
                            lastPositionChanged = Square.CopySquare(nextEmpty);
                            break;
                        }
                        nextEmpty.StoredValue += 1;
                    }
                    if (nextEmpty.StoredValue == 10) 
                    {
                        //Go back to last changed location and increment.
                    }
                }
            }
            return solvedBoard;
        }
        public Board Solve(Board boardToSolve, int backtrackingValue = 1)
        {
            Board solvedBoard = new Board(boardToSolve);
            for (int row = 0; row < Board.BoardLength; ++row)
            {
                for (int column = 0; column < Board.BoardLength; ++column)
                {
                    if (!solvedBoard.AllSquares[row, column].IsSet())
                    {
                        if (backtrackingValue > 9) backtrackingValue = 1;
                        solvedBoard.AllSquares[row, column].StoredValue = backtrackingValue;
                        //The newly set location is incorrect in the given board
                        if (!LocationIsValid(solvedBoard, row, column))
                        {
                            if (column < 9)
                            {
                                solvedBoard.AllSquares[row, column].StoredValue = Square.DefaultStoredValue;
                                column--;
                                backtrackingValue++;
                            }
                            //if (backtrackingValue == 10) return solvedBoard;
                        }
                        else
                        {
                            if(solvedBoard.FirstLocSet == null) solvedBoard.FirstLocSet = Square.CopySquare(solvedBoard.AllSquares[row, column]);
                            backtrackingValue = 1;
                        }
                    }
                }
            }
            return solvedBoard;
        }
        private bool LocationIsValid(Board board, int row, int column)
        {
            return IsValidInColumn(board, row, column) && IsValidInRow(board, row, column) && IsValidInNonet(board, row, column);
        }

        private Queue<Square> CreateRandomizedPositionList(Board board) 
        {
            List<Square> allSquaresAsList = board.AllSquares.TwoDArrayAsList();
            allSquaresAsList.Shuffle();
            Queue<Square> positionList = new Queue<Square>(allSquaresAsList);
            return positionList;
        }
        //@Unhandled ClassCastException;
        private Board GenerateBoard()
        {
            Board board = new Board();
            ArrayList fillList;
            int randLocationInList;
            for (int row = 0; row < Board.BoardLength; ++row)
            {
                //Create an ArrayList of numbers 1- 9
                fillList = GenerateFillList();
                for (int column = 0; column < Board.BoardLength; ++column)
                {

                    //Get a random location from the fillList of numbers
                    randLocationInList = _rand.Next(0, fillList.Count);
                    //Assign the appropriate data to the square
                    board.AllSquares[row, column].StoredValue = (int)fillList[randLocationInList];
                    board.AllSquares[row, column].XGridPosition = row;
                    board.AllSquares[row, column].YGridPosition = column;
                    //Remove number from the fillList to guarantee uniquness
                    fillList.RemoveAt(randLocationInList);
                }
                //Check vaidity for any row past the first row, if the row isn't valid for suduko retry setting row
                if (row <= 8 && row > 0)
                {
                    if (!RowIsValid(board, row))
                    {
                        row--;
                    }
                }
            }
            return board;
        }
        private ArrayList GenerateFillList()
        {
            ArrayList fillList = new ArrayList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            return fillList;
        }
        private bool RowIsValid(Board board, int row)
        {
            bool isValid = true;
            for (int column = 0; column < Board.BoardLength; column++)
            {
                //Check Above
                for (int prevRow = row - 1; prevRow > -1; prevRow--)
                {
                    if (board.AllSquares[prevRow, column].StoredValue == board.GetValueAtLoc(row, column)) isValid = false;
                    if (!isValid) break;
                }
                if (!isValid) break;
                isValid = IsValidInNonet(board, row, column);
                if (!isValid) break;
            }

            return isValid;
        }
        private bool IsValidInColumn(Board board, int row, int column)
        {
            int value = board.GetValueAtLoc(row, column);
            bool isValidColumn = true;
            for (int nextRow = 0; nextRow < Board.BoardLength; nextRow++)
            {
                if (nextRow != row && board.GetValueAtLoc(nextRow, column) == value) 
                {
                    isValidColumn = false;
                    break;
                }
            }
            return isValidColumn;
        }
        private bool IsValidInRow(Board board, int row, int column)
        {
            int value = board.GetValueAtLoc(row, column);
            bool isValidRow = true;
            for (int nextColumn = 0; nextColumn < Board.BoardLength; nextColumn++)
            {
                if (nextColumn != column && board.GetValueAtLoc(row, nextColumn) == value)
                {
                    isValidRow = false;
                    break;
                }
            }
            return isValidRow;
        }
        private bool IsValidInNonet(Board board, int row, int column)
        {
            int value = board.GetValueAtLoc(row, column);
            bool isValid = true;
            int rowModHelper = row % 3;
            //Check down 2 rows
            if (rowModHelper == 0)
            {
                isValid = CheckNonetColumns(board, row + 1, column, value);
                if (isValid) isValid = CheckNonetColumns(board, row + 2, column, value);
            }
            //Check up 1 row and down 1 row
            if (rowModHelper == 1)
            {
                isValid = CheckNonetColumns(board, row + 1, column, value);
                if (isValid) isValid = CheckNonetColumns(board, row - 1, column, value);
            }
            //Check up 2 rows
            if (rowModHelper == 2)
            {
                isValid = CheckNonetColumns(board, row - 2, column, value);
                if (isValid) isValid = CheckNonetColumns(board, row - 1, column, value);
            }
            return isValid;
        }
        private bool CheckNonetColumns(Board board, int row, int column, int value)
        {
            bool isValid = true;
            //Column mod 3 if result is 0 check +2 if result is 1 check +-1 if result is 2 check -2
            int columnModHelper = column % 3;
            //Check forward 2 columns
            if (columnModHelper == 0)
            {
                if (board.AllSquares[row, column + 1].StoredValue == value || board.AllSquares[row, column + 2].StoredValue == value)
                {
                    isValid = false;
                }
            }
            //Check forward one column and check back one column
            if (columnModHelper == 1)
            {
                if (board.AllSquares[row, column + 1].StoredValue == value || board.AllSquares[row, column - 1].StoredValue == value)
                {
                    isValid = false;
                }
            }
            //Check back two columns
            if (columnModHelper == 2)
            {
                if (board.AllSquares[row, column - 1].StoredValue == value || board.AllSquares[row, column - 2].StoredValue == value)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
    //Checks the entire suduko column based on the value passsed in
    //private bool CheckColumn(Board board, int row, int column, int value)
    //{
    //    bool isValid = true;
    //    int counter = Board.BoardLength - 1;
    //    while (counter >= 0)
    //    {
    //        if (counter != row && board.AllSquares[counter, column].StoredValue == value)
    //        {
    //            isValid = false;
    //            break;
    //        }
    //        counter--;
    //    }
    //    return isValid;
    //}
    //private int SolveSudukoBackTracking(Board board, int backtrackingValue, int row, int column, int numberOfSolutions)
    //{
    //    if (column == Board.BoardLength) row++;
    //    if (row == Board.BoardLength) return numberOfSolutions++;
    //    if (backtrackingValue == 9)
    //    {
    //        backtrackingValue = 1;
    //    }
    //    if (board.AllSquares[row, column].StoredValue == Square.DefaultStoredValue)
    //    {
    //        board.AllSquares[row, column].StoredValue = backtrackingValue;
    //    }
    //    if (LocationIsValid(board, row, column))
    //    {
    //        //Move Forward
    //        SolveSudukoBackTracking(board, backtrackingValue, row, column++, numberOfSolutions);
    //    }
    //    else
    //    {
    //        if (row == 8)
    //            //Move Backwards and increment backtrackingValue
    //            SolveSudukoBackTracking(board, backtrackingValue++, row, column, numberOfSolutions);
    //    }
    //    return -1;
    //}
}

