/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.AdminScreen
{
    class AdminScreenFactory
    {
        public static IAdminScreen GetAdminScreen(AdminScreenMasterViewModel vm, Type type)
        {
            IAdminScreen result = null;
            if (type == typeof(DrugAdminScreenViewModel))
                result = new DrugAdminScreenViewModel(vm);
            else if (type == typeof(DropdownAdminScreenViewModel))
                result = new DropdownAdminScreenViewModel(vm);

            return result;
        }

        public static ICollection<TypeRow> GetAdminTypeRows()
        {
            var result = new ObservableCollection<TypeRow>()
            {
                new TypeRow()
                {
                    Name = Helper.Helper.GetAttribute<ScreenAttribute>(typeof (DrugAdminScreenViewModel)).DisplayName,
                        Type = typeof (DrugAdminScreenViewModel)
                },
                new TypeRow()
                {
                    Name = Helper.Helper.GetAttribute<ScreenAttribute>(typeof (DropdownAdminScreenViewModel)).DisplayName, 
                    Type = typeof (DropdownAdminScreenViewModel)
                }
            };
            return result;
        }
    }
}
