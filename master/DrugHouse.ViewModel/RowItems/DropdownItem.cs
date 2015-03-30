/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.RowItems
{
    public class DropdownItem
        : DrugHouseViewModelBase
    {
        private readonly ISimpleEntity EntityValue;
        public DropdownItem(ISimpleEntity entity)
        {
            EntityValue = entity;
        }                        

        public class PropName
        {
            public const string Id = "Id";
            public const string Name = "Name";
        }
        public ISimpleEntity Entity { get { return EntityValue; } }

        public string Name
        {
            get
            { return Entity.Name; }
            set
            {
                Entity.Name = value;
                Entity.DbStatus = RepositoryStatus.Edited;
                RaisePropertyChanged(PropName.Name);
            }
        }

        public long Id
        {
            get { return Entity.Id; }
            set
            {
                Entity.Id = value;
                Entity.DbStatus = RepositoryStatus.Edited;
                RaisePropertyChanged(PropName.Id);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
