/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using DrugHouse.Model.Enum;

namespace DrugHouse.Model.Types
{
    public abstract class ModelBase :ICloneable, IEntity
    {
        private RepositoryStatus DbStatusValue;
        [NotMapped]
        public RepositoryStatus DbStatus
        {
            get { return DbStatusValue; }
            set
            {
                if (DbStatusValue == RepositoryStatus.New)
                {
                    if (value == RepositoryStatus.Deleted)
                        DbStatusValue = RepositoryStatus.Disregard;
                }
                else
                    DbStatusValue = value;
            }
        }
        public void ResetDbStatus()
        { DbStatusValue=RepositoryStatus.None;}

        public abstract object Clone();

        public virtual void CleanUp()
        { }
    }
}
