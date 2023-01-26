using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System;
using Xamarin.Forms;
using Plugin.DeviceInfo;

namespace Capremci.Vistas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var deviceId = CrossDeviceInfo.Current.Id; 
            var deviceIdResult = new Label(); 
            deviceIdResult.Text = deviceId; 
            titleDeviceId.Text = "My device Id: " + deviceIdResult.Text;
        
        }

        private async void BtnAuthenticate_Clicked(object sender, EventArgs e)
        {
            var dialogConfig = new AuthenticationRequestConfiguration("Beweise, dass du Finger hast!", "")
            {
                CancelTitle = "Abbrechen",
                FallbackTitle = "Anders!"
            };
            var res = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (res) {
                var result = await CrossFingerprint.Current.AuthenticateAsync(dialogConfig);
            
                if (result.Authenticated)
            {
                LblAuthenticate.Text = "VALIDATION DONE";
                LblAuthenticate.TextColor = Color.Green;
            }
            else
            {
                LblAuthenticate.Text = "VALIDATION FAILED";
                LblAuthenticate.TextColor = Color.Red;
            }
            }
        }

        private async void BtnStatus_Clicked(object sender, EventArgs e)
        {
            FingerprintAvailability status = await CrossFingerprint.Current.GetAvailabilityAsync();
            LblStatus.Text = status.ToString();
        }

        private async void BtnFace_Clicked(object sender, EventArgs e)
        {

            var res = await CrossFingerprint.Current.IsAvailableAsync();

            if (!res)
            {

                await DisplayAlert("Alerta", "No hay biometrico habilitado", "ok");

                return;
            }

            var autResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Hola", "Maycol"));

            if (autResult.Authenticated) {

                LblFace.Text = "VALIDATION FACE DONE";
                LblFace.TextColor = Color.Green;

            }

            


        }
    }
}
