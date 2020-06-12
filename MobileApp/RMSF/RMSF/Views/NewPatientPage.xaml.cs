using Org.BouncyCastle.Crypto.Engines;
using RMSF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RMSF.Views
{
    [DesignTimeVisible(false)]
    public partial class NewPatientPage : ContentPage
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string age { get; set; }
        public string risk { get; set; }
        

        public NewPatientPage()
        {
            InitializeComponent();


            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(age) || String.IsNullOrEmpty(address) || picker.SelectedItem == null)
            {
                DisplayAlert("Error", "Please fill all the fields before proceeding", "OK");
            }
            else
            {
                Patient p = new Patient(name, surname, age, address, setRisk(picker.SelectedItem.ToString()));
                MessagingCenter.Send(this, "AddPatient", p);
                await Navigation.PopModalAsync();



            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        string setRisk(string selectedRisk)
        {
            if (selectedRisk.Equals("Low"))
            {
                return  "L";
            }
            else if (selectedRisk.Equals("Medium"))
            {
                return  "M";
            }
            else {
                return "H";
            }

        }
    }
}