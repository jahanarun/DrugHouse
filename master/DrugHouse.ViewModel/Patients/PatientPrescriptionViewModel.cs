/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DrugHouse.Model;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Patients
{
    public class PatientPrescriptionViewModel : DrugHouseViewModelBase
    {
        private bool Ignore = false;
        private DrugType LastDrugType = DrugType.None;
        private readonly IDataAccess DataAccess;

        public PatientPrescriptionViewModel(PatientVisit visit, IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
            AddDrugCommand = new RelayCommand(AddDrug, CanAddDrug);
            RemoveDrugCommand = new RelayCommand(RemoveDrug, CanRemoveDrug);
            Prescriptions = new ObservableCollection<PrescriptionRow>();
            Visit = visit;
            visit.Prescriptions = visit.Prescriptions ?? new Collection<Prescription>();
            SelectedDrugTypeValue = DrugType.None;

            foreach (var p in PrescriptionList.Where(p=> p.DbStatus != RepositoryStatus.Deleted 
                                                         && p.DbStatus!= RepositoryStatus.Disregard)
                                              .Select(prescription => new PrescriptionRow(prescription)))
            {
                Prescriptions.Add(p);
            }
        }

        private readonly PatientVisit Visit;
        private IList<Prescription> PrescriptionList{get { return Visit.Prescriptions; }}

        public class PropName
        {
            public const string SelectedDrugCount = "SelectedDrugCount";
            public const string SelectedDrug = "SelectedDrug";
            public const string SelectedPrescription = "SelectedPrescription";
            public const string SelectedTypeDrugList = "SelectedTypeDrugList";
            public const string PrescriptionList = "PrescriptionList";
            public const string SelectedDrugType = "SelectedDrugType";
            public const string SelectedDosage = "SelectedDosage";
            public const string SelectedRemark = "SelectedRemark";
            public const string PrescriptionFilter = "PrescriptionFilter";
            public const string IsDrugPanelVisible = "IsDrugPanelVisible";
        }
        #region Properties
        public ICollection<Drug> DrugList
        {
            get { return MasterViewModel.Globals.Drugs(DataAccess).OrderBy(d => d.Name).ToList(); }
        }

        
        public ObservableCollection<PrescriptionRow> Prescriptions { get; private set; }

        public bool IsDrugPanelVisible { get { return SelectedPrescription != null; } }

        private Drug SelectedDrugValue;
        public Drug SelectedDrug
        {
            get { return SelectedDrugValue; }
            set
            {
                if (SelectedPrescription == null)
                    return;
                if (value == null) return;
                SelectedDrugValue = value;
                SelectedPrescription.Drug = value;
                RaisePropertyChanged(PropName.SelectedDrug);
                RaisePropertyChanged(PropName.SelectedPrescription);
            }
        }
        private PrescriptionRow SelectedPrescriptionValue;
        public PrescriptionRow SelectedPrescription
        {
            get { return SelectedPrescriptionValue; }
            set
            {
                Ignore = true;
                SelectedPrescriptionValue = value;
                SelectionChanged(PropName.IsDrugPanelVisible);
                if (value == null)
                    return;
                SelectedDrugValue = SelectedPrescriptionValue.Drug;
                SelectedDrugTypeValue = SelectedPrescriptionValue.DrugType;
                SelectedDrugCountValue = SelectedPrescriptionValue.DrugCount;
                SelectedRemarkValue = SelectedPrescriptionValue.Remark;

                SelectionChanged(PropName.SelectedDrug);
                SelectionChanged(PropName.SelectedDrugCount);
                SelectionChanged(PropName.SelectedPrescription);
                SelectionChanged(PropName.SelectedDrugType);
                SelectionChanged(PropName.SelectedDosage);
                SelectionChanged(PropName.SelectedRemark);

                Ignore = false;
            }
        }

        private DrugType SelectedDrugTypeValue;
        public DrugType SelectedDrugType
        {
            get { return SelectedDrugTypeValue; }
            set
            {
                if (SelectedPrescription == null)
                    return;
                if (Ignore) return;
                SelectedDrugTypeValue = value;
                SelectionChanged(PropName.SelectedDrugType);
            }
        }
        public ICollection<string> PrescriptionCollection
        {
            get
            {
                return Prescription.Dosages;
            } 
        }
        public string  SelectedDosage
        {
            get
            {
                return SelectedPrescription!= null ? SelectedPrescription.Dosage : string.Empty;
            }
            set
            {
                if (SelectedPrescription == null)
                    return;
                SelectedPrescription.Dosage = value;
                RaisePropertyChanged(PropName.SelectedDosage);
            }
        }
        private string SelectedRemarkValue;
        public string SelectedRemark
        {
            get { return SelectedRemarkValue; }
            set
            {
                if (SelectedPrescription == null)
                    return;
                SelectedRemarkValue = value;
                SelectedPrescription.Remark = value;
                RaisePropertyChanged(PropName.SelectedRemark);
            }
        }

        private int SelectedDrugCountValue;
        public int SelectedDrugCount
        {
            get { return SelectedDrugCountValue; }
            set
            {
                if (SelectedPrescription == null)
                    return;
                SelectedDrugCountValue = value;
                SelectedPrescription.DrugCount = value;
                RaisePropertyChanged(PropName.SelectedDrugCount);
            }
        }

        #endregion

        #region PrivateProperties


        #endregion

        #region RelayCommand
        public RelayCommand AddDrugCommand { get; private set; }
        public RelayCommand RemoveDrugCommand { get; private set; }

        private bool CanAddDrug()
        {
            return true;
        }
        private void AddDrug()
        {
            var prescription = new PrescriptionRow(Visit.AddPrescription());
            prescription.PropertyChanged += (o,e) => RaiseDirty();
            Prescriptions.Add(prescription);
            SelectedPrescription = prescription;
            RaiseDirty();
        }
        private bool CanRemoveDrug()
        {
            if (SelectedPrescription == null)
                return false;

            return SelectedPrescription.Prescription != Prescription.Empty;
        }

        private void RemoveDrug()
        {
            var row = SelectedPrescription;
            row.Delete();
            Prescriptions.Remove(row);
            SelectedPrescription = null;
            RaiseDirty();
        }
        #endregion

        #region PrivateMethods


        #endregion

        public List<Prescription> GetList()
        {
            return Prescriptions.Select(prescriptionRow => prescriptionRow.Prescription).ToList();
        }

    }
}