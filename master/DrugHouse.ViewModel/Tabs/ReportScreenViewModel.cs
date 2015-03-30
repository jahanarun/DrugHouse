/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using DrugHouse.ViewModel.ReportScreen;
using DrugHouse.ViewModel.RowItems;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Tabs
{
    [TabAttribute("9DC11748-A992-4758-AA24-2862B75D30CB")]
    public class ReportScreenViewModel:TabViewModel
    {
        public ReportScreenViewModel(MasterViewModel vm) : base(vm)
        {
            ReportScreenCollection = ReportScreenFactory.GetReportTypeRows();
            GenerateReportCommand = new RelayCommand(ExecuteGenerateCommand, CanExecuteGenerateCommand);
        }

        public class PropName
        {
            public const string SelectedReport = "SelectedReport";
            public const string TabName = "TabName";
        }

        public RelayCommand GenerateReportCommand { get; private set; }
        public ICollection<TypeRow> ReportScreenCollection { get; private set; }

        public TypeRow SelectedReportScreenType
        {
            get { return SelectedReportScreenTypeValue; }
            set
            {
                SelectedReportScreenTypeValue = value;
                SelectedReport = GetReportScreen(SelectedReportScreenType.Type);
                SelectionChanged(AdminScreenMasterViewModel.PropName.SelectedAdminScreenType);
            }
        }

        private TypeRow SelectedReportScreenTypeValue;
        private IReportScreen SelectedReportValue;

        public IReportScreen SelectedReport
        {
            get { return SelectedReportValue; }
            private set
            {
                SelectedReportValue = value;
                SelectionChanged(PropName.SelectedReport);
                SelectionChanged(PropName.TabName);
            }
        }



        public override string TabName
        {
            get { return "Report"; }
        }

        private static IReportScreen GetReportScreen(Type type)
        {
            var result = ReportScreenFactory.GetReportScreen(type);
            return result;
        }
        private bool CanExecuteGenerateCommand()
        {
            return SelectedReport != null;
        }

        private void ExecuteGenerateCommand()
        {
            SelectedReport.GenerateReportAsync();
        }

    }
}