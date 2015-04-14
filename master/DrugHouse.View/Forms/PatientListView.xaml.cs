/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DrugHouse.View.Helpers;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Interaction logic for MasterPageContent.xaml
    /// </summary>
    public partial class PatientListView
    {
        private PatientMasterViewModel ViewModel;
        public PatientListView()
        {
            InitializeComponent();
            this.IsVisibleChanged += OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if ((bool)e.NewValue)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate()
                {
                    SearchTextBox.Focus();
                }));
            }

        }

        #region Event Handlers

        private void PatientListView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = (PatientMasterViewModel)DataContext;
        }

        private void SearchTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                if (PatientGrid.Items.Count > 0)
                {
                    var item = (DataGridRow)PatientGrid
                        .ItemContainerGenerator
                        .ContainerFromItem(PatientGrid.Items[0]);

                    if (item != null)
                    {
                        var cell = DataGridExtensions.GetCell(PatientGrid, item, 0);

                        if (cell != null)
                            cell.Focus();
                    }

                }
            }
            e.Handled = true;


        }

        private void PatientGrid_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ViewModel != null)
                    ViewModel.OpenPatientCommand.Execute(null);
                e.Handled = true;
            }

        }
        #endregion
    }
}