/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.ReportScreen
{
    class ReportScreenFactory
    {
        public static IReportScreen GetReportScreen(Type type)
        {
            IReportScreen result = null;
            if (type == typeof(MedicalCertificateViewModel))
                result = new MedicalCertificateViewModel();

            return result;
        }

        public static ICollection<TypeRow> GetReportTypeRows()
        {
            var result = new ObservableCollection<TypeRow>()
            {
                new TypeRow()
                {
                    Name = Helper.Helper.GetAttribute<ScreenAttribute>(typeof (MedicalCertificateViewModel)).DisplayName,
                        Type = typeof (MedicalCertificateViewModel)
                }
               
            };
            return result;
        }
    }
}
