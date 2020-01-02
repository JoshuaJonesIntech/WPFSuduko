using System;
using System.Collections.Generic;
using System.Text;

namespace WpfSuduko.MVC.Models
{
    class Board
    {
        public Square[,] AllSquares { get; }

        public Board() 
        {
            AllSquares = new Square[9, 9];
        }


        /**
        Add a random number at one of the free cells(the cell is chosen randomly, and the number is chosen randomly from the list of numbers valid for this cell according to the SuDoKu rules).

        Use the backtracking solver to check if the current board has at least one valid solution. If not, undo step 2 and repeat with another number and cell.
        Note that this step might produce full valid boards on its own, but those are in no way random.

        Repeat until the board is completely filled with numbers.
        */ 
        private void GenerateBoard() 
        {
            
        }
    }
}
