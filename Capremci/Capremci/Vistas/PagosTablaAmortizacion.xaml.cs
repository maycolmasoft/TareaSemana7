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
using Capremci.Modelos;
using System.Diagnostics;

namespace Capremci.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagosTablaAmortizacion : ContentPage
    {
        int id_creditos_global = 0;
        public PagosTablaAmortizacion(int id_creditos)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();
            lbl_pagos_creditos.Text = "PAGOS DEL CREDITO N°: "+id_creditos.ToString();
            id_creditos_global = id_creditos;
            obtenerPagosTablaAmortizacion();
        }


        public async void obtenerPagosTablaAmortizacion()
        {

            try
            {

                var parametros = "?id_creditos=" + id_creditos_global;
                var Url = "http://192.168.1.232/rp_c/webservices/PagosTablaAmortizacionService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {


                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.PagosTablaAmortizacion> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.PagosTablaAmortizacion>>(responseContent);
                    ObservableCollection<Capremci.Modelos.PagosTablaAmortizacion> _post = new ObservableCollection<Capremci.Modelos.PagosTablaAmortizacion>(posts);
                    ListaPagos.ItemsSource = _post;

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

        private async void ListaPagos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Capremci.Modelos.PagosTablaAmortizacion)e.SelectedItem;
            var item = Obj.id_transacciones.ToString();
            int ID = Convert.ToInt32(item);



            try
            {


                string action = await DisplayActionSheet("Seleccione una Opción", "Cancelar", null, "Ver Detalle");
                Debug.WriteLine("Action: " + action);

                if (action == "Ver Detalle")
                {
                    await Navigation.PushAsync(new DetallePagosTablaAmortizacion(ID));
                }
                


            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}