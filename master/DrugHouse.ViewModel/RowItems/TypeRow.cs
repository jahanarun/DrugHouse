/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;

namespace DrugHouse.ViewModel.RowItems
{
    public class TypeRow
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}