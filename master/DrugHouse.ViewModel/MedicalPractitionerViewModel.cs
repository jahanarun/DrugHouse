/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Collections.Generic;
using System.Linq;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using GalaSoft.MvvmLight;

namespace DrugHouse.ViewModel
{
    public class MedicalPractitionerViewModel : ViewModelBase
    {
        public MedicalPractitionerViewModel()
        {
            medicalPractitionersList = new List<MedicalPractitioner>();
        }
        private readonly List<MedicalPractitioner> medicalPractitionersList;

        #region PublicMembers
        public class PropName
        {
            public const string PatientListUpdated = "PatientListUpdated";
        }
        public int Count()
        {
            if (medicalPractitionersList != null) return medicalPractitionersList.Count();
            return 0;
        }

        public void Add(MedicalPractitioner p)
        {
            medicalPractitionersList.Add(p);
            RaisePropertyChanged(PropName.PatientListUpdated);
        }
        public void Delete(MedicalPractitioner p)
        {
            RaisePropertyChanged(PropName.PatientListUpdated);
        }

        public List<MedicalPractitioner> GetList()
        {
            var t = medicalPractitionersList.Where(x => x.DbStatus != RepositoryStatus.Deleted);
            return t.ToList();
        }

        private long NextPatientId()
        {
            if (medicalPractitionersList != null)
            {
                return medicalPractitionersList.Last().Id + 1;
            }
            else
            {
                return 1;
            }
        }
        #endregion

    }
}
