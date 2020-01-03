using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSuduko.MVC.Models;

namespace WPFSudukoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SudukoBoardController : Window
    {
        private Board board;
        public SudukoBoardController()
        {
            InitializeComponent();
            board = new Board();
            generateGridButtons();
        }

        private void generateGridButtons() 
        {
            for (int row = 0; row < 9; row++) 
            {
                for (int column = 0; column < 9; column++) 
                {
                    Button myButton = new Button();
                    myButton.Name = "GridButton" + row + column;
                    myButton.Content = board.AllSquares[row, column].StoredValue;
                    myButton.FontSize = 15;
                    myButton.Click += gridButtonClicked;
                    Grid.SetRow(myButton, row);
                    Grid.SetColumn(myButton, column);
                    pnlMainGrid.Children.Add(myButton);
                }
            }
        }
        private void gridButtonClicked(object sender, RoutedEventArgs routedEventArgs) {
            MessageBox.Show("You clicked me " + sender.ToString());

        }
    }
}
