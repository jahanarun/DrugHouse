/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Data.Entity;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Repositories
{
    public interface IRepo<T>
        where T:ModelBase
    {
        DbSet<TType> Select<TType>() where TType : ModelBase;
        T Insert(T item);
        T Update(T item);
        T Delete(T item);
        void Reset(T item);
        void SaveChanges();
    }
}
