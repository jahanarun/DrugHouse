/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.ObjectModel;
using DrugHouse.Model;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.ReportScreen
{
    [Screen("Medical Certificate")]
    public class MedicalCertificateViewModel : ReportViewModelBase
    {
        private Patient Patient = Patient.Empty;

        public MedicalCertificateViewModel()
        {
            Cases = new ObservableCollection<PatientCaseRow>();
            SearchCommand = new RelayCommand(ExecuteSearchCommand, () => true);
            DoctorName = ApplicationConstants.DoctorName;
            InitializeValues();
        }

        private void InitializeValues()
        {
            Visit = new PatientVisit(Patient);
            Diagnosis = Visit.PrimaryDiagnosis.Name;
            PatientName = Patient.Name;
            FromDate = Visit.Date;
            Duration = Visit.RestDuration;
        }

        #region Properties
        public class PropName
        {
            public const string PatientName = "PatientName";
            public const string Diagnosis = "Diagnosis";
            public const string Duration = "Duration";
            public const string FromDate = "FromDate";
            public const string SelectedCase = "SelectedCase";
        }

        protected PatientVisit Visit
        {
            get { return (PatientVisit) Entity; }
            set { Entity = value; }
        }

        public RelayCommand SearchCommand { get; private set; }

        public string SearchText { get; set; }

        public ObservableCollection<PatientCaseRow> Cases { get; private set; }

        private PatientCaseRow SelectedCaseValue;

        public PatientCaseRow SelectedCase
        {
            get { return SelectedCaseValue; }
            set
            {
                SelectedCaseValue = value; 
                SelectionChanged(PropName.SelectedCase);
                Visit = (PatientVisit) SelectedCase.Case;
                Diagnosis = Visit.PrimaryDiagnosis.Name;
                PatientName = Patient.Name;
                FromDate = Visit.Date;
                Duration = Visit.RestDuration;
            }
        }

        public string DoctorName { get; set; }

        private string PatientNameValue = string.Empty;
        private string DiagnosisValue = string.Empty;
        private string DurationValue;
        private DateTime FromDateValue;

        public string PatientName
        {
            get { return PatientNameValue; }
            set
            {
                PatientNameValue = value; 
                SelectionChanged(PropName.PatientName);
            }
        }

        public string Diagnosis
        {
            get { return DiagnosisValue; }
            set
            {
                DiagnosisValue = value;
                SelectionChanged(PropName.Diagnosis);
            }
        }

        public string Duration
        {
            get { return DurationValue; }
            set
            {
                DurationValue = value;
                SelectionChanged(PropName.Duration);
            }
        }

        public DateTime FromDate
        {
            get { return FromDateValue; }
            set
            {
                FromDateValue = value;
                SelectionChanged(PropName.FromDate);
            }
        }

        #endregion

        public async override void GenerateReportAsync()
        {
            try
            {
                var task = Report.CreateDocumentAsync(DocumentType.MedicalCertificate, Visit);
                await task;
            }
            catch (Exception ex)
            {
                MessageService.Error(ex.Message, "Medical Certificate Generation");
            }
        }

        public override void GenerateReport()
        {
            Report.CreateDocument(DocumentType.MedicalCertificate, Visit);
        }

        private void GenerateCaseRows()
        {
            Cases.Clear();
            foreach (var visit in Patient.Visits)
            {
                var row = new PatientCaseRow(visit);
                Cases.Add(row);
            }
        }

        #region Relay Commands
        private void ExecuteSearchCommand()
        {
            using (var data = DataAccess.GetInstance())
            {
                if (data.TryGetPatientDetails(Convert.ToInt64(SearchText), out Patient))
                {
                    GenerateCaseRows();
                }
            }
        }
        #endregion
    }
}
