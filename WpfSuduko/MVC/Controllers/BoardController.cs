using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WpfSuduko.Extensions;
using WpfSuduko.MVC.Models;

namespace WpfSuduko.MVC.Controllers
{
    class BoardController
    {
        private Random _rand;
        public BoardController()
        {
            _rand = new Random();
        }


        public void ResetBoard(Board board)
        {
            GenerateBoard(board);
            CreatePositionList(board);
        }
        private void CreateUniquePuzzle() 
        {
            
        }
        private void CreatePositionList(Board board) 
        {
            List<Square> allSquaresAsList = board.AllSquares.TwoDArrayAsList();
            allSquaresAsList.Shuffle();
            Console.WriteLine();
        }
        //@Unhandled ClassCastException;
        private void GenerateBoard(Board board)
        {
            ArrayList fillList;
            int randLocationInList;
            for (int row = 0; row < Board.BoardLength; ++row)
            {
                fillList = GenerateFillList();
                for (int column = 0; column < Board.BoardLength; ++column)
                {
                    randLocationInList = _rand.Next(0, fillList.Count);
                    board.AllSquares[row, column].StoredValue = (int)fillList[randLocationInList];
                    board.AllSquares[row, column].DisplayedValue = (int)fillList[randLocationInList];
                    board.AllSquares[row, column].XGridPosition = row;
                    board.AllSquares[row, column].YGridPosition = column;
                    fillList.RemoveAt(randLocationInList);
                }
                if (row < 8 && row > 0)
                {
                    if (!RowIsValid(board, row))
                    {
                        row--;
                    }
                }
                else if (row == 8)
                {
                    if (!RowIsValid(board, row))
                    {
                        row--;
                    }
                }
            }
        }
        private ArrayList GenerateFillList()
        {
            ArrayList fillList = new ArrayList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            return fillList;
        }
        private bool RowIsValid(Board board, int row)
        {
            bool isValid = true;
            int currentValue = -1;
            for (int column = 0; column < Board.BoardLength; column++)
            {
                if (row != 0) currentValue = board.AllSquares[row, column].StoredValue;
                //Check Above
                for (int prevRow = row - 1; prevRow > -1; prevRow--)
                {
                    if (board.AllSquares[prevRow, column].StoredValue == currentValue) isValid = false;
                    if (!isValid) break;
                }
                if (!isValid) break;
                isValid = CheckNonet(board, row, column, currentValue);
                if (!isValid) break;
            }

            return isValid;
        }
        private bool CheckNonet(Board board, int row, int column, int currentValue)
        {
            bool isValid = true;
            //Row mod 3 if result is 0 check +2 if result is 1 check +-1 if result is 2 check -2
            int rowModHelper = row % 3;
            if (rowModHelper == 0)
            {
                isValid = CheckColumns(board, row + 1, column, currentValue);
                if (isValid) isValid = CheckColumns(board, row + 2, column, currentValue);
            }
            if (rowModHelper == 1)
            {
                isValid = CheckColumns(board, row + 1, column, currentValue);
                if (isValid) isValid = CheckColumns(board, row - 1, column, currentValue);
            }
            if (rowModHelper == 2)
            {
                isValid = CheckColumns(board, row - 2, column, currentValue);
                if (isValid) isValid = CheckColumns(board, row - 1, column, currentValue);
            }
            return isValid;
        }
        private bool CheckColumns(Board board, int row, int column, int currentValue)
        {
            bool isValid = true;
            //Column mod 3 if result is 0 check +2 if result is 1 check +-1 if result is 2 check -2
            int columnModHelper = column % 3;
            if (columnModHelper == 0)
            {
                if (board.AllSquares[row, column + 1].StoredValue == currentValue || board.AllSquares[row, column + 2].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            //Check + 1 place and check -1 place
            if (columnModHelper == 1)
            {
                if (board.AllSquares[row, column + 1].StoredValue == currentValue || board.AllSquares[row, column - 1].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            //Check back two places
            if (columnModHelper == 2)
            {
                if (board.AllSquares[row, column - 1].StoredValue == currentValue || board.AllSquares[row, column - 2].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}

