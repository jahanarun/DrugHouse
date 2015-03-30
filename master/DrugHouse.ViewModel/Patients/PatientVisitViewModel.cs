/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using DrugHouse.Model.Types;

namespace DrugHouse.ViewModel.Patients
{
    public sealed class PatientVisitViewModel : PatientCaseViewModel
    {

        public PatientVisitViewModel(PatientVisit visit)
            :base(visit)
        {
            PatientPrescriptionViewModelValue = new PatientPrescriptionViewModel(PatientVisit);
            PatientPrescriptionViewModelValue.OnDirty += (sender, e) => RaiseDirty();
        }

        public class PropName
        {
            public const string DoctorFee = "DoctorFee";
            public const string DrugFee = "DrugFee";
            public const string Complaints = "Complaints";
            public const string PatientPrescriptionViewModel = "PatientPrescriptionViewModel";
            public const string Diagnoses = "Diagnoses";
            public const string PrimaryDiagnosis = "PrimaryDiagnosis";
            public const string SecondaryDiagnosis = "SecondaryDiagnosis";
            public const string History = "History";
            public const string Treatment = "Treatment";
            public const string Observation = "Observation";
            public const string VisitDate = "VisitDate";
        }

        #region Properties
        private PatientVisit PatientVisit
        {
            get { return (PatientVisit)Case; }
        }

        private PatientPrescriptionViewModel PatientPrescriptionViewModelValue;
        public PatientPrescriptionViewModel PatientPrescriptionViewModel
        {
            get { return PatientPrescriptionViewModelValue; }
            set
            {
                PatientPrescriptionViewModelValue = value;
                RaisePropertyChanged(PropName.PatientPrescriptionViewModel);
            }
        }

        public SimpleEntity SecondaryDiagnosis
        {
            get
            {
                return PatientVisit.SecondaryDiagnosis;
            }
            set
            {
                PatientVisit.SecondaryDiagnosis = value;
                RaisePropertyChanged(PropName.SecondaryDiagnosis);
            }
        }
        public string Complaints
        {
            get
            {
                return PatientVisit.Complaints;
            }
            set
            {
                PatientVisit.Complaints = value;
                RaisePropertyChanged(PropName.Complaints);
            }
        }
        public string History
        {
            get
            {
                return PatientVisit.History;
            }
            set
            {
                PatientVisit.History = value;
                RaisePropertyChanged(PropName.History);
            }
        }
        public string Observation
        {
            get
            {
                return PatientVisit.Observation;
            }
            set
            {
                PatientVisit.Observation = value;
                RaisePropertyChanged(PropName.Observation);
            }
        }
        public string Treatment
        {
            get
            {
                return PatientVisit.Treatment;
            }
            set
            {
                PatientVisit.Treatment = value;
                RaisePropertyChanged(PropName.Treatment);
            }
        }  

       public decimal DrugFee
        {
            get { return PatientVisit.DrugFee; }
            set
            {
                PatientVisit.DrugFee = value;
                RaisePropertyChanged(PropName.DrugFee);
            }
        }
        #endregion
       
    }
}
