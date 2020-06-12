using Newtonsoft.Json;
using RMSF.Models;
using RMSF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RMSF.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public Command LoadPatientsCommand { get; set; }

        public PatientsViewModel()
        {
            Title = "Patients";
            Patients = new ObservableCollection<Patient>();
            LoadPatientsCommand = new Command(async () => await ExecuteLoadPatientsCommand());

            MessagingCenter.Subscribe<NewPatientPage, Patient>(this, "AddPatient", async (obj, item) =>
            {
                var newPatient = item as Patient;
                postPatient(newPatient);
                Patients.Add(newPatient);
            });
        }

        async void postPatient(Patient patient)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", patient.Name));
            postData.Add(new KeyValuePair<string, string>("surname", patient.Surname));
            postData.Add(new KeyValuePair<string, string>("age", patient.Age.ToString()));
            postData.Add(new KeyValuePair<string, string>("address", patient.Address));
            postData.Add(new KeyValuePair<string, string>("risk", patient.Risk));
            postData.Add(new KeyValuePair<string, string>("email", MainPage.Email));

            var content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");

            var response = await client.PostAsync("ist178685/FallDetection/newPatient.php", content).ConfigureAwait(false); 
        }

        async Task<ObservableCollection<Patient>> getPatientsAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");
            var response = await client.GetAsync("ist178685/FallDetection/getPatients.php?email=" + MainPage.Email).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false); 

            List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(json);
            var _patients = new ObservableCollection<Patient>(patients);

            return _patients;
        }

        async Task ExecuteLoadPatientsCommand()
        {
            IsBusy = true;

            try
            {
                Patients.Clear();

                var items = await getPatientsAsync();
                foreach (var item in items)
                {
                    Patients.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
