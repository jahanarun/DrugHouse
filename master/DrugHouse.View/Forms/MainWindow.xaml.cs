/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DrugHouse.Shared.Helpers;
using DrugHouse.View.Properties;
using DrugHouse.View.Services;
using DrugHouse.ViewModel;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.View.Forms
{
    public partial class MainWindow : Window
    {
        private readonly MasterViewModel ViewModel;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                ViewModel = (MasterViewModel) DataContext;
                DrugHouseViewModelBase.MessageService = MessageServiceBoxService.Instance;
                ViewModel.PropertyChanged += MasterViewModelPropertyChanged;
                ViewModel.OnViewLoaded();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Helper.GetInnerMostException(ex).Message);
            }
        }

        private void MasterViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case MasterViewModel.PropName.SelectedTabChanged:
                    break;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void RootWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !ViewModel.PrepareClosing();
        }

        private void Ribbon_OnLoaded(object sender, RoutedEventArgs e)
        {
            var child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                child.RowDefinitions[0].Height = new GridLength(0);
            }
            Ribbon.Height = 100;
        }
    }
}