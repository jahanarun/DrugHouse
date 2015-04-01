/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using DrugHouse.Model.Exceptions;
using DrugHouse.Model.Properties;
using DrugHouse.Model.Repositories;
using DrugHouse.Model.Types;

namespace DrugHouse.Model
{
    public class DataAccess : IDataAccess
    {
        #region "Constructors"

        private readonly DbContext Context;
        private readonly DrugHouseRepository DrugHouseRepository;
        public DataAccess()
        {
            Context = DrugHouseContextFactory.Instance.Create();
            DrugHouseRepository = new DrugHouseRepository(Context);
        }
        #endregion

        #region Properties

        public static IDataAccess GetInstance()
        {
            return new DataAccess(); 
        }

        #endregion

        #region ApplicationFunction
        public string GetEnvironment()
        {
            try
            {
                return Settings.Default.EnvironmentName;
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public string GetDatabaseServer()
        {
            try
            {
                return Settings.Default.DatabaseServer;
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public static string GetApplicationConnectionString()
        {
            try
            {
                var encryptedString = Settings.Default.ApplicationConnectionStringEncrypted;

                return encryptedString.Length < 1 
                    ? Settings.Default.ApplicationConnectionString 
                    : Decoder.Decrypt(Settings.Default.ApplicationConnectionStringEncrypted);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public void SaveChanges()
        {
            try
            {
                DrugHouseRepository.SaveTrackedChanges();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        #endregion
                                     
        #region Insert/Update


        public Patient SavePatient(Patient patient)
        {
            try
            {
                return (Patient) DrugHouseRepository.SaveEntity(patient);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }

        }
        #endregion                     

        #region Select

        public List<Drug> GetDrugs()
        {
            try
            {
                return DrugHouseRepository.GetDrugs();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }
        public List<Drug> GetDrugs(Func<Drug, bool> condition)
        {
            try
            {
                return DrugHouseRepository.GetDrugs(condition);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public List<SimpleEntity> GetLocations()
        {
            try
            {
                return DrugHouseRepository.GetLocations();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public void SaveIEnumerable(IEnumerable<ModelBase> items)
        {
            try
            {
                DrugHouseRepository.SaveIEnumerable(items);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                if (innerEx.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                    throw new DeleteConstraintException("", innerEx);
                throw new DataException(innerEx.Message, ex);
            }
        }

        public List<SimpleEntity> GetDiagnoses()
        {
            try
            {
                return DrugHouseRepository.GetAllDiagnoses();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public List<Patient> GetAllPatients()
        {
            try
            {
                return DrugHouseRepository.GetPatients();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }
        public List<Patient> GetAllPatients(Func<Patient, bool> predicate)
        {
            try
            {
                return DrugHouseRepository.GetPatients(predicate);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }
        public ICollection<Patient> GetAllPatientPartialDetails()
        {
            try
            {
                return DrugHouseRepository.GetAllPatientPartialDetails();
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public Patient GetPatientDetails(long id)
        {
            try
            {
                return DrugHouseRepository.GetPatientDetails(id);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }

        public bool TryGetPatientDetails(long id, out Patient patient)
        {
            try
            {
                return DrugHouseRepository.TryGetPatientDetails(id, out patient);
            }
            catch (Exception ex)
            {
                var innerEx = ex;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;
                throw new DataException(innerEx.Message, ex);
            }
        }
        #endregion

        public void Dispose()
        {
             Context.Dispose();
        }
    }
}
