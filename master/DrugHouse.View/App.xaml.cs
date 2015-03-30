using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DrugHouse.View.Properties;

namespace DrugHouse.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        public bool DoHandle { get; set; }
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (this.DoHandle)
            {
                //Handling the exception within the UnhandledException handler.
                MessageBox.Show(e.Exception.Message, "Exception Caught",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            else
            {
                //If you do not set e.Handled to true, the application will close due to crash.
                MessageBox.Show("Application is going to close! ", "Uncaught Exception");
                e.Handled = false;
            }
        }
    }
}
