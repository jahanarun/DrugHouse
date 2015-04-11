/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;
using System.Windows.Controls;
using Dexuse.ICommand;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Interaction logic for DrugAdmin.xaml
    /// </summary>
    public partial class DrugAdmin : UserControl
    {
        private bool IsAddButtonCanExecutedBinded;
        public DrugAdmin()
        {
            InitializeComponent();
        }

        private void AddDrugButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsAddButtonCanExecutedBinded)
                ((ObservableCommand)AddDrugButton.Command).CommandExecuted += (o, a) => AfterAddingNewItem();
            IsAddButtonCanExecutedBinded = true;
        }

        private void AfterAddingNewItem()
        {
            DrugNameTextBox.SelectionStart = 0;
            DrugNameTextBox.SelectionLength = DrugNameTextBox.Text.Length;
            DrugNameTextBox.Focus();
        }
    }
}
