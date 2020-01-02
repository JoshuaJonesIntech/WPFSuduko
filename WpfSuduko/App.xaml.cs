using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

			// Create the startup window
			SudukoBoardController wnd = new SudukoBoardController();
			// Do stuff here, e.g. to the window
			wnd.Title = "Suduko";
			// Show the window
			wnd.Show();
		}
	}
}
