/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.ViewModel.Interfaces;

namespace DrugHouse.ViewModel.AdminScreen
{
    public interface IAdminScreen: IDirty
    {
        string DisplayName { get; }
    }
}
