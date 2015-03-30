/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
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

        public PatientPrescriptionViewModel(PatientVisit visit)
        {
            AddDrugCommand = new RelayCommand(AddDrug, CanAddDrug);
            RemoveDrugCommand = new RelayCommand(RemoveDrug, CanRemoveDrug);
            PrescriptionsValue = new ObservableCollection<PrescriptionRow>();
            Visit = visit;
            visit.Prescriptions = visit.Prescriptions ?? new Collection<Prescription>();
            SelectedDrugTypeValue = DrugType.None;
            foreach (var prescription in PrescriptionList)
            {
                var p = new PrescriptionRow(prescription);
                PrescriptionsValue.Add(p);
            }
            FilterPrescription();
            PrescriptionsValue.CollectionChanged += PrescriptionsValueCollectionChanged;
        }

        private readonly PatientVisit Visit;
        private ICollection<Prescription> PrescriptionList{get { return Visit.Prescriptions; }}

        public class PropName
        {
            public const string SelectedDrug = "SelectedDrug";
            public const string SelectedPrescription = "SelectedPrescription";
            public const string SelectedTypeDrugList = "SelectedTypeDrugList";
            public const string PrescriptionList = "PrescriptionList";
            public const string SelectedDrugType = "SelectedDrugType";
            public const string SelectedPrescriptionType = "SelectedPrescriptionType";
            public const string SelectedRemark = "SelectedRemark";
            public const string PrescriptionFilter = "PrescriptionFilter";
            public const string IsDrugPanelVisible = "IsDrugPanelVisible";
        }
        #region Properties
        public ICollection<Drug> DrugList
        {
            get { return MasterViewModel.Globals.Drugs; }
        }

        private List<Drug> SelectedTypeDrugListValue;
        public List<Drug> SelectedTypeDrugList
        {
            get { return SelectedTypeDrugListValue; }
            set
            {
                SelectedTypeDrugListValue = value;
                SelectionChanged(PropName.SelectedTypeDrugList);
            }
        }

        private ListCollectionView PrescriptionFilterValue;
        public CollectionView PrescriptionFilter
        {
            get { return PrescriptionFilterValue; }
        }

        private ObservableCollection<PrescriptionRow> PrescriptionsValue;
        public ObservableCollection<PrescriptionRow> Prescriptions
        {
            get { return PrescriptionsValue; }
            set
            {
                PrescriptionsValue = value;
                FilterPrescription();

            }
        }
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
                if (value == null)
                    return;
                SelectedPrescriptionValue = value;
                SelectedDrugValue = SelectedPrescriptionValue.Drug;
                SelectedDrugTypeValue = SelectedPrescriptionValue.DrugType;
                SelectedRemarkValue = SelectedPrescriptionValue.Remark;
                SelectionChanged(PropName.IsDrugPanelVisible);
                SelectionChanged(PropName.SelectedDrug);
                SelectionChanged(PropName.SelectedPrescription);
                SelectionChanged(PropName.SelectedDrugType);
                SelectionChanged(PropName.SelectedPrescriptionType);
                SelectionChanged(PropName.SelectedRemark);
                UpdateDrugList(SelectedDrugTypeValue);

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
                UpdateDrugList(SelectedDrugTypeValue);
            }
        }
        public ICollection<string> PrescriptionCollection
        {
            get
            {
                return Prescription.PrescriptionCollection;
            } 
        }
        public string  SelectedPrescriptionType
        {
            get
            {
                return SelectedPrescription!= null ? SelectedPrescription.PrescriptionType : string.Empty;
            }
            set
            {
                if (SelectedPrescription == null)
                    return;
                SelectedPrescription.PrescriptionType = value;
                RaisePropertyChanged(PropName.SelectedPrescriptionType);
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

        #endregion

        #region PrivateProperties
        private List<Drug> CapsuleList
        {
            get { return DrugList.Where(e => e.DrugType == DrugType.Capsule).ToList(); }
        }

        private List<Drug> TabletList
        {
            get { return DrugList.Where(e => e.DrugType == DrugType.Tablet).ToList(); }
        }
        private List<Drug> SyrupList
        {
            get { return DrugList.Where(e => e.DrugType == DrugType.Syrup).ToList(); }
        }

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
            var prescription = new PrescriptionRow();
            //prescription.PropertyChanged += prescription_PropertyChanged;
            Prescriptions.Add(prescription);
            FilterPrescription();
            SelectedPrescription = prescription;
            SelectionChanged(PropName.PrescriptionList);
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
            SelectedPrescription.Delete();
            Visit.Prescriptions.Remove(SelectedPrescription.Prescription);
            FilterPrescription();
            RaiseDirty();
        }
        #endregion

        #region PrivateMethods

        private void UpdateDrugList(DrugType drugType)
        {
            if (drugType == LastDrugType) return;
            LastDrugType = drugType;
            switch (drugType)
            {
                case DrugType.Capsule:
                    SelectedTypeDrugList = CapsuleList;
                    break;
                case DrugType.Tablet:
                    SelectedTypeDrugList = TabletList;
                    break;
                case DrugType.Syrup:
                    SelectedTypeDrugList = SyrupList;
                    break;
                case DrugType.None:
                    SelectedTypeDrugList = new List<Drug>();
                    break;
            }
        }
        private void FilterPrescription()
        {
            PrescriptionFilterValue = new ListCollectionView(Prescriptions);
            PrescriptionFilterValue.Filter += NonDeletePredicate;
            SelectionChanged(PropName.PrescriptionFilter);
        }
        private bool NonDeletePredicate(object obj)
        {
            var p = obj as PrescriptionRow;
            return (p.Prescription.DbStatus != RepositoryStatus.Deleted);
        }
        #endregion

        public List<Prescription> GetList()
        {
            return Prescriptions.Select(prescriptionRow => prescriptionRow.Prescription).ToList();
        }
        void PrescriptionsValueCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PrescriptionList.Clear();
            foreach (var prescriptionRow in PrescriptionsValue)
            {
                PrescriptionList.Add(prescriptionRow.Prescription);
            }
            RaiseDirty();
        }
    }
}