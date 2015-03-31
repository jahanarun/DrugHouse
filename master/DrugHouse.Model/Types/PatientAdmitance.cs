/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using DrugHouse.Model.PatientDetails;

namespace DrugHouse.Model.Types
{
    public class PatientAdmitance : ModelBase, ICase
    {
        public PatientAdmitance() { }
        public PatientAdmitance(Patient p)
        {
            Patient = p;
            Date = DateTime.Today;
            Status=new PatientStatus(){Status ="Test STatus"};
            PrimaryDiagnosis = SimpleEntity.Empty;
            //Fee = new PatientFee(){DoctorFee = 10, DrugFee = 10};
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public virtual SimpleEntity PrimaryDiagnosis { get; set; }
        public decimal DoctorFee { get; set; }
        public virtual PatientStatus Status { get; set; }
        public virtual Patient Patient { get; set; }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override void SetValuesBeforeDbSave()
        {
            if (PrimaryDiagnosis != null && PrimaryDiagnosis.GetType() == typeof(SimpleEntityEmpty))
                PrimaryDiagnosis = null;
            Status = null;
        }

    }
}
