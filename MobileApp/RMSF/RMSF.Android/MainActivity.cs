using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Platform;

namespace RMSF.Droid
{
    [Activity(Label = "RMSF", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            if (Intent.Extras != null)
            {

                string[] words = NotificationHelper.Message.Split(" at ");

                words[1] = words[1].Remove(words[1].Length - 1);

                var r = patientByAddress(words[1]);

                base.OnCreate(savedInstanceState);

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


                LoadApplication(new App(r.Result));

            }
            else
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


                LoadApplication(new App());
            }
        }

        public async Task<string> patientByAddress(string address) {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("address", address));

            var content = new FormUrlEncodedContent(postData);

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://web.tecnico.ulisboa.pt/");

            var response = await client.PostAsync("ist178685/FallDetection/camIP.php", content).ConfigureAwait(false); 
            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false); 

            return result;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

     
        


    }


}