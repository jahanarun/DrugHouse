/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;

namespace DrugHouse.ViewModel.Tabs
{
    public class ScreenAttribute
        :Attribute
    {
        public ScreenAttribute(string name)
        {
            DisplayName = name;

        }
        public string DisplayName { get; private set; }
    }
}
