/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Reports;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.ReportScreen
{
    public abstract class ReportViewModelBase : DrugHouseViewModelBase, IReportScreen
    {
        protected ModelBase Entity;
        protected Report Report = new Report();



        public void SetEntity(ModelBase item)
        {
            Entity = item;
        }
        public abstract void GenerateReport();
        public abstract void GenerateReportAsync();
    }
}
