/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Linq;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.AdminScreen;
using DrugHouse.ViewModel.RowItems;

namespace DrugHouse.ViewModel.Tabs
{
    [TabAttribute("5CB08D84-FFE4-4E8A-B954-EE444A1CBE16")]
    public class AdminScreenMasterViewModel : TabViewModel
    {
        private TypeRow SelectedScreenTypeValue;

        public AdminScreenMasterViewModel(MasterViewModel masterViewModel)
            : base(masterViewModel)
        {
            AdminScreenCollection = AdminScreenFactory.GetAdminTypeRows();
        }

        #region Properties

        public class PropName
        {
            public const string SelectedAdminScreen = "SelectedAdminScreen";
            public const string SelectedAdminScreenType = "SelectedAdminScreenType";
            public const string TabName = "TabName";
        }

        public override string TabName
        {
            get { return "Administration - " + SelectedAdminScreen.DisplayName; }
        }
        public ICollection<TypeRow> AdminScreenCollection { get; private set; }

        public TypeRow SelectedScreenType
        {
            get { return SelectedScreenTypeValue; }
            set
            {
                SelectedScreenTypeValue = value;
                SelectedAdminScreen = GetAdminScreen(SelectedScreenType.Type);
                SelectionChanged(PropName.SelectedAdminScreenType);       
            }
        }

        private IAdminScreen SelectedAdminScreenValue;

        public IAdminScreen SelectedAdminScreen
        {
            get { return SelectedAdminScreenValue; }
            private set
            {
                SelectedAdminScreenValue = value;
                SelectionChanged(PropName.SelectedAdminScreen);
                SelectionChanged(PropName.TabName);
            }
        }

        #endregion

        #region Data Collections

        private List<Drug> DrugsValue;
        public List<Drug> Drugs
        {
            get { return DrugsValue ?? (DrugsValue = Data.GetDrugs()); }
        }
        private List<SimpleEntity> DiagnosesValue;
        public List<SimpleEntity> Diagnoses
        {
            get { return DiagnosesValue ?? (DiagnosesValue = Data.GetDiagnoses()); }
        }
        private List<SimpleEntity> LocationsValue;
        public List<SimpleEntity> Locations
        {
            get { return LocationsValue ?? (LocationsValue = Data.GetLocations()); }
        }
        private List<SimpleEntity> DictionaryValue;
        public List<SimpleEntity> Dictionary
        {
            get { return DictionaryValue ?? (DictionaryValue = Data.GetDictionaryItems()); }
        }

        #endregion

        public void SelectAdminScreenBasedOnEnum(AdminScreenType type)
        {
            switch (type)
            {
                case AdminScreenType.DrugAdmin:
                    SelectedScreenType =
                        AdminScreenCollection.First(row => row.Type == typeof (DrugAdminScreenViewModel));
                    break;
                case AdminScreenType.DropdownAdmin:
                    SelectedScreenType =
                        AdminScreenCollection.First(row => row.Type == typeof(DropdownAdminScreenViewModel));
                    break;
            }
        }

        private IAdminScreen GetAdminScreen(Type type)
        {
            var result = AdminScreenFactory.GetAdminScreen(this, type);

            if(result !=null)
                result.OnDirty += (sender, e) => RaiseDirty();
            return result;
        }

        protected override void SaveOperations()
        {
            Data.SaveIEnumerable(Drugs);
            Data.SaveIEnumerable(Diagnoses);
            Data.SaveIEnumerable(Locations);
            Data.SaveIEnumerable(Dictionary);
        }

        
    }
}
