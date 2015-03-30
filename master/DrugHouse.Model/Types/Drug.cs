/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.ComponentModel.DataAnnotations;
using DrugHouse.Shared.Enumerations;

namespace DrugHouse.Model.Types
{
    #region "Constructors"

    public class Drug : ModelBase
    {
        public Drug(DrugType dType)
        {
            DrugType = dType;
        }
        public Drug()
        {
            DrugId = (long)decimal.Zero;
            Name = string.Empty;
            DrugType = DrugType.None;
            Remarks = string.Empty;
        }

    #endregion
        public static Drug Empty = new DrugEmpty();
        [Key]
        public long DrugId { get; set; }
        public string Name { get; set; }
        public DrugType DrugType { get; set; }
        public string Remarks { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public override object Clone()
        {
            var result = new Drug {DrugId = DrugId, Name = Name, DrugType = DrugType, Remarks = Remarks};

            return result;
        }
    }
}