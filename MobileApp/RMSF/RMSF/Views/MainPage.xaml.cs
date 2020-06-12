using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RMSF.Models;
using Xamarin.Essentials;

namespace RMSF.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {

        public static string Email { get; set; }
        public static string Password { get; set; }

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage(string email, string password)
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

            Email = email;
            Password = password;
        }

        public class BrowserTest
        {
            public async Task OpenBrowser(Uri uri)
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }


        public MainPage(string i) {

            InitializeComponent();
            BrowserTest b = new BrowserTest();

            var id = i.Split(new string[] { "\r" }, StringSplitOptions.None);

            var uri = "http://web.tecnico.ulisboa.pt/ist178685/FallDetection/showPhotos.php?id=" + id[0];
            Uri link = new Uri(uri);
            b.OpenBrowser(link);

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new PatientsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}