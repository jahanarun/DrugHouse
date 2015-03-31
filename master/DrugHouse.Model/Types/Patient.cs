/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrugHouse.Model.Enum;

namespace DrugHouse.Model.Types
{
    public class Patient : Person
    {
        #region Constructors

        public Patient()
            :base(Person.Empty)
        {
            Initialize();
        }

        public Patient(long id)
            : this()
        {
            Id = id;
            Initialize();
        }

        public Patient(Person person)
            : base(person)
        {
            Initialize();
        }
        #endregion

        public new static readonly Patient Empty = new PatientEmpty();

        #region Properties

        public virtual ICollection<PatientVisit> Visits { get; set; }
        public virtual ICollection<PatientAdmitance> Admitances { get; set; }

        private ICollection<ICase> CasesValue;
        public ICollection<ICase> Cases
        {
            get
            {
                if (CasesValue == null)
                    InitializeCases();
                return CasesValue ;
            }
        }
        #endregion

        private void InitializeCases()
        {
            CasesValue = new Collection<ICase>();
            foreach (var visit in Visits)
            {
                CasesValue.Add(visit);
            }
            foreach (var @case in Admitances)
            {
                CasesValue.Add(@case);
            }
        }

        //[Column("ReferredMedicalPractitioner", TypeName="int")]
        //public virtual MedicalPractitioner ReferredMedicalPractitioner { get; set; }


        private class PatientEmpty : Patient
        {
            public PatientEmpty()
            {
               Initialize();
            }
        }


        private void Initialize()
        {
            Category = PersonType.Patient;
            DbStatus = RepositoryStatus.None;
            Visits = new Collection<PatientVisit>();
            Admitances = new Collection<PatientAdmitance>(); 
        }
        #region Public Methods
        //public ICase AddVisit()
        //{
        //    var result = new PatientVisit(this) {DbStatus = RepositoryStatus.New};
        //    DbStatus=RepositoryStatus.Edited;
        //    Visits.Add(result);
        //    return result;
        //}
        public ICase AddCase(Type t)
        {
            ICase result = null;
            if (t == typeof (PatientVisit))
            {
                result = new PatientVisit(this) {DbStatus = RepositoryStatus.New};
                Visits.Add((PatientVisit) result);
            }
            else if (t == typeof (PatientAdmitance))
            {
                result = new PatientAdmitance(this) {DbStatus = RepositoryStatus.New};
                Admitances.Add((PatientAdmitance) result);
            }

            DbStatus = RepositoryStatus.Edited;
            Cases.Add(result);

            return result;
        }
        public void RemoveCase(ICase @case)
        {
            if(@case.GetType() == typeof(PatientVisit))
                Visits.Remove((PatientVisit) @case);
            else if (@case.GetType() == typeof (PatientAdmitance))
                Admitances.Remove((PatientAdmitance) @case);
            Cases.Remove(@case);
            @case.DbStatus=RepositoryStatus.Deleted;
            DbStatus = RepositoryStatus.Edited;
        }

        #endregion

        #region Overrided Methods
        public override object Clone()
        {

            var result = new Patient(this) {Visits = new Collection<PatientVisit>()};

            foreach (var visit in Visits)
            {
                result.Visits.Add((PatientVisit) visit.Clone()); 
            }


            return result;
        }

        public override void SetValuesBeforeDbSave()
        {
            foreach (var visit in Visits)
            {
                visit.SetValuesBeforeDbSave();
            }
            foreach (var admitance in Admitances)
            {
                admitance.SetValuesBeforeDbSave();
            }
        }

        public override void SetValuesAfterDbSave()
        {
            foreach (var visit in Visits)
            {
                visit.SetValuesAfterDbSave();
            }
            foreach (var admitance in Admitances)
            {
                admitance.SetValuesAfterDbSave();
            }
        }

        #endregion
    }

}
