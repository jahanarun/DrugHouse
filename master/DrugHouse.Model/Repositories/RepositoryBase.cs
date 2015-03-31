/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Data.Entity;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Repositories
{
    internal abstract class RepositoryBase<T>
        : IRepo<T> where T : ModelBase
    {
        private readonly DrugHouseContext DbContext;
        protected RepositoryBase(DbContext context)
        {
            DbContext = (DrugHouseContext)context;
        }
        public T Insert(T item)
        {
            DbContext.Entry(item).State = EntityState.Added;
            return item;
        }

        public T Update(T item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
            return item;
        }

        public T Delete(T item)
        {

            DbContext.Entry(item).State = EntityState.Deleted;
            return item;
        }

        public void Reset(T item)
        {
            DbContext.Entry(item).State = EntityState.Unchanged;
        }

        public DbSet<TType> Select<TType>() where TType : ModelBase
        {
            return DbContext.Set<TType>();
        }

        protected T SetState(T item)
        {
            switch (item.DbStatus)
            {
                case RepositoryStatus.New:
                    item = Insert(item);
                    break;
                case RepositoryStatus.Edited:
                    item = Update(item);
                    break;
                case RepositoryStatus.Deleted:
                    item = Delete(item);
                    break;
                default:
                    Reset(item);
                    break;
            }
            return item;
        }

        public void SaveChanges()
        {
            SetChangeTrackerEntityState();

            DbContext.SaveChanges();

            ResetDbStatus();
        }

        public void SaveIEnumerable(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                SetState(item);
            }
            DbContext.SaveChanges();

            ResetDbStatus();
        }

        /// <summary>
        /// Saves the entity and returns the updated value from db.
        /// This is used when the db inserts identity column value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected T SaveChaangesForEntity(T item) 
        {
            SetChangeTrackerEntityState();

            item = SetState(item);

            DbContext.SaveChanges();

            ResetDbStatus();

            return item;
        }

        private void SetChangeTrackerEntityState()
        {
            foreach (var entity in DbContext.ChangeTracker.Entries())
            {
                SetState((T)entity.Entity);
            }
        }
        private void ResetDbStatus()
        {
            foreach (var entity in DbContext.ChangeTracker.Entries())
                ((ModelBase)entity.Entity).ResetDbStatus();            
        }
    }
}
