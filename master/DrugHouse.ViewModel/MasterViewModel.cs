﻿/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
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

        #region Properties

        public static class Globals
        {
            public static ICollection<Drug> Drugs { get; internal set; }
            public static ICollection<SimpleEntity> Diagnoses { get; internal set; }
            public static List<SimpleEntity> Locations { get; internal set; }
        }
        public class PropName
        {
            public const string SelectedTabViewModel = "SelectedTabViewModel";
            public const string HasMessage = "HasMessage";
            public const string SaveTabCommand = "SaveTabCommand";
            public const string IsInitializing = "IsInitializing";
        }

       public TabManager TabManager { get; private set; }
        public RelayCommand<ITabViewModel> CloseTabCommand { get { return TabManager.CloseTabCommand; } }
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand<AdminScreenType> OpenAdminScreenCommand { get; private set; }
        public RelayCommand SearchPatientCommand { get; private set; }
        public RelayCommand NewPatientCommand { get; private set; }
        public RelayCommand OpenReportCommand { get; private set; }

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
                AddDefaultTab();
                IsInitializing = false;
            }
            catch (Exception ex)
            {
                MessageService.Error(ex.Message, "DrugHouse");
                ExecuteExitCommand();
            }
        }

        #endregion

        #region PrivateMembers

        static void RefreshGlobals()
        {
            using (var data = DataAccess.GetInstance())
            {
                Globals.Drugs = data.GetDrugs();
                Globals.Diagnoses = data.GetDiagnoses();
                Globals.Locations = data.GetLocations();
            }
        }

        /// <summary>
        /// Adds the default tab on start of the Application
        /// </summary>
        private void AddDefaultTab()
        {
            var patientmaster = new PatientMasterViewModel(this);
            TabManager.AddTab(patientmaster);
        }
        private static async Task<bool> InitializeAsync()
        {
            var result = false;
            await Task.Run(() =>
            {
                RefreshGlobals();
                result = true;
            });
            return result;
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
            if (TabManager.TryClosingAllTabs())
                Application.Current.Shutdown();
        }

        #endregion


    }
}