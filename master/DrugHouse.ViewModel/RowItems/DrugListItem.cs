/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;

namespace DrugHouse.ViewModel.RowItems
{
    public class DrugListItem    
        : NotificationObject
    {
        private readonly Drug DrugValue;
        public DrugListItem(Drug drugValue)
        {
            DrugValue = drugValue;

        }


        public class PropName
        {
            public const string Remarks = "Remarks";
            public const string Name = "Name";
            public const string DrugType = "DrugType";
        }
        public Drug Drug { get { return DrugValue; } }

        public string Name
        {
            get
            { return Drug.Name; }
            set
            {
                Drug.Name = value;
                Drug.DbStatus = RepositoryStatus.Edited;
                OnPropertyChanged(PropName.Name);
            }
        }

        public DrugType DrugType
        {
            get { return DrugValue.DrugType; }
            set
            {
                Drug.DrugType = value;
                Drug.DbStatus = RepositoryStatus.Edited;
                OnPropertyChanged(PropName.DrugType);
            }
        }

        public string Remark
        {
            get { return Drug.Remarks; }
            set
            {
                Drug.Remarks = value;
                Drug.DbStatus = RepositoryStatus.Edited;
                OnPropertyChanged(PropName.Remarks);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
