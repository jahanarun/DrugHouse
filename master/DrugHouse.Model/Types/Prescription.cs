/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DrugHouse.Model.Types
{
    public class Prescription : ModelBase
    {
        public Prescription()
        {
            Drug = Drug.Empty;
            Dosage = string.Empty;
            Remark = string.Empty;
            Duration = 0;
        }

        public static ICollection<string> Dosages = new Collection<string>()
        {
            string.Empty,
            "0-0-1",
            "0-1-0",
            "1-0-0",
            "1-0-1",
            "1-1-1"
        };

        public static Prescription Empty = new PrescriptionEmpty();
        public long Id { get; set; }
        public string Remark { get; set; }  
        public int Duration
        {
            get { return DurationValue; }
            set
            {
                DurationValue = value; 
                UpdateTotalDrugs();
            }
        }

        public virtual Drug Drug { get; set; }

        public string Dosage
        {
            get { return DosageValue; }
            set
            {
                DosageValue = value; 
                UpdateTotalDrugs();
            }
        }

        public virtual PatientVisit PatientVisit { get; set; }

        public int TotalDrugs;
        private string DosageValue;
        private int DurationValue;

        private void UpdateTotalDrugs()
        {
            TotalDrugs = 0;
            if (Dosage == null)
                return;
            foreach (var c in Dosage)
            {
                int value;
                if (int.TryParse(c.ToString(), out value))
                {
                    TotalDrugs += (value*Duration);
                }      
            }
        }
        public override object Clone()
        {
            var result = new Prescription()
            {
                Id = Id,
                Remark = Remark,
                Drug = (Drug) Drug.Clone(),
                Dosage = Dosage
            };
            return result;
        }
    }

}
