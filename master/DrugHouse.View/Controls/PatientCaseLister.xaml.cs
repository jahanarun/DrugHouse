/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DrugHouse.ViewModel.RowItems;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.View.Controls
{
    /// <summary>
    /// Interaction logic for PatientCaseLister.xaml
    /// </summary>
    public partial class PatientCaseLister : UserControl
    {
        public PatientCaseLister()
        {
            InitializeComponent();
        }

        public string SearchText
        {
            get { return (string) GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public RelayCommand SearchCommand
        {
            get { return (RelayCommand )GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }

        public ObservableCollection<PatientCaseRow> PatientCases
        {
            get { return (ObservableCollection<PatientCaseRow>)GetValue(PatientCasesProperty); }
            set { SetValue(PatientCasesProperty, value); }
        }

        public PatientCaseRow SelectedCase
        {
            get { return (PatientCaseRow) GetValue(SelectedCaseProperty); }
            set { SetValue(SelectedCaseProperty, value); }
        }


        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string),
              typeof(PatientCaseLister), new PropertyMetadata(null));

        public static readonly DependencyProperty SearchCommandProperty =
            DependencyProperty.Register("SearchCommand", typeof(RelayCommand),
            typeof(PatientCaseLister), new PropertyMetadata(default(RelayCommand)));

        public static readonly DependencyProperty PatientCasesProperty =
            DependencyProperty.Register("PatientCases", typeof(ObservableCollection<PatientCaseRow>),
            typeof(PatientCaseLister), new PropertyMetadata(default(ObservableCollection<PatientCaseRow>)));

        public static readonly DependencyProperty SelectedCaseProperty = 
            DependencyProperty.Register("SelectedCase", typeof (PatientCaseRow), 
            typeof (PatientCaseLister), new PropertyMetadata(default(PatientCaseRow)));
    }
}
