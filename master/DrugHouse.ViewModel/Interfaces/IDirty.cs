/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;

namespace DrugHouse.ViewModel.Interfaces
{
    public interface IDirty
    {
        event EventHandler OnDirty;
        void RaiseDirty();
    }
}