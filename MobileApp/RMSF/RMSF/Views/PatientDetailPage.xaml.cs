using RMSF.Models;
using RMSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RMSF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientDetailPage : ContentPage
    {
        PatientDetailViewModel viewModel;

        public PatientDetailPage(PatientDetailViewModel viewModel)
        {
            
            InitializeComponent();
            checkIfOK(viewModel.Patient);
            BindingContext = this.viewModel = viewModel;
        }

        public PatientDetailPage()
        {
            
            InitializeComponent();

            viewModel = new PatientDetailViewModel();
            BindingContext = viewModel;
        }

        async void OnButtonClicked(object sender, EventArgs args) 
        {
            if (this.viewModel.Patient.IsOk == 1)
            {
                await DisplayAlert("Failed", "No stream video avaiable", "OK");
            }
            else
            {
                var url = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/showPhotos.php?id=" + this.viewModel.Patient.Id ;
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            }
            
        }

        async void OnButtonHelpClicked(object sender, EventArgs args)
        {

            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("id", this.viewModel.Patient.Id.ToString()));
            var content = new FormUrlEncodedContent(postData);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");
            var response = await client.PostAsync("ist178685/FallDetection/patientRescued.php", content).ConfigureAwait(false); ;
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;

            Device.BeginInvokeOnMainThread(() =>
            {
                helpButton.IsVisible = false;
                helpButton.IsEnabled = false;
            });
           

        }

        void checkIfOK(Patient p) {



            if (p.IsOk == 1)
            {
                helpButton.IsVisible = false;
                helpButton.IsEnabled = false;
            }
            else {
                helpButton.IsVisible = true;
                helpButton.IsEnabled = true;
            }

            return;
        }




    }
}