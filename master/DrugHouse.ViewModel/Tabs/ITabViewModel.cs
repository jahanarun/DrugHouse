/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using GalaSoft.MvvmLight.Command;

namespace DrugHouse.ViewModel.Tabs
{
    public interface ITabViewModel
    {
        event EventHandler OnSave;
        void Refresh();
        void PrepareToClose();
        RelayCommand SaveCommand { get; }
        string TabName { get; }
        bool IsReadyToClose { get; }
        Guid Guid { get; }
        bool IsMultipleTabsAllowed { get; }

    }
}