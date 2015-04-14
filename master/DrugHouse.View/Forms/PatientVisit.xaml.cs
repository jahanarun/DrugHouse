/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace DrugHouse.View.Forms
{
    /// <summary>
    /// Interaction logic for Visit.xaml
    /// </summary>
    public partial class PatientVisit : UserControl
    {
        public PatientVisit()
        {
            InitializeComponent(); 
            this.IsVisibleChanged += OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate()
                {
                    ComplaintTextbox.Focus();
                }));
            } 

        }

        private void PatientVisit_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ComplaintTextbox.Focus();
        }

        private void AddDrugButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DrugListBox.Items.Count > 0)
            {
                var listBoxItem = (ListBoxItem) DrugListBox
                    .ItemContainerGenerator
                    .ContainerFromItem(DrugListBox.Items[0]);

                if(listBoxItem != null)
                    listBoxItem.Focus();
            }
        }
    }
}
