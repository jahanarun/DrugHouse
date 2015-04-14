/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView 
    {
        public PatientView()
        {
            InitializeComponent();
            this.IsVisibleChanged += OnIsVisibleChanged;
        }


        private PatientViewModel ViewModel;

        private void AddCasePopupButton_Click(object sender, RoutedEventArgs e)
        {
            AddCasePopup.IsOpen = true;
        }

        private void AddCaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddCasePopup.IsOpen = false;
        }


        private void OnFocusChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case PatientViewModel.FocusElement.Case:
                    TabControl.SelectedIndex = 1;
                    break;                 
            }
        }

        private void PatientView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            ViewModel = (PatientViewModel)DataContext;
            if(ViewModel != null)
                ViewModel.OnFocusChanged += OnFocusChanged;          
        }

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
            {
                textbox.SelectionStart = 0;
                textbox.SelectionLength = textbox.Text.Length;
            }
        }
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate()
                {
                    NameTextBox.Focus();
                }));
            }

        }
    }
}
