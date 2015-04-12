/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dexuse.ICommand;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;
using DrugHouse.ViewModel.Common;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.AdminScreen
{
    [Screen("Drugs")]
    public class DrugAdminScreenViewModel : AdminScreenBase
    {
        public ObservableCommand AddDrugCommand { get; private set; }
        public RelayCommand RemoveDrugCommand { get; private set; }
        public RelayCommand FilterDrugCommand { get; private set; }

        #region Constructor

        private List<Drug> Drugs { get { return AdminScreenMaster.Drugs; } }

        internal DrugAdminScreenViewModel(AdminScreenMasterViewModel vm)
            :base(vm)
        {
            AddDrugCommand = new ExecutedCommand(ExecuteAddDrugCommand, CanExecuteAddDrugCommand);
            RemoveDrugCommand = new RelayCommand(ExecuteRemoveDrugCommand, CanExecuteRemoveDrugCommand);
            FilterDrugCommand = new RelayCommand(ExecuteFilterDrug, () => true);
            Initialize();
        }


        #endregion

        #region Properties

        public class PropName
        {
            public const string DrugItemsList = "DrugItemsList";
            public const string IsSelected = "IsSelected";
            public const string DrugType = "DrugType";
            public const string Remark = "Remark";
            public const string DrugName = "DrugName";
            public const string SelectedDrugItem = "SelectedDrugItem";
            public const string SearchString = "SearchString";
        }

        public ObservableCollection<DrugListItem> DrugItemsList { get; private set; }

        public bool IsSelected { get { return SelectedDrugItem != null; } }

        private DrugListItem SelectedDrugItemValue;
        public DrugListItem SelectedDrugItem
        {
            get { return SelectedDrugItemValue; }
            set
            {
                SelectedDrugItemValue = value;
                SelectionChanged(PropName.SelectedDrugItem);
                SelectionChanged(PropName.DrugName);
                SelectionChanged(PropName.DrugType);
                SelectionChanged(PropName.Remark);
                SelectionChanged(PropName.IsSelected);
            }
        }

        private string SearchStringValue = string.Empty;
        public string SearchString
        {
            get { return SearchStringValue; }
            set
            {
                SearchStringValue = value;
                FilterDrugCommand.Execute(null);
                SelectionChanged(PropName.SearchString);
            }
        }

        private bool IsTabletValue;

        public bool IsTablet
        {
            get { return IsTabletValue; }
            set
            {
                IsTabletValue = value;
                FilterDrugCommand.Execute(null);
            }
        }

        private bool IsCapsuleValue;

        public bool IsCapsule
        {
            get { return IsCapsuleValue; }
            set
            {
                IsCapsuleValue = value;
                FilterDrugCommand.Execute(null);
            }
        }

        private bool IsInjectionValue;

        public bool IsInjection
        {
            get { return IsInjectionValue; }
            set
            {
                IsInjectionValue = value;
                FilterDrugCommand.Execute(null);
            }
        }
        private bool IsSyrupValue;

        public bool IsSyrup
        {
            get { return IsSyrupValue; }
            set
            {
                IsSyrupValue = value;
                FilterDrugCommand.Execute(null);
            }
        }

        #endregion

        #region DrugItem

        public string DrugName
        {
            get
            {
                return SelectedDrugItem == null ? string.Empty : SelectedDrugItem.Name;
            }
            set
            {
                if (SelectedDrugItem == null)
                    return;
                SelectedDrugItem.Name = value; 
                RaisePropertyChanged(PropName.DrugName);
            }
        }

        public string Remark
        {
            get
            {
                return SelectedDrugItem == null ? string.Empty : SelectedDrugItem.Remark;
            }
            set
            {
                if (SelectedDrugItem == null)
                    return;
                SelectedDrugItem.Remark = value;
                RaisePropertyChanged(PropName.Remark);
            }
        }

        public DrugType DrugType
        {
            get
            {
                return SelectedDrugItem == null ? DrugType.None : SelectedDrugItem.DrugType;
            }
            set
            {
                if (SelectedDrugItem == null)
                    return;
                if (SelectedDrugItem.DrugType == value)
                    return;
                SelectedDrugItem.DrugType = value;
                RaisePropertyChanged(PropName.DrugType);
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            FilterDrugCommand.Execute(null);
        }

        private void AddDrug()
        {
            var drug = new Drug {Name = "New Drug...", DbStatus = RepositoryStatus.New};
            Drugs.Add(drug);
            var drugItem = new DrugListItem(drug);
            DrugItemsList.Insert(0, drugItem);
            SelectedDrugItem = drugItem;
            RaiseDirty();
        }
        private void RemoveDrug()
        {
            var drug = SelectedDrugItem.Drug;
            drug.DbStatus = RepositoryStatus.Deleted;
            var temp = SelectedDrugItem;
            SelectedDrugItem = DrugItemsList.First();
            DrugItemsList.Remove(temp);
            RaiseDirty();
        }
        private void FilterDrug(Func<Drug, bool> condition)
        {
            var result = Drugs.Where(condition).ToList();

            // Inefficient coding - to be replaced after understanding dynamic LINQ
            if (IsCapsule && !IsSyrup && !IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule).ToList();
            else if (!IsCapsule && IsSyrup && !IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Syrup).ToList();
            else if (!IsCapsule && !IsSyrup && IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Tablet).ToList();
            else if (!IsCapsule && !IsSyrup && !IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Injection).ToList();

            else if (IsCapsule && IsSyrup && !IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule || d.DrugType == DrugType.Syrup).ToList();
            else if (IsCapsule && !IsSyrup && IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule || d.DrugType == DrugType.Tablet).ToList();
            else if (IsCapsule && !IsSyrup && !IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule || d.DrugType == DrugType.Injection).ToList();

            else if (!IsCapsule && IsSyrup && IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Tablet || d.DrugType == DrugType.Syrup).ToList();
            else if (!IsCapsule && IsSyrup && !IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Syrup || d.DrugType == DrugType.Injection).ToList();

            else if (!IsCapsule && !IsSyrup && IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Tablet || d.DrugType == DrugType.Injection).ToList();

            else if (!IsCapsule && IsSyrup && IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Syrup 
                                        || d.DrugType == DrugType.Injection 
                                        || d.DrugType == DrugType.Tablet).ToList();
            else if (IsCapsule && !IsSyrup && IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule 
                                        || d.DrugType == DrugType.Tablet 
                                        || d.DrugType == DrugType.Injection).ToList();
            else if (IsCapsule && IsSyrup && !IsTablet && IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Syrup 
                                        || d.DrugType == DrugType.Injection 
                                        || d.DrugType == DrugType.Capsule).ToList();
            else if (IsCapsule && IsSyrup && IsTablet && !IsInjection)
                result = result.Where(d => d.DrugType == DrugType.Capsule 
                                        || d.DrugType == DrugType.Syrup 
                                        || d.DrugType == DrugType.Tablet).ToList();


            var finalResult = result.Select(d => new DrugListItem(d)).ToList();

            DrugItemsList = new ObservableCollection<DrugListItem>(finalResult);
            SelectionChanged(PropName.DrugItemsList);
        }

        #endregion

        #region Relay Command Methods

        private bool CanExecuteRemoveDrugCommand()
        {
            return SelectedDrugItem != null;
        }

        private void ExecuteRemoveDrugCommand()
        {
            RemoveDrug();
        }

        private bool CanExecuteAddDrugCommand()
        {
            return true;
        }

        private void ExecuteAddDrugCommand()
        {
            AddDrug();
        }
        private void ExecuteFilterDrug()
        {
            Func<Drug, bool> condition;
            var searchText = SearchString.ToUpper();

            if (SearchString != string.Empty)
            {
                condition = (x => x.Name.ToUpper().Contains(searchText) ||
                                  x.Remarks.ToUpper().Contains(searchText) 
                                  );
            }
            else
                condition = (p) => true;

            FilterDrug(condition);
        }  
        #endregion

    }
}