/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

namespace DrugHouse.Model.Types
{
    public class PrescriptionEmpty : Prescription
    {
        public PrescriptionEmpty()
        {
            Id = 0;
            Drug = Drug.Empty;
            PrescriptionType = string.Empty;
            Remark = string.Empty;

        }
    }
}
