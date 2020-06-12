using RMSF.Services;
using RMSF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RMSF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public static string token;
        static string email;
        

        public string Email {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        static string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {

            string result = logInAsync(email).Result;
            string[] field = result.Split(' ');

            if (field[3] == password) {
                Info.ID = Int32.Parse(field[1]);

                if (VersionTracking.IsFirstLaunchEver) {
                    token = DependencyService.Get<IMyInterface>()
                        .getToken();

                    SendRegistrationToServerAsync(field[1], token);
                }
                

                App.Current.MainPage.DisplayAlert("Login Successful", "", "Ok");


                
                App.Current.MainPage = new MainPage(Email, Password);

                Email = email;
                Password = password;

            }
            else {

                App.Current.MainPage.DisplayAlert("Login Failed", "Please enter correct Email and Password", "OK");
            }
        }

        public async Task<string> logInAsync(string email)
        {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("email", email));
                postData.Add(new KeyValuePair<string, string>("password", password));

                var content = new FormUrlEncodedContent(postData);

                HttpClient client = new HttpClient();
           
                client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");
            

                var response = await client.PostAsync("ist178685/FallDetection/Login.php", content).ConfigureAwait(false); ;
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;

                

            return result;
        }

        async void SendRegistrationToServerAsync(string id, string token)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("id", id));
            postData.Add(new KeyValuePair<string, string>("token", token));

            var content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");


            var response = await client.PostAsync("ist178685/FallDetection/Token.php", content).ConfigureAwait(false); ;
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false); ;

            return;
        }


    }
}
