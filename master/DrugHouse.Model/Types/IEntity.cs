/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Enum;

namespace DrugHouse.Model.Types
{
    public interface IEntity
    {
        void SetValuesBeforeDbSave();
        RepositoryStatus DbStatus { get; set; }
    }
}
