using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WpfSuduko.MVC.Controllers;

namespace WpfSuduko.MVC.Models
{
    class Board
    {

        public Square[,] AllSquares { get; }
        public Square FirstLocSet { get; set; }
        public const int BoardLength = 9, _hard = 64, _medium = 32, _easy = 20;

        public Board()
        {
            AllSquares = new Square[BoardLength, BoardLength];
            InitializeSquares();
        }
        public Board(Board board) 
        {
            AllSquares = new Square[BoardLength, BoardLength];
            InitializeSquares(board.AllSquares);
        }
        public void SetSquare(ref Square square) 
        {
            AllSquares[square.XGridPosition, square.YGridPosition] = square;
        }
        public void SetSquare(int xPos, int yPos, int newValue)
        {
            AllSquares[xPos, yPos].StoredValue = newValue;
        }
        private void InitializeSquares(Square[,] squares = null)
        {
            for (int row = 0; row < BoardLength; row++)
            {
                for (int column = 0; column < BoardLength; column++)
                {
                    if (squares != null) AllSquares[row, column] = Square.CopySquare(squares[row, column]);
                    else AllSquares[row, column] = new Square();
                }
            }
        }
        public string GetDisplayValue(int row, int column)
        {
            return AllSquares[row, column].GetDisplayValue();
        }
        public int GetValueAtLoc(int row, int column)
        {
            return AllSquares[row, column].StoredValue;
        }
        public override string ToString() 
        {
            string returnVal = "";
            for (int row = 0; row < BoardLength; row++)
            {
                for (int column = 0; column < BoardLength; column++)
                {
                    returnVal += AllSquares[row, column].ToString();
                    if (column != 8) returnVal += ",";
                }
                if (row != 8) returnVal += "\n";
            }
            return returnVal;
        }
        public override bool Equals(object otherObject)
        {
            bool equals = true;
            if (otherObject is Board otherAsBoard)
            {
                foreach (Square square in otherAsBoard.AllSquares)
                {
                    equals = this.AllSquares[square.XGridPosition, square.YGridPosition].Equals(square);
                    if (!equals) break;
                }
            }
            return equals;
        }

        public bool BoardIsComplete() 
        {
            bool isComplete = true;
            foreach (Square square in this.AllSquares)
            {
                isComplete = square.StoredValue != Square.DefaultStoredValue;
                if (!isComplete) break;
            }
            return isComplete;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AllSquares);
        }
    }
}
