/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;

namespace DrugHouse.ViewModel.Tabs
{
    class TabAttribute
        :Attribute
    {

        public TabAttribute(string guid, bool multipleInstance = false)
        {
            Guid.TryParse(guid, out GuidValue);
            IsMultipleInstanceAllowed = multipleInstance;
        }

        private Guid GuidValue;
        public Guid Guid
        {
            get { return GuidValue; }
            private set { GuidValue = value; }
        }

        public bool IsMultipleInstanceAllowed { get; private set; }
    }
}
