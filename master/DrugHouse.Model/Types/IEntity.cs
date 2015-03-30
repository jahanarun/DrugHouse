/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Enum;

namespace DrugHouse.Model.Types
{
    public interface IEntity
    {
        void CleanUp();
        RepositoryStatus DbStatus { get; set; }
    }
}
