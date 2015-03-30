/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Shared.Enumerations;

namespace DrugHouse.Model.Types
{
    public class DrugEmpty : Drug
    {
        public DrugEmpty()
        {
            DrugId = (long)decimal.Zero;
            Name = string.Empty;
            DrugType = DrugType.None;
            Remarks = string.Empty;
        }
    }
}