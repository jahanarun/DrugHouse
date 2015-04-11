/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Windows;
using System.Windows.Controls;
using Dexuse.ICommand;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Interaction logic for DropdownAdminScreen.xaml
    /// </summary>
    public partial class DropdownAdminScreen : UserControl
    {
        private bool IsAddButtonCanExecutedBinded;
        public DropdownAdminScreen()
        {
            InitializeComponent();  
        }

        private void AfterAddingNewItem()
        {
            NewItemTextBox.SelectionStart = 0;
            NewItemTextBox.SelectionLength = NewItemTextBox.Text.Length;
            NewItemTextBox.Focus();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsAddButtonCanExecutedBinded)
                ((ObservableCommand)AddButton.Command).CommandExecuted += (o, a) => AfterAddingNewItem();
            IsAddButtonCanExecutedBinded = true;

        }
    }
}
