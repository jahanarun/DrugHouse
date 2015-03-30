/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using System.Globalization;
using DrugHouse.Model.PatientDetails;
using DrugHouse.Model.Types;

namespace DrugHouse.ViewModel.RowItems
{
    public class PatientCaseRow : NotificationObject
    {
        public PatientCaseRow(ICase v)
        {
            Case = v;
        }

        #region Properties
        public ICase Case { get; private set; }

        public DateTime Date
        {
            get { return Case.Date; }
        }

        public string CaseType
        {
            get
            {
                return Case.GetType() == typeof (PatientVisit) ? "Visit" : "Admittance";
                
            }
        }

        public string PrimaryDiagnosis
        {
            get { return Case.PrimaryDiagnosis == null ? string.Empty : Case.PrimaryDiagnosis.Name; }
        }
        public PatientStatus Status
        {
            get { return Case.Status; }
        }
        public string DoctorFee
        {
            get { return Case.DoctorFee.ToString(CultureInfo.InvariantCulture); }

        }

        #endregion
    }
}
