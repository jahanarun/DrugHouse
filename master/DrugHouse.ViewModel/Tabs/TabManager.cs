/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Tabs
{
    public  class TabManager : DrugHouseViewModelBase
    {

        private readonly Dictionary<Guid, ITabViewModel> TabGuids = new Dictionary<Guid, ITabViewModel>();

        public TabManager(MasterViewModel master)
        {
            OpenTabCommand = new RelayCommand<ITabViewModel>(ExecuteOpenTab, CanExecuteOpenTabCommand);
            CloseTabCommand = new RelayCommand<ITabViewModel>(ExecuteCloseTabCommand, (tab) => true);
            SaveTabCommand = new RelayCommand(() => { }, () => false);
            MasterViewModel = master;
        }

        public RelayCommand SaveTabCommand { get; private set; }
        public RelayCommand<ITabViewModel> CloseTabCommand { get; private set; }
        public RelayCommand<ITabViewModel> OpenTabCommand { get; private set; }
        protected MasterViewModel MasterViewModel;

        public class PropName
        {
            public const string SelectedTab = "SelectedTab";
            public const string SaveTabCommand = "SaveTabCommand";
        }

        private  readonly ObservableCollection<ITabViewModel> TabItemsValue = new ObservableCollection<ITabViewModel>();
        public  IEnumerable<ITabViewModel> TabItems
        {
            get { return TabItemsValue; }
        }

        private ITabViewModel SelectedTabValue;
        public ITabViewModel SelectedTab
        {
            get { return SelectedTabValue; }
            set
            {
                if (SelectedTabValue == value) return;
                SelectedTabValue = value;
                SelectionChanged(PropName.SelectedTab);
                if (SelectedTabValue != null)
                {
                    SaveTabCommand = SelectedTab.SaveCommand;
                    SelectionChanged(PropName.SaveTabCommand);
                    SelectedTab.Refresh();
                }
            }
        }
        public  void AddTab(ITabViewModel tab)
        {
            if (IsTabGoodToAdd(tab))
            {
                TabItemsValue.Add(tab);
                TabGuids.Add(tab.Guid, tab);
                tab.OnSave += (o,e) => RefreshGuid();
            }
            SelectTabBasedOnGuid(tab.Guid);
        }

        public void RefreshGuid()
        {
            TabGuids.Clear();
            foreach (var tab in TabItems)
            {
                TabGuids.Add(tab.Guid, tab);
            }
        }

        public bool TryClosingAllTabs()
        {
            for (var i = TabItems.Count() - 1; i >= 0; i--)
            {
                var tabItem = TabItems.ElementAt(i);

                // If the tab still exist after calling CloseCommand, it means the Exit action is cancelled
                ExecuteCloseTabCommand(tabItem);
                if (TabItems.Contains(tabItem))
                    return false;
            }

            return true;
        }

        private void SelectTabBasedOnGuid(Guid guid)
        {
            if (!TabGuids.ContainsKey(guid)) return;

            ITabViewModel tab;
            TabGuids.TryGetValue(guid, out tab);
            SelectedTab = tab;
        }

        private bool IsTabGoodToAdd(ITabViewModel tab)
        {

            var tabGuid = tab.Guid;
            return !TabGuids.ContainsKey(tabGuid);
        }



        private bool CanExecuteOpenTabCommand(ITabViewModel tabViewModelViewModel)
        {
            return true;
        }
        private void ExecuteOpenTab(ITabViewModel tabViewModel)
        {
            AddTab(tabViewModel);
            //SelectedTab = tabViewModel;
        }

        private void ExecuteCloseTabCommand(ITabViewModel tab)
        {
            tab.PrepareToClose();
            SelectedTab = tab;
            if (!tab.IsReadyToClose) return;
            if (!TabItems.Contains(tab)) return;

            TabItemsValue.Remove(tab);
            TabGuids.Remove(tab.Guid);

            if (TabItems.Contains(tab))  //Close action was cancelled
                return;
            if (SelectedTab == null & TabItems.Any())
                SelectedTab = TabItems.Last();

        }

    }
}