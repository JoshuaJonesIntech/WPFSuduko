using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSuduko.MVC.Controllers;
using WpfSuduko.MVC.Models;

namespace WpfSuduko.MVC.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BoardController boardController;
        public MainWindow()
        {
            InitializeComponent();
            boardController = BoardController.Instance;
        }

        private void SolveClicked(object sender, RoutedEventArgs e)
        {
            if (boardController.IsSolved())
            {
                MessageBox.Show("YAYAYAAYA, you won! The board is valid.");
            }
            else 
            {
                MessageBox.Show("Oooops, looks like the board is not valid. Keep trying!");
            }
        }
        private void NewGameClicked(object sender, RoutedEventArgs e)
        {
            if (EasyButton.IsChecked == true) 
            {
                boardController.ResetBoard();
            }
            if (MediumButton.IsChecked == true)
            {
                boardController.ResetBoard(Board._medium);
            }
            if (HardButton.IsChecked == true)
            {
                boardController.ResetBoard(Board._hard);
            }
            SudukoBoardFrame.Refresh();
        }
    }
}
