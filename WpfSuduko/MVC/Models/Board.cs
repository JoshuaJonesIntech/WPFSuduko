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
        public const int BoardLength = 9;
        private BoardController boardController;

        public Board()
        {
            AllSquares = new Square[BoardLength, BoardLength];
            InitializeSquares();
            boardController = new BoardController();
            boardController.ResetBoard(this);
        }
        private void InitializeSquares()
        {
            for (int row = 0; row < BoardLength; row++)
            {
                for (int column = 0; column < BoardLength; column++)
                {
                    AllSquares[row, column] = new Square();
                }
            }
        }
    }
}
