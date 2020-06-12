using RMSF.Models;
using RMSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RMSF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientsPage : ContentPage
    {

        PatientsViewModel viewModel;

        public PatientsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new PatientsViewModel();
        }

        async void OnPatientSelected(object sender, EventArgs args) 
        {
            var layout = (BindableObject)sender;
            var patient = (Patient)layout.BindingContext;
            await Navigation.PushAsync(new PatientDetailPage(new PatientDetailViewModel(patient)));
        }

        async void AddPatient_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewPatientPage()));
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Patients.Count == 0)
                viewModel.IsBusy = true;
        }


    }
}