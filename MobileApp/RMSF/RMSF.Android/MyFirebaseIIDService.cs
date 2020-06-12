using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Iid;
using RMSF.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(
          typeof(PlatformInterface))]

namespace RMSF.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]


    
    public class MyFirebaseIIDService : FirebaseInstanceIdService 
    {
        const string TAG = "MyFirebaseIIDService";
        private string result;
        public static string token;
        public static string Token {

            get { return token; }
            set { token = value; }
        }

    

    public override void OnTokenRefresh()
        {
             token = FirebaseInstanceId.Instance.Token;
        }

        async System.Threading.Tasks.Task SendRegistrationToServerAsync(string token)
        {
            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                //postData.Add(new KeyValuePair<string, int>("id",id));
                postData.Add(new KeyValuePair<string, string>("token", token));
                var content = new FormUrlEncodedContent(postData);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://web.tecnico.ulisboa.pt/");

                var response = await client.PostAsync("http://web.tecnico.ulisboa.pt/ist178685/FallDetection/Token.php", content);
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
                return;
            }
        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }

    public class PlatformInterface : IMyInterface
    {
     
        public PlatformInterface()
        {
          
        }

        public string getToken()
        {
            return MyFirebaseIIDService.Token;
        }

        public string lastNotification()
        {
            return "";
        }
    }
}
