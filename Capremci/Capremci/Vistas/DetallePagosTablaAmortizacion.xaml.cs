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
    public partial class DetallePagosTablaAmortizacion : ContentPage
    {
        int id_transacciones_global=0;
        public DetallePagosTablaAmortizacion(int id_transacciones)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();
            lbl_detalle_pagos_creditos.Text= "DETALLE DEL PAGO N°: "+ id_transacciones.ToString();
            id_transacciones_global=id_transacciones;
            obtenerDetallePagosTablaAmortizacion();
        }


        public async void obtenerDetallePagosTablaAmortizacion()
        {

            try
            {

                var parametros = "?id_transacciones=" + id_transacciones_global;
                var Url = "http://192.168.1.232/rp_c/webservices/PagosDetalleTablaAmortizacionService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {


                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.PagosDetalleTablaAmortizacion> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.PagosDetalleTablaAmortizacion>>(responseContent);
                    ObservableCollection<Capremci.Modelos.PagosDetalleTablaAmortizacion> _post = new ObservableCollection<Capremci.Modelos.PagosDetalleTablaAmortizacion>(posts);
                    ListaDetallePagos.ItemsSource = _post;

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