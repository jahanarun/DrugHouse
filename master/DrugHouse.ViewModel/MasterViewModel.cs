/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using DrugHouse.Model;
using DrugHouse.Model.Types;
using DrugHouse.Shared;
using DrugHouse.ViewModel.AdminScreen;
using DrugHouse.ViewModel.Tabs;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace DrugHouse.ViewModel
{
    public class MasterViewModel : DrugHouseViewModelBase
    {
        #region Relay Commands
        public TabManager TabManager { get; set; }
        public RelayCommand<ITabViewModel> CloseTabCommand { get { return TabManager.CloseTabCommand; } }
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand<AdminScreenType> OpenAdminScreenCommand { get; private set; }
        public RelayCommand SearchPatientCommand { get; private set; }
        public RelayCommand NewPatientCommand { get; private set; }
        public RelayCommand OpenReportCommand { get; private set; }


        #endregion

        #region Constructors
        [PreferredConstructor]
        public MasterViewModel()//IMessageService messageService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            try
            {
                IsInitializing = true;
                UserIdValue = Environment.UserName.ToUpper().Trim();
                TabManager = new TabManager(this);
                TabManager.PropertyChanged += TabManagerOnPropertyChanged;

                NewPatientCommand = new RelayCommand(ExecuteNewPatientCommand, CanExecuteNewPatientCommand);
                SearchPatientCommand = new RelayCommand(() => TabManager.AddTab(new PatientMasterViewModel(this)),
                                                        () => true);
                ExitCommand = new RelayCommand(ExecuteExitCommand, CanExecuteExitCommand);
                OpenAdminScreenCommand = new RelayCommand<AdminScreenType>(ExecuteOpenAdminScreenCommand, CanExecuteOpenAdminScreenCommand);
                OpenReportCommand = new RelayCommand(ExecuteOpenRportScreenCommand, () => true);

                Title = ApplicationConstants.Product + " " + ApplicationConstants.Version;

            }
            catch (Exception ex)
            {
                MessageService.Error(ex.Message + ex.InnerException.Message, "DrugHouse");
            }


        }

        #endregion
        public class PropName
        {
            public const string HasMessage = "HasMessage";
            public const string SaveTabCommand = "SaveTabCommand";
            public const string IsInitializing = "IsInitializing";
            public const string SelectedTabChanged = "SelectedTabChanged";
        }

        public class RelayCommandPropName
        {
            public const string GenerateReportCommand = "GenerateReportCommand";
            public const string OpenPatientCommand = "OpenPatientCommand";
            public const string DeletePatientCommand = "DeletePatientCommand";
            public const string AddVisitCommand = "AddVisitCommand";
        }
        #region Properties

        public static class Globals
        {
            public static ICollection<Drug> Drugs(IDataAccess dataAccess = null)
            {
                List<Drug> result;
                if (dataAccess == null)
                    using (var data = DataAccess.GetInstance())
                    {
                        result = data.GetDrugs();
                    }
                else
                    result = dataAccess.GetDrugs();
                return result;
            }

            public static ICollection<SimpleEntity> Diagnoses(IDataAccess dataAccess = null)
            {
                List<SimpleEntity> result;
                if (dataAccess == null)
                    using (var data = DataAccess.GetInstance())
                    {
                        result = data.GetDiagnoses();
                    }
                else
                    result = dataAccess.GetDiagnoses();
                return result;
            }
            public static ICollection<SimpleEntity> DictionaryCollection(IDataAccess dataAccess = null)
            {
                List<SimpleEntity> result;
                if (dataAccess == null)
                    using (var data = DataAccess.GetInstance())
                    {
                        result = data.GetDictionaryItems();
                    }
                else
                    result = dataAccess.GetDictionaryItems();
                return result;
            }

            public static List<SimpleEntity> Locations(IDataAccess dataAccess = null)
            {
                List<SimpleEntity> result;
                if (dataAccess == null)
                    using (var data = DataAccess.GetInstance())
                    {
                        result = data.GetLocations();
                    }
                else
                    result = dataAccess.GetLocations();
                return result;
            }
        }
        private bool IsInitializingValue;
        public bool IsInitializing
        {
            get
            {
                return IsInitializingValue;
            }
            private set
            {
                IsInitializingValue = value; 
                SelectionChanged(PropName.IsInitializing);
            }
        }

        public string Title { get; private set; }

        private string UserIdValue;

        #endregion

        #region Public Members
        public async void OnViewLoaded()
        {
            try
            {
                var task = InitializeAsync();
                await task;
                TabManager.AddTab(task.Result);
                IsInitializing = false;
            }
            catch (Exception ex)
            {
                MessageService.Error(ex.Message, "DrugHouse");
                ExecuteExitCommand();
            }
        }

        public bool PrepareClosing()
        {
            return TabManager.TryClosingAllTabs();
        }

        public void OpenTab(ITabViewModel vm)
        {
            TabManager.OpenTabCommand.Execute(vm);
        }

        #endregion

        #region PrivateMembers

        void RefreshGlobals()
        {
            //using (var data = DataAccess.GetInstance())
            //{
            //    Globals.Drugs = data.GetDrugs();
            //    Globals.Diagnoses = data.GetDiagnoses();
            //    Globals.Locations = data.GetLocations();
            //}
        }

        /// <summary>
        /// Adds the default tab on start of the Application
        /// </summary>
        private TabViewModel GetDefaultTab()
        {
            return new PatientMasterViewModel(this);
        }
        private async Task<TabViewModel> InitializeAsync()
        {
            TabViewModel result = null;
            await Task.Run(() =>
            {
                RefreshGlobals();
                result = GetDefaultTab();
            });
            return result;
        }

        private void SetRelayCommands()
        {
            var type = TabManager.SelectedTab.GetType();

            if (type == typeof (ReportScreenViewModel))
            {
                GenerateReportCommand = ((ReportScreenViewModel) TabManager.SelectedTab).GenerateReportCommand;
            }
            else if (type == typeof (PatientMasterViewModel))
            {
                OpenPatientCommand = ((PatientMasterViewModel)TabManager.SelectedTab).OpenPatientCommand;
                DeletePatientCommand = ((PatientMasterViewModel) TabManager.SelectedTab).DeletePatientCommand;
            }
            else if (type == typeof(PatientViewModel))
            {
                AddVisitCommand = ((PatientViewModel)TabManager.SelectedTab).AddVisitCommand;
            }
        }
        #endregion


        #region Event Handlers

        private void TabManagerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case TabManager.PropName.SelectedTab:
                    if ((TabViewModel)TabManager.SelectedTab != null)
                    {
                        ((TabViewModel)TabManager.SelectedTab).PropertyChanged += OnSelecteTabPropertyChanged;
                        SetRelayCommands();
                        RaisePropertyChanged(PropName.SelectedTabChanged);
                    }
                    break;

            }
        }

        private void OnSelecteTabPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch(propertyChangedEventArgs.PropertyName)
            {
                case TabViewModel.EventPropName.RelayCommandUpdated:
                    SetRelayCommands();
                    break;
            }
        }

        #endregion
        #region Relay Command Methods


        private bool CanExecuteNewPatientCommand()
        {
            return true; 
        }
        private void ExecuteNewPatientCommand()
        {
            var tabViewModel = new PatientViewModel(this);
            TabManager.AddTab(tabViewModel);
        }

        private bool CanExecuteOpenAdminScreenCommand(AdminScreenType adminScreenType)
        {
            return true;

        }
        private void ExecuteOpenAdminScreenCommand(AdminScreenType adminScreenType)
        {
            var tabViewModel = new AdminScreenMasterViewModel(this);
            tabViewModel.OnSave += (sender, e) => RefreshGlobals();
            TabManager.AddTab(tabViewModel);
            tabViewModel.SelectAdminScreenBasedOnEnum(adminScreenType);
        }
        private void ExecuteOpenRportScreenCommand()
        {
            var tabViewModel = new ReportScreenViewModel(this);
            TabManager.AddTab(tabViewModel);
        }
        private bool CanExecuteExitCommand()
        {
            return true;
        }

        private void ExecuteExitCommand()
        {
            if (PrepareClosing())
                Application.Current.Shutdown();
        }

        #endregion

        #region Tabs' Relay Commands

        #region ReportScreen

        private RelayCommand GenerateReportCommandValue;
        public RelayCommand GenerateReportCommand
        {
            get { return GenerateReportCommandValue; }
            private set
            {
                GenerateReportCommandValue = value; 
                RaisePropertyChanged(RelayCommandPropName.GenerateReportCommand);
            }
        }
        #endregion

        #region PatientMaster

        private RelayCommand OpenPatientCommandValue;
        public RelayCommand OpenPatientCommand  
        {
            get { return OpenPatientCommandValue; }
            private set
            {
                OpenPatientCommandValue = value;
                RaisePropertyChanged(RelayCommandPropName.OpenPatientCommand);
            }
        }

        private RelayCommand DeletePatientCommandValue;
        public RelayCommand DeletePatientCommand
        {
            get { return DeletePatientCommandValue; }
            private set
            {
                DeletePatientCommandValue = value;
                RaisePropertyChanged(RelayCommandPropName.DeletePatientCommand);
            }
        }
        #endregion

        #region Patient
        private RelayCommand AddVisitCommandValue;
        public RelayCommand AddVisitCommand
        {
            get { return AddVisitCommandValue; }
            private set
            {
                AddVisitCommandValue = value;
                RaisePropertyChanged(RelayCommandPropName.AddVisitCommand);
            }
        }
        #endregion

        #endregion


    }
}
