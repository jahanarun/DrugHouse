/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using DrugHouse.Model.Types;

namespace DrugHouse.Model
{
    public interface IDataAccess:IDisposable
    {
        string GetEnvironment();                                        
        string GetDatabaseServer();                                     
        //string GetApplicationConnectionStringEncrypted();

        #region PatientRepository

        void SaveChanges();
        Patient SavePatient(Patient patient);
        ICollection<Patient> GetAllPatientPartialDetails();
        List<Patient> GetAllPatients();
        List<Patient> GetAllPatients(Func<Patient, bool> predicate);
        Patient GetPatientDetails(long id);
        bool TryGetPatientDetails(long id, out Patient patient);

        #endregion

        #region POCO Repositories

        List<Drug> GetDrugs();
        void SaveIEnumerable(IEnumerable<ModelBase> items);
        List<SimpleEntity> GetDiagnoses();                 
        List<Drug> GetDrugs(Func<Drug, bool> condition);   
        List<SimpleEntity> GetLocations();

        #endregion
    }
}
