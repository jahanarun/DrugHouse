/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System;
using DrugHouse.Model.Enum;
using DrugHouse.Model.PatientDetails;

namespace DrugHouse.Model.Types
{
    public interface ICase
    {
        DateTime Date { get; set; }

        PatientStatus Status { get; set; }
        SimpleEntity PrimaryDiagnosis { get; set; }
        decimal DoctorFee { get; set; }
        RepositoryStatus DbStatus { get; set; }

    }
}