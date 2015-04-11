/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dexuse.ICommand;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.Common;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.AdminScreen
{
    [Screen("Other fields")]
    public class DropdownAdminScreenViewModel 
        : AdminScreenBase
    {

        public ObservableCommand AddItemCommand { get; private set; }
        public RelayCommand RemoveItemCommand { get; private set; }

        private DropdownViewModel DropdownViewModel { get; set; }

        public DropdownAdminScreenViewModel(AdminScreenMasterViewModel vm) 
            : base(vm)
        {
            Initialize();
            AddItemCommand = new ExecutedCommand(ExecuteAddItem, () => SelectedDropdownItem != null);
            RemoveItemCommand = new RelayCommand(ExecuteRemoveItem, CanExecuteRemoveItem);
        }

        public class PropName
        {
            public const string SelectedDropdownItem = "SelectedDropdownItem";
            public const string SelectedEntityItem = "SelectedEntityItem";
            public const string SelectedEntityItemText = "SelectedEntityItemText";
            public const string EntityCollection = "EntityCollection";
            public const string IsSelectedEntityItemTextEnabled = "IsSelectedEntityItemTextEnabled";
            
        }

        public class AdminScreen
        {
            public const string Dictionary = "Dictionary";
            public const string Location = "Location";
            public const string Diagnosis = "Diagnosis";
        }

        private ObservableCollection<DropdownItem> EntityCollectionValue = new ObservableCollection<DropdownItem>();
        public ObservableCollection<DropdownItem> EntityCollection
        {
            get { return EntityCollectionValue; }
            private set
            {
                EntityCollectionValue = value;
                SelectionChanged(PropName.EntityCollection);
            }
        }

        private DropdownItem SelectedEntityItemValue;
        public DropdownItem SelectedEntityItem
        {
            get { return SelectedEntityItemValue; }
            set
            {
                SelectedEntityItemValue = value;
                SelectionChanged(PropName.SelectedEntityItem);
                SelectionChanged(PropName.SelectedEntityItemText);
                SelectionChanged(PropName.IsSelectedEntityItemTextEnabled);
            }
        }

        public bool IsSelectedEntityItemTextEnabled
        {
            get { return SelectedEntityItem != null ; }
        }

        public string SelectedEntityItemText
        {
            get { return SelectedEntityItem != null ? SelectedEntityItem.Name : string.Empty; }
            set
            {
                SelectedEntityItem.Name = value;
                SelectionChanged(PropName.SelectedEntityItemText);
                SelectionChanged(PropName.SelectedEntityItem);
            }
        }

        public List<string> DropdownSelectionItems { get; private set; }

        private string SelectedDropdownItemValue;
        public string SelectedDropdownItem
        {
            get { return SelectedDropdownItemValue; }
            set
            {
                SelectedDropdownItemValue = value; 
                SelectionChanged(PropName.SelectedDropdownItem);
                AddItemCommand.RaiseCanExecuteChanged();
                RefreshEntityCollection();
            }
        }

        private void RefreshEntityCollection()
        {
            EntityCollectionValue.Clear();
            switch (SelectedDropdownItem)
            {
                case AdminScreen.Diagnosis:
                    DropdownViewModel = new DropdownViewModel(AdminScreenMaster.Diagnoses, SimpleEntity.Markers.Diagnosis); 
                    break;
                case AdminScreen.Location:
                    DropdownViewModel = new DropdownViewModel(AdminScreenMaster.Locations, SimpleEntity.Markers.Location);
                    break;
                case AdminScreen.Dictionary:
                    DropdownViewModel = new DropdownViewModel(AdminScreenMaster.Dictionary, SimpleEntity.Markers.Dictionary);
                    break;
            }

            EntityCollection = DropdownViewModel.Entities;
            DropdownViewModel.OnDirty += (o, e) => RaiseDirty();
        }

        private void Initialize()
        {
            var result = new List<string>
            {
                AdminScreen.Diagnosis,
                AdminScreen.Location,
                AdminScreen.Dictionary
            };
            DropdownSelectionItems = result.OrderBy(s => s).ToList();
        }

        private void ExecuteAddItem()
        {
            SelectedEntityItem = DropdownViewModel.AddEntity();

        }
        private void ExecuteRemoveItem()
        {
            DropdownViewModel.RemoveEntity(SelectedEntityItem);
        }
        private bool CanExecuteRemoveItem()
        {
            return SelectedEntityItem != null;
        }
    }
}