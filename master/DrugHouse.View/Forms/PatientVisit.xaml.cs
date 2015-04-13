/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Windows;
using System.Windows.Controls;

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
        }

        private void PatientVisit_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ComplaintTextbox.Focus();
        }
    }
}
