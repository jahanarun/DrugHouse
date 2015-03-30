/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.ComponentModel.DataAnnotations;
using DrugHouse.Model.Enum;

namespace DrugHouse.Model.Types
{
    public class MedicalPractitioner : Person
    {
        #region Constructors

        public MedicalPractitioner(long idValue)
        {
            this.Id = idValue;
            Category = PersonType.MedicalPractitioner;
        }

        #endregion

        public struct MedicalPractitionerData
        {
            public long Id;
            public string EducationalDegree;
            public PersonData Person;

        }

        #region Properties

        [Key]
        public long Id { get; set; }

        public string EducationDegree { get; set; }

        #endregion

        #region Public Method

        #endregion

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}
