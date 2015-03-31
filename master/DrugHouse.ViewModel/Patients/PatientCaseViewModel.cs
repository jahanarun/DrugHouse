/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.Patients
{
    public abstract class PatientCaseViewModel : DrugHouseViewModelBase
    {
        protected readonly ICase Case;

        protected PatientCaseViewModel(ICase patientCase)
        {
            Case = patientCase;
            if (Case.DbStatus == RepositoryStatus.New)
                Timer();

            DiagnosesItems = Diagnoses.Select(diagnosis => new ToggleButtonItem(diagnosis)).ToList();
        }

        public new class PropNameBase
        {
            public const string DoctorFee = "DoctorFee";
            public const string Diagnoses = "Diagnoses";
            public const string PrimaryDiagnosis = "PrimaryDiagnosis";
            public const string CaseDate = "CaseDate";
        }

        #region Properties

        public List<ToggleButtonItem> DiagnosesItems { get; private set; }
        public ICollection<SimpleEntity> Diagnoses { get { return MasterViewModel.Globals.Diagnoses; } }
        public SimpleEntity PrimaryDiagnosis
        {
            get
            {
                return Case.PrimaryDiagnosis;
            }
            set
            {
                Case.PrimaryDiagnosis = value;
                RaisePropertyChanged(PropNameBase.PrimaryDiagnosis);
            }
        }

   
        public string CaseDate
        {
            get { return Case.Date.ToString(CultureInfo.InvariantCulture); }
            set
            {
                Case.Date = DateTime.ParseExact(value, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                SelectionChanged(PropNameBase.CaseDate);
            }
        }

        public decimal DoctorFee
        {
            get { return Case.DoctorFee; }
            set
            {
                Case.DoctorFee = value;
                RaisePropertyChanged(PropNameBase.DoctorFee);
            }
        }
      
        #endregion

        protected void Timer()
        {
            var timer = new Timer {Interval = 1000};
            timer.Elapsed += (sender, e) =>
            {
                CaseDate = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                if (Case.DbStatus != RepositoryStatus.New)
                    timer.Stop();
            };
            timer.Start();
        }

    }
}
