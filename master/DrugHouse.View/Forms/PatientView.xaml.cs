/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;

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
        }

        private void AddCasePopupButton_Click(object sender, RoutedEventArgs e)
        {
            AddCasePopup.IsOpen = true;
        }

        private void AddCaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddCasePopup.IsOpen = false;
        }
    }
}
