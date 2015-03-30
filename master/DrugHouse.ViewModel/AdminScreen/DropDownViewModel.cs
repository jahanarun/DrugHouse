/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.ViewModel.RowItems;
using DrugHouse.ViewModel.Tabs;

namespace DrugHouse.ViewModel.AdminScreen
{
    public class DropdownViewModel : DrugHouseViewModelBase
    {
        public DropdownViewModel(List<SimpleEntity> entitites, string marker)
        {
            Marker = marker;
            InternaList = entitites;
            RefreshEntities();
        }

        protected List<SimpleEntity> InternaList;
        public ObservableCollection<DropdownItem> Entities = new ObservableCollection<DropdownItem>();
        private readonly string Marker;

        public DropdownItem AddEntity()
        {
            var entity = new SimpleEntity(Marker) { Name = "New...", DbStatus = RepositoryStatus.New };
            InternaList.Add(entity);
            var dropdownItem = new DropdownItem(entity);
            Entities.Insert(0, dropdownItem);
            dropdownItem.OnDirty += (o, e) => RaiseDirty();
            RaiseDirty();
            return dropdownItem;
        }

        public void RemoveEntity(DropdownItem entiity)
        {
            var diagnosis = entiity.Entity;
            diagnosis.DbStatus = RepositoryStatus.Deleted;
            Entities.Remove(entiity);
            RaiseDirty();
        }

        protected void RefreshEntities()
        {
            foreach (var dropItem in InternaList.Where(x => x.GetType() == typeof(SimpleEntity)).Select(entity => new DropdownItem(entity)))
            {
                dropItem.OnDirty += (o, e) => RaiseDirty();
                Entities.Add(dropItem);
            }
            foreach (var entity in Entities)
            {
                entity.OnDirty += (o, e) => RaiseDirty();
            }
        }

    }
}
