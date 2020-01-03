using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WpfSuduko.MVC.Models
{
    class Board
    {

        public Square[,] AllSquares { get; }
        private Random _rand;
        private const int BoardLength = 9;

        public Board() 
        {
            AllSquares = new Square[BoardLength, BoardLength];
            InitializeSquares();
            _rand = new Random();
            GenerateBoard();
        }
        private void InitializeSquares()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    AllSquares[row, column] = new Square();
                }
            }
        }
        public void ResetBoard() 
        {
            GenerateBoard();
        }

        //@Unhandled ClassCastException;
        private void GenerateBoard()
        {
            ArrayList fillList;
            int randLocationInList;
            for (int row = 0; row < BoardLength; ++row)
            {
                fillList = GenerateFillList();
                for (int column = 0; column < BoardLength; ++column)
                {
                    randLocationInList = _rand.Next(0, fillList.Count);
                    AllSquares[row, column].StoredValue = (int)fillList[randLocationInList];
                    fillList.RemoveAt(randLocationInList);
                }

                if (row < 8 && row > 0)
                {
                    if (!RowIsValid(row))
                    {
                        row--;
                    }
                }
                else if (row == 8)
                {
                    if (!RowIsValid(row))
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
        private bool RowIsValid(int row)
        {
            bool isValid = true;
            int currentValue = -1;
            for (int column = 0; column < 9; column++) 
            {
                if(row != 0) currentValue = AllSquares[row, column].StoredValue;
                //Check Above
                for (int prevRow = row - 1; prevRow > -1; prevRow--)
                {
                    if (AllSquares[prevRow, column].StoredValue == currentValue) isValid = false;
                    if (!isValid) break;
                }
                if (!isValid) break;
                isValid = CheckNonet(row, column, currentValue);
                if (!isValid) break;
            }

            return isValid;
        }

        private bool CheckNonet(int row, int column, int currentValue) 
        {
            bool isValid = true;
            //Row mod 3 if result is 0 check +2 if result is 1 check +-1 if result is 2 check -2
            int rowModHelper = row % 3;
            if (rowModHelper == 0) 
            {
                isValid = CheckColumns(row + 1, column, currentValue);
                if(isValid) isValid = CheckColumns(row + 2, column, currentValue);
            }
            if (rowModHelper == 1)
            {
                isValid = CheckColumns(row + 1, column, currentValue);
                if (isValid) isValid = CheckColumns(row - 1, column, currentValue);
            }
            if (rowModHelper == 2)
            {
                isValid = CheckColumns(row - 2, column, currentValue);
                if (isValid) isValid = CheckColumns(row - 1, column, currentValue);
            }
            return isValid;
        }
        private bool CheckColumns(int row, int column, int currentValue ) 
        {
            bool isValid = true;
            //Column mod 3 if result is 0 check +2 if result is 1 check +-1 if result is 2 check -2
            int columnModHelper = column % 3;
            if (columnModHelper == 0)
            {
                if (AllSquares[row, column + 1].StoredValue == currentValue || AllSquares[row, column + 2].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            //Check + 1 place and check -1 place
            if (columnModHelper == 1)
            {
                if (AllSquares[row, column + 1].StoredValue == currentValue || AllSquares[row, column - 1].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            //Check back two places
            if (columnModHelper == 2)
            {
                if (AllSquares[row, column - 1].StoredValue == currentValue || AllSquares[row, column - 2].StoredValue == currentValue)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        //private void GenerateBoard()
        //{
        //    ArrayList fillList;
        //    int randLocationInList;

        //    for (int row = 0; row < BoardLength; ++row)
        //    {
        //        fillList = GenerateFillList();
        //        for (int column = 0; column < BoardLength; ++column)
        //        {
        //            randLocationInList = _rand.Next(0, fillList.Count - 1);
        //            AllSquares[row, column].StoredValue = (int)fillList[randLocationInList];
        //            fillList.RemoveAt(randLocationInList);
        //        }

        //    }
        //}

        //private bool IsBoardFilled() 
        //{
        //    bool isCompleted = true;
        //    for (int r = 0; r < 9; ++r) 
        //    {
        //        for (int c = 0; c < 9; ++c) 
        //        {
        //            if (AllSquares[r, c].StoredValue < 0) 
        //            {
        //                isCompleted = false;
        //                break;
        //            }
        //        }
        //        if (!isCompleted) break;
        //    }
        //    return isCompleted;
        //}
    }
}
