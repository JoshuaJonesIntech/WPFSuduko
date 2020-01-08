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
using WpfSuduko.MVC.Controllers;
using WpfSuduko.MVC.Models;

namespace WPFSudukoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SudukoBoardView : UserControl
    {
        private readonly BoardController boardController;
        public SudukoBoardView()
        {
            InitializeComponent();
            boardController = BoardController.Instance;
            GenerateView();
        }

        private void GenerateView() 
        {
            for (int row = 0; row < 9; row++) 
            {
                for (int column = 0; column < 9; column++) 
                {
                    TextBox myTextBox = new TextBox
                    {
                        Margin = GetTextBoxMargin(),
                        Name = "GridTextBox" + row + column,
                        FontSize = 15,
                        Text = boardController.SudukoBoard.GetDisplayValue(row, column),
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center
                    };
 
                    myTextBox.LostKeyboardFocus += EndKeyboardFocusEvent;
                    if (myTextBox.Text != String.Empty)
                    {
                        myTextBox.IsEnabled = false;
                    }
                    Grid.SetRow(myTextBox, row);
                    Grid.SetColumn(myTextBox, column);
                    pnlMainGrid.Children.Add(myTextBox);
                }
            }
        }
        private Thickness GetTextBoxMargin() 
        {
            Thickness margin = pnlMainGrid.Margin;
            margin.Top = 2;
            margin.Bottom = 2;
            margin.Left = 2;
            margin.Right = 2;
            return margin;
        }
        private void EndKeyboardFocusEvent(object sender, RoutedEventArgs routedEventArgs)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox senderAsTB = (TextBox)sender;
                try
                {
                    int textAsInt = Int32.Parse(senderAsTB.Text);
                    //It is not a valid suduko number
                    if (textAsInt > 9 || textAsInt < 0)
                    {
                        MessageBox.Show("The input value must be a number between 1 and 9!");
                        senderAsTB.Text = boardController.SudukoBoard.GetDisplayValue(Grid.GetRow(senderAsTB), Grid.GetColumn(senderAsTB));
                    }
                    //It is valid, set the value in the board.
                    else
                    {
                        boardController.SudukoBoard.SetSquare(Grid.GetRow(senderAsTB), Grid.GetColumn(senderAsTB), textAsInt);
                    }
                }
                catch (FormatException)
                {
                    if (senderAsTB.Text != String.Empty) 
                    {
                        MessageBox.Show("The input value must be a number!");
                        senderAsTB.Text = boardController.SudukoBoard.GetDisplayValue(Grid.GetRow(senderAsTB), Grid.GetColumn(senderAsTB));
                    }
                }
            }
        }
    }
}
