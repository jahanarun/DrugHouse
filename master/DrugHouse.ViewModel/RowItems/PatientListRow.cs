/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Shared.Enumerations;

namespace DrugHouse.ViewModel.RowItems
{
    public class PatientListRow
    {
        public PatientListRow()
        {
            Name = string.Empty;
            Address = string.Empty;
            Location = string.Empty;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }

    }

}
