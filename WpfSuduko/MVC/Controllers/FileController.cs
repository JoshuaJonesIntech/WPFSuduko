using System;
using System.Collections.Generic;
using System.Text;
using WpfSuduko.MVC.Models;

namespace WpfSuduko.MVC.Controllers
{
    class FileController
    {
        private string _filePath = "D:\\Workspaces\\WpfSuduko\\WpfSuduko\\Files\\Boards.txt";
        public void AppendBoardToFile(Board board, string optionalAppendBegin = "") 
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@_filePath, true))
            {
                file.WriteLine(optionalAppendBegin + board.ToString());
            }
        }
    }
}
