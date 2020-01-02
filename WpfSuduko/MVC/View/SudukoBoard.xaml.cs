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

namespace WPFSudukoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SudukoBoardController : Window
    {
        public SudukoBoardController()
        {
            InitializeComponent();
            //Subscribing to the MouseUp event within the code file.
            generateGridButtons();
        }

        private void generateGridButtons() {
            for (int i = 0; i < 9; i++) {
                for (int g = 0; g < 9; g++) {
                    Button myButton = new Button();
                    myButton.Name = "GridButton" + i + g;
                    myButton.Content = "( " + i + ", " + g + " )";
                    myButton.FontSize = 15;
                    myButton.Click += gridButtonClicked;
                    Grid.SetRow(myButton, i);
                    Grid.SetColumn(myButton, g);
                    pnlMainGrid.Children.Add(myButton);
                }
            }
        }
        private void gridButtonClicked(object sender, RoutedEventArgs routedEventArgs) {
            MessageBox.Show("You clicked me " + sender.ToString());

        }
    }
}
