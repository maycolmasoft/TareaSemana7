using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capremci.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablaAmortizacion : ContentPage
    {
        int id_creditos_global = 0;

        public TablaAmortizacion(int id_creditos)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();
            lbl_amortizacion_creditos.Text = "TABLA DE AMORTIZACIÓN DEL CREDITO N°: " + id_creditos.ToString();
            id_creditos_global = id_creditos;
            obtenerTablaAmortizacion();
        }


        public async void obtenerTablaAmortizacion()
        {

            try
            {

                var parametros = "?id_creditos=" + id_creditos_global;
                var Url = "http://192.168.1.232/rp_c/webservices/TablaAmortizacionService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {


                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.TablaAmortizacion> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.TablaAmortizacion>>(responseContent);
                    ObservableCollection<Capremci.Modelos.TablaAmortizacion> _post = new ObservableCollection<Capremci.Modelos.TablaAmortizacion>(posts);
                    ListaTablaAmortizacion.ItemsSource = _post;

                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {

                    //await Navigation.PushAsync(new Login());

                }
                else
                {
                    await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");

                }


            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Error no pudimos conectar con el servidor " + dirEx.Message, "OK");

            }



        }


    }
}