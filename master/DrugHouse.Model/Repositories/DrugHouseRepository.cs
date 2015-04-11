/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Repositories
{
    internal class DrugHouseRepository : RepositoryBase<ModelBase> 
    {
        public DrugHouseRepository(DbContext context)
            : base(context)
        {
            
        }

        #region Selectors

        public List<Patient> GetPatients()
        {
            return Select<Patient>().Include(p => p.Location).ToList();
        }

        public List<Patient> GetPatients(Func<Patient,bool> predicate )
        {
            return GetPatients().Where(predicate).ToList();
        }
        public List<Drug> GetDrugs()
        {
            return Select<Drug>().ToList();
        }

        public List<Drug> GetDrugs(Func<Drug, bool> condition)
        {
            return GetDrugs().Where(condition).ToList();
        }
        public List<SimpleEntity> GetLocations()
        {
            var result = Select<SimpleEntity>().Where(x => x.Marker == SimpleEntity.Markers.Location)
                                                .ToList();
            result.Insert(0, SimpleEntity.Empty);
            return result;
        }

        public List<SimpleEntity> GetDictionaryItems()
        {
            var result = Select<SimpleEntity>().Where(x => x.Marker == SimpleEntity.Markers.Dictionary)
                                                .ToList();
            result.Insert(0, SimpleEntity.Empty);
            return result;
        }
        public Patient GetPatientDetails(long id)
        {
            var result = Select<Patient>().Include(p => p.Visits.Select(v => v.Prescriptions.Select(u => u.Drug)))
                .Include(p => p.Visits.Select(v => v.PrimaryDiagnosis))
                .Include(p => p.Visits.Select(v => v.SecondaryDiagnosis))
                .Include(p => p.Location)
                .Single(t => t.Id == id);
            foreach (var visit in result.Visits)
            {
                if (visit.PrimaryDiagnosis == null)
                    visit.PrimaryDiagnosis = SimpleEntity.Empty;
                if (visit.SecondaryDiagnosis == null)
                    visit.SecondaryDiagnosis = SimpleEntity.Empty;

            }
            return result;
        }

        public bool TryGetPatientDetails(long id, out Patient patient)
        {
            var result = Select<Patient>().Find(id);
            if (result == null)
            {
                patient = null;
                return false;               
            }
            patient = GetPatientDetails(id);
            return true;
        }

        public ICollection<Patient> GetAllPatientPartialDetails()
        {
            return Select<Patient>().ToList();
        }
        public List<SimpleEntity> GetAllDiagnoses()
        {
            var result = Select<SimpleEntity>().Where(x => x.Marker == SimpleEntity.Markers.Diagnosis)
                                                 .ToList();
            result.Insert(0, SimpleEntity.Empty);
            return result;
        }


        #endregion

        #region Insertors / Updators

        public ModelBase SaveEntity(ModelBase item)
        {
            item = SaveUntrackedChanges(item);

            return item;
        }

        #endregion

    }
}
