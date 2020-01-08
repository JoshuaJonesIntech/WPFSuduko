using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfSuduko.MVC.View;

namespace WPFSudukoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{

			//Show splash screen
			MessageBox.Show("Application is starting.......");

			MainWindow mwd = new MainWindow
			{
				Title = "Suduko"
			};
			mwd.Show();
			//// Create the startup window
			//SudukoBoardView wnd = new SudukoBoardView
			//{
			//	// Do stuff here, e.g. to the window
			//	Title = "Suduko"
			//};
			//// Show the window
			//wnd.Show();
		}
	}
}
