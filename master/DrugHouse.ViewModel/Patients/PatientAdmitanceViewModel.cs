/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;

namespace DrugHouse.ViewModel.Patients
{
    public sealed class PatientAdmitanceViewModel : PatientCaseViewModel
    {

        public PatientAdmitanceViewModel(PatientAdmitance admitance)
            :base(admitance)
        {
            if (admitance.DbStatus == RepositoryStatus.New)
                Timer();            
        }

        public class PropName
        {
        }

        #region Properties
        private PatientAdmitance PatientAdmitance
        {
            get { return (PatientAdmitance)Case; }
        }

        #endregion


    }
}
