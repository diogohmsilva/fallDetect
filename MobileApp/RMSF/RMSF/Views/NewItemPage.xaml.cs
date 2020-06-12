using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RMSF.Models;

namespace RMSF.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Patient patient { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddPatient", patient);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void setRisk()
        {
            if (picker.SelectedItem.Equals("Low"))
            {
                patient.Risk = "L";
            }
            else if (picker.SelectedItem.Equals("Medium"))
            {
                patient.Risk = "M";
            }
            else {
                patient.Risk = "H";
            }

        }
    }
}