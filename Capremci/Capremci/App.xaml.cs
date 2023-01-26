using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Capremci.Vistas;
using Capremci.VistasSQLite;

namespace Capremci
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = MainPage = new NavigationPage(new Login());
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
}
