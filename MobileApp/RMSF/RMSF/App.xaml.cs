using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RMSF.Services;
using RMSF.Views;

namespace RMSF
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();
        }

        public App(string i) {

            MainPage = new MainPage(i);
               
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    static class Info{
        public static int ID;
        public static string token;
    }

    
}
