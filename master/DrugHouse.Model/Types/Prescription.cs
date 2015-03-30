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
            PrescriptionType = string.Empty;
            Remark = string.Empty;
        }

        public static ICollection<string> PrescriptionCollection = new Collection<string>()
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
        public virtual Drug Drug { get; set; }
        public string  PrescriptionType { get; set; }
        public virtual PatientVisit PatientVisit { get; set; }
        public override object Clone()
        {
            var result = new Prescription()
            {
                Id = Id,
                Remark = Remark,
                Drug = (Drug) Drug.Clone(),
                PrescriptionType = PrescriptionType
            };
            return result;
        }
    }

}
