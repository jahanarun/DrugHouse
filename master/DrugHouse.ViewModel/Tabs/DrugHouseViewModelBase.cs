/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using DrugHouse.ViewModel.Interfaces;
using GalaSoft.MvvmLight;

namespace DrugHouse.ViewModel.Tabs
{

    public class DrugHouseViewModelBase : ViewModelBase, IDirty
    {
        public static IMessageService MessageService;

        public class PropNameBase
        {
            public const string HasMessage = "HasMessage";
        }
        public string Message { get; set; }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            RaiseDirty();
        }

        protected void SelectionChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
        }


        public event EventHandler OnDirty;

        public virtual void RaiseDirty()
        {
            if (OnDirty != null) OnDirty(this, EventArgs.Empty);
        }

        protected void AttachDirtyState(DrugHouseViewModelBase vm)
        {
            vm.OnDirty += (sender, e) => RaiseDirty(); 
        }
    }
}