using RMSF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMSF.ViewModels
{
    public class PatientDetailViewModel : BaseViewModel
    {
        public Patient Patient { get; set; }

        public PatientDetailViewModel(Patient patient = null) 
        {
            Title = patient?.Name + " " + patient?.Surname;
            Patient = patient;
        }
    }
}
