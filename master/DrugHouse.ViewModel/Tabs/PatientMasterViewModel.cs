/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using DrugHouse.Model;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.RowItems;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Tabs
{
    [TabAttribute("173BE214-DB56-4D7B-90EA-323BD561026B")]
    public class PatientMasterViewModel : TabViewModel
    {
        public RelayCommand OpenPatientCommand { get; private set; }
        public RelayCommand FilterPatientCommand { get; private set; }
        public RelayCommand DeletePatientCommand { get; private set; }

        private List<Patient> InternalePatientList;
       

        protected override IDataAccess Data
        {
            get
            {
                return DataAccess.GetInstance();                
            }
        }

        #region Constructors
        public PatientMasterViewModel(MasterViewModel masterVm)
            : base(masterVm)
        {
            if (masterVm == null) throw new ArgumentNullException("masterVm");
            FilterParameter= new PatientListRow();
            TabNameValue = "Patients";
            FilterPatientCommand = new RelayCommand(ExecuteFilterPatient, ()=>true);
            OpenPatientCommand = new RelayCommand(ExecuteOpenPatient, CanExecuteOpenPatient);
            DeletePatientCommand = new RelayCommand(ExecuteDeletePatient, CanExecuteDeletePatient);
            RefreshInternalPatientList();
            FilterPatientCommand.Execute(null);
        }


        #endregion

        #region Private Members
        private void ExecuteFilterPatient()
        {
            Func<Patient, bool> condition;
            long searchNumber = 0;
            var searchText = SearchString.ToUpper();
            if (SearchString != string.Empty && long.TryParse(SearchString, out searchNumber))
            {
                condition = (x => x.Id == searchNumber ||
                                  x.Name.ToUpper().Contains(searchText) ||
                                  x.Address.ToUpper().Contains(searchText) ||
                                  x.Location.Name.ToUpper().Contains(searchText) ||
                                  x.Age == searchNumber &&
                                  x.DbStatus != RepositoryStatus.Deleted
                            );
            }
            else if (SearchString != string.Empty)
            {
                condition = (x => x.Name.ToUpper().Contains(searchText) ||
                                  x.Address.ToUpper().Contains(searchText) ||
                                  x.Location.Name.ToUpper().Contains(searchText) &&
                                  x.DbStatus != RepositoryStatus.Deleted
                            );
            }
            else
                condition = x => x.DbStatus != RepositoryStatus.Deleted;

            FilterPatient(condition);
         
        }

        private void FilterPatient(Func<Patient, bool> condition)
        {
            var patientList = InternalePatientList.Where(condition).Select(patient => new PatientListRow()
            {
                Id = patient.Id,
                Name = patient.Name,
                Address = patient.Address,
                Age = patient.Age,
                Location = patient.Location.Name                
            }).ToList();

            PatientFilteredCollectionViewValue = new ListCollectionView(patientList);
            SelectionChanged(PropName.PatientFilteredCollectionView);
        }
        #endregion

        #region Public Members

        public override void RefreshAfterSaveOperations()
        {                                
            base.RefreshAfterSaveOperations();
            RefreshInternalPatientList();
            FilterPatientCommand.Execute(null);
            SelectedPatient = (PatientListRow) PatientFilteredCollectionView.CurrentItem;
        }

        private void RefreshInternalPatientList()
        {
            using (var data = Data)
            {
                InternalePatientList = data.GetAllPatients().ToList();
            }   
        }

        #endregion

        #region RelayCommands
      
        private bool CanExecuteOpenPatient()
        {
            return SelectedPatient != null;
        }

        private void ExecuteOpenPatient()
        {
            var vm = new PatientViewModel(SelectedPatient.Id, MasterWindow);
            MasterWindow.TabManager.OpenTabCommand.Execute(vm);
        }

        protected override bool CanExecuteClose()
        {
            return false;
        }

        private void ExecuteDeletePatient()
        {
            var patient = InternalePatientList.Find(x => x.Id == SelectedPatient.Id);
            patient.DbStatus = RepositoryStatus.Deleted;
            RaiseDirty();
            FilterPatientCommand.Execute(null); 
        }

        private bool CanExecuteDeletePatient()
        {
            return SelectedPatient != null;
        }
        #endregion

        #region Properties   
        public class PropName
        {
            public const string PatientListUpdated = "PatientListUpdated";
            public const string TabName = "TabName";
            public const string OpenPatient = "OpenPatient";
            public const string SelectedPatient = "SelectedPatient";
            public const string SelectedItem = "SelectedItem";
            public const string PatientFilteredCollectionView = "PatientFilteredCollectionView";
            public const string SearchString = "SearchString";
        }

        private string SearchStringValue = string.Empty;
        public string SearchString
        {
            get { return SearchStringValue; }
            set
            {
                SearchStringValue = value;
                FilterPatientCommand.Execute(null);
                SelectionChanged(PropName.SearchString);
            }
        }

        public PatientListRow FilterParameter { get; private set; }

        private ListCollectionView PatientFilteredCollectionViewValue;
        public CollectionView PatientFilteredCollectionView
        {
            get { return PatientFilteredCollectionViewValue; }
        }

        private readonly string TabNameValue;
        public override string TabName
        {
            get { return TabNameValue; }
        }

        private PatientListRow SelectedPatientValue;

        public PatientListRow SelectedPatient
        {
            get { return SelectedPatientValue; }
            set
            {
                SelectedPatientValue = value;
                SelectionChanged(PropName.SelectedPatient);
            }

        }
        #endregion

        protected override void SaveOperations()
        {
            using (var data = Data)
            {
                data.SaveIEnumerable(InternalePatientList);
            }
            RefreshAfterSaveOperations();
        }
    }
}
