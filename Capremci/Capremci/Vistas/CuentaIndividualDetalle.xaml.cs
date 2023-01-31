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
    public partial class CuentaIndividualDetalle : ContentPage
    {

        int id_contribucion_tipo_global = 0; 
        int id_participes_global = 0;
        string tipo_global = "";
        public CuentaIndividualDetalle(int id_contribucion_tipo, int id_participes, string tipo)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();
            lbl_detalle_cta_ind.Text = "MOVIMIENTOS DEL RUBRO: " + tipo;
            id_contribucion_tipo_global = id_contribucion_tipo;
            id_participes_global = id_participes;
            tipo_global = tipo;
            obtenerDetalleCuentaIndividual();
        }


        public async void obtenerDetalleCuentaIndividual()
        {

            try
            {

                var parametros = "?id_contribucion_tipo=" + id_contribucion_tipo_global+ "&id_participes=" + id_participes_global;
                var Url = "http://192.168.1.232/rp_c/webservices/CuentaIndividualDetalleService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {


                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.DetalleAportes> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.DetalleAportes>>(responseContent);
                    ObservableCollection<Capremci.Modelos.DetalleAportes> _post = new ObservableCollection<Capremci.Modelos.DetalleAportes>(posts);
                    ListaDetalleCtaInd.ItemsSource = _post;

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