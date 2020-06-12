using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RMSF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StreamPage : ContentPage
    {


        public class BrowserTest
        {
            public async Task OpenBrowser(Uri uri)
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }

        public StreamPage()
        {
            InitializeComponent();

            

            BrowserTest b = new BrowserTest();
            Uri link = new Uri("http://google.com");
            b.OpenBrowser(link);

        }


        
    }

    
}