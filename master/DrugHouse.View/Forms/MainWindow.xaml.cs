/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Windows;
using DrugHouse.Shared.Helpers;
using DrugHouse.View.Properties;
using DrugHouse.View.Services;
using DrugHouse.ViewModel;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Description for MainWindow.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the TestView class.
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                ViewModel = (MasterViewModel) DataContext;
                DrugHouseViewModelBase.MessageService = MessageServiceBoxService.Instance;
                ViewModel.OnViewLoaded();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Helper.GetInnerMostException(ex).Message);
            }
        }


        private readonly MasterViewModel ViewModel;
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void RootWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ViewModel.PrepareClosing();
        }
    }
}