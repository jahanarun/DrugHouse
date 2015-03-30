/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;
using DrugHouse.ViewModel.Patients;
using DrugHouse.ViewModel.RowItems;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Tabs
{
    [TabAttribute("C1455213-66D2-48EE-8306-AD59ACEA9024",true)]
    public sealed class PatientViewModel : TabViewModel
    {      
        private Patient Patient;

        #region Constructor

        public PatientViewModel(MasterViewModel vm)
            : base(vm)
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                //SelectedCase = new PatientCaseRow(new PatientVisit());
            }
            else
            {
                // Code runs "for real"
            }
            Patient = new Patient();
            Initialize();
            Patient.DbStatus = RepositoryStatus.New;
            RaiseDirty();
        }
        public PatientViewModel(long id, MasterViewModel vm)
            : base(vm)
        {
            Patient = Data.GetPatientDetails(id);
            Initialize();
            GuidValue = Helper.Helper.StringToGUID(id.ToString());
        }
        #endregion

        #region Properties

        public class PropName
        {
            public const string Id = "Id";
            public const string Name = "Name";
            public const string Gender = "Gender";
            public const string Age = "Age";
            public const string Address = "Address";
            public const string Location = "Location";
            public const string State = "State";
            public const string Email = "Email";
            public const string PhoneNumber = "PhoneNumber";
            public const string TabName = "TabName";
            public const string DbStatus = "DbStatus";
            public const string GuardianRelationship = "GuardianRelationship";
            public const string GuardianName = "GuardianName";
            public const string Remark = "Remark";
            public const string PatientVisitViewModel = "PatientVisitViewModel";
            public const string SelectedVisit = "SelectedVisit";
            public const string ReferredMedicalPractitioner = "ReferredMedicalPractitioner";
            public const string VisitFilter = "VisitFilter";
            public const string SelectedCase = "SelectedCase";
            
        }


        private DrugHouseViewModelBase PatientVisitViewModelValue;
        public DrugHouseViewModelBase PatientVisitViewModel
        {
            get { return PatientVisitViewModelValue; }
            set
            {
                PatientVisitViewModelValue = value;
                SelectionChanged(PropName.PatientVisitViewModel);
            }
        }

        public override string TabName
        {
            get { return "Patient - " + Name; }
        }

        private Guid GuidValue= Guid.Empty;
        public override Guid Guid
        {
            get
            {
                return GuidValue == Guid.Empty ? base.Guid : GuidValue;
            }
        }

        public List<SimpleEntity> Locations { get { return MasterViewModel.Globals.Locations; } }
        #endregion

        #region Patient Properties
        public long Id
        {
            get { return Patient.Id; }
        }
        public string Name
        {
            get { return Patient.Name; }
            set
            {
                Patient.Name = value;
                RaisePropertyChanged(PropName.Name);
                SelectionChanged(PropName.TabName);
            }
        }

        public GenderType Gender
        {
            get { return Patient.Gender; }
            set
            {
                Patient.Gender = value;
                RaisePropertyChanged(PropName.Gender);
            }
        }

        public RelationshipType GuardianRelationship
        {
            get { return Patient.GuardianRelationship; }
            set
            {
                Patient.GuardianRelationship = value;
                RaisePropertyChanged(PropName.GuardianRelationship);
            }
        }
        public string GuardianName
        {
            get { return Patient.GuardianName; }
            set
            {
                Patient.GuardianName = value;
                RaisePropertyChanged(PropName.GuardianName);
            }
        }
        public int Age
        {
            get { return Patient.Age; }
            set
            {
                Patient.Age = value;
                RaisePropertyChanged(PropName.Age);
            }
        }

        public string Address
        {
            get { return Patient.Address; }
            set
            {
                Patient.Address = value;
                RaisePropertyChanged(PropName.Address);
            }
        }
        public string Remark
        {
            get { return Patient.Remark; }
            set
            {
                Patient.Remark = value;
                RaisePropertyChanged(PropName.Remark);
            }
        }
        public SimpleEntity Location
        {
            get { return Patient.Location; }
            set
            {
                Patient.Location = value;
                RaisePropertyChanged(PropName.Location);
            }
        }

        public string Email
        {
            get { return Patient.Email; }
            set
            {
                Patient.Email = value;
                RaisePropertyChanged(PropName.Email);
            }
        }

        public string PhoneNumber
        {
            get { return Patient.PhoneNumber; }
            set
            {
                Patient.PhoneNumber = value;
                RaisePropertyChanged(PropName.PhoneNumber);
            }
        }

        //public MedicalPractitioner ReferreddMedicalPractitioner
        //{
        //    get { return Patient.ReferredMedicalPractitioner; }
        //    set
        //    {
        //        Patient.ReferredMedicalPractitioner = value;
        //        RaisePropertyChanged(PropName.ReferredMedicalPractitioner);
        //    }
        //}
        #endregion

        #region Case Properties

        public ObservableCollection<PatientCaseRow> CaseRows { get; set; }

        private PatientCaseRow SelectedCaseValue;
        public PatientCaseRow SelectedCase
        {
            get { return SelectedCaseValue; }
            set
            {
                SelectedCaseValue = value;
                if(value == null)
                    PatientVisitViewModel =  new DrugHouseViewModelBase() ;
                else if(value.CaseType == "Visit")
                    PatientVisitViewModel =  new  PatientVisitViewModel((PatientVisit) SelectedCaseValue.Case) ;
                else
                    PatientVisitViewModel =  new  PatientAdmitanceViewModel((PatientAdmitance) SelectedCaseValue.Case) ;

                AttachDirtyState(PatientVisitViewModel);
                SelectionChanged(PropName.SelectedCase);
            }
        }

        #endregion
                              
        #region Private Methods

        private void Initialize()
        {
            AddVisitCommand = new RelayCommand(AddVisit, () => true);
            RemoveCaseCommand = new RelayCommand(RemoveCase, () => SelectedCase != null);
            AddAdmittanceCommand = new RelayCommand(AddAdmittance, () => true);
            RefreshVisits();
            OnSave += (sender, args) => RefreshVisits();
        }

        private void AddVisit()
        {
            AddCase(typeof (PatientVisit));
        }

        private void AddAdmittance()
        {
            AddCase(typeof(PatientAdmitance));
        }

        private void RemoveCase()
        {
            Patient.RemoveCase(SelectedCase.Case);
            var temp = SelectedCase;
            CaseRows.Remove(temp);
            SelectedCase = (CaseRows.Count > 0 ? CaseRows.First() : null);
            RaiseDirty();
        }

        private void AddCase(Type type)
        {

            var visit = new PatientCaseRow(Patient.AddCase(type));
            CaseRows.Insert(0, visit);
            SelectedCase = visit;
            RaiseDirty();
        }
        
        private void RefreshVisits()
        {
            if (CaseRows == null)
                CaseRows = new ObservableCollection<PatientCaseRow>();
            else
                CaseRows.Clear();

            SelectedCase = null;
            foreach (var visit in Patient.Visits.OrderByDescending(p => p.Date))
            {
                var vRow = new PatientCaseRow(visit);
                CaseRows.Add(vRow);    
            }
        }

        private bool ValidateInput()
        {
            return true;
        }
                       
        private void SavePatientDetails()
        {
            Patient.CleanUp();
            Patient = Data.SavePatient(Patient);
            RaisePropertyChanged(PropName.Id);
        }


        #endregion

        #region Relay Commands
        public RelayCommand AddVisitCommand { get; private set; }
        public RelayCommand RemoveCaseCommand { get; private set; }
        public RelayCommand AddAdmittanceCommand { get; private set; }

        public override void RaiseDirty()
        {
            Patient.DbStatus = RepositoryStatus.Edited;
            base.RaiseDirty();
        }

        protected override void SaveOperations()
        {
            if (!ValidateInput()) return;
            var prevId = Id;
            SavePatientDetails();
            if (prevId == 0)
                GuidValue = Helper.Helper.StringToGUID(Id.ToString());
        }

        #endregion

    }
}
