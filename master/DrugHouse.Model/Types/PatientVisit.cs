/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DrugHouse.Model.PatientDetails;

namespace DrugHouse.Model.Types
{
    public class PatientVisit : ModelBase, ICase
    {
        private PatientVisit() { }
        public PatientVisit(Patient p)
        {
            Patient = p;
            Prescriptions = new List<Prescription>();
            Date = DateTime.Now;
            Status=new PatientStatus(){Status ="Test STatus"};
            PrimaryDiagnosis = SimpleEntity.Empty;
            SecondaryDiagnosis = SimpleEntity.Empty;
            RestDuration = string.Empty;
            //Diagnosis = new Diagnosis() {Id = 14};
            //Fee = new PatientFee(){DoctorFee = 10, DrugFee = 10};
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public virtual SimpleEntity PrimaryDiagnosis { get; set; }
        public virtual SimpleEntity SecondaryDiagnosis { get; set; }
        public virtual PatientStatus Status { get; set; }
        public virtual Patient Patient { get; set; }
        public string Complaints { get; set; }
        public string History { get; set; }
        public string Observation { get; set; }
        public string Treatment { get; set; }
        public decimal DoctorFee { get; set; }
        public decimal DrugFee { get; set; }
        public string RestDuration { get; set; }
        public virtual IList<Prescription> Prescriptions { get; set; }

        public Prescription AddPrescription()
        {
            var result = new Prescription();
            Prescriptions.Add(result);
            return result;
        }

        public void RemovePrescription(Prescription prescription)
        {
            Prescriptions.Remove(prescription);
        }

        public override object Clone()
        {
            var result = new PatientVisit
            {
                Id = Id,
                Date = Date,
                Complaints = Complaints,
                History = History,
                Observation = Observation,
                Treatment = Treatment,
                DoctorFee = DoctorFee,
                DrugFee = DrugFee,
                PrimaryDiagnosis = (SimpleEntity)PrimaryDiagnosis.Clone(),
                SecondaryDiagnosis = (SimpleEntity)SecondaryDiagnosis.Clone(),
                Status = (PatientStatus) Status.Clone()
            };

            foreach (var prescription in Prescriptions)
            {
                result.Prescriptions.Add((Prescription) prescription.Clone());
            }

            return result;
        }

        /// <summary>
        /// Remove prescrisptions which has empty drug
        /// Remove diagnosis which are of type simpleentityempty
        /// </summary>
        public override void CleanUp()
        {
            for (int i = Prescriptions.Count - 1; i >= 0; i--)
            {
                if (Prescriptions[i].Drug == Drug.Empty)   
                    Prescriptions.Remove(Prescriptions[i]);
            }
            if (PrimaryDiagnosis != null && PrimaryDiagnosis.GetType() == typeof (SimpleEntityEmpty)) 
                PrimaryDiagnosis = null;
            if (SecondaryDiagnosis != null && SecondaryDiagnosis.GetType() == typeof(SimpleEntityEmpty))
                SecondaryDiagnosis = null;
            Status = null;
        }
    }
}
