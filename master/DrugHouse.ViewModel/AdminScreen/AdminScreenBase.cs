/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.AdminScreen
{
    public abstract class AdminScreenBase
        : DrugHouseViewModelBase, IAdminScreen
    {
        protected AdminScreenMasterViewModel AdminScreenMaster;

        protected AdminScreenBase(AdminScreenMasterViewModel vm)
        {
            AdminScreenMaster = vm;
        }
        public virtual string DisplayName { get
        {
            return Helper.Helper.GetAttribute<ScreenAttribute>(this.GetType()).DisplayName;
        } }
    }
}