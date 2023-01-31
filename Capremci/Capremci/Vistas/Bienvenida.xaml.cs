using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Capremci.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Capremci.VistasSQLite;
using Xamarin.Essentials;
using System.Diagnostics;
using Syncfusion.SfNavigationDrawer.XForms;
using static Android.Provider.ContactsContract;
using System.IO;

namespace Capremci.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bienvenida : ContentPage
    {
       

        int id_usuarios_global = 0;
        string cedula_usuarios_global = "";
        string nombre_usuarios_global = "";
        string apellidos_usuarios_global = "";
        string correo_usuarios_global = "";
        string celular_usuarios_global = "";
        string celular_cifrado_global = "";
        string digito_verificador_global = "";
        string usuario_usuarios_global = "";
        int id_rol_global = 0;
        int id_estado_global = 0;
        byte[] fotografia_usuarios_global = null;

        public Bienvenida(int id_usuarios, string cedula_usuarios, string nombre_usuarios, string apellidos_usuarios, string correo_usuarios, string celular_usuarios, string celular_cifrado, string digito_verificador, string usuario_usuarios, int id_rol, int id_estado, byte[] fotografia_usuarios)
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();

            lbl_nombre_usuarios.Text = nombre_usuarios + " " + apellidos_usuarios;
            lbl_fotografia_usuarios.Source = ImageSource.FromStream(() => new MemoryStream(fotografia_usuarios));


            id_usuarios_global = id_usuarios;
            cedula_usuarios_global = cedula_usuarios;
            nombre_usuarios_global = nombre_usuarios;
            apellidos_usuarios_global = apellidos_usuarios;
            correo_usuarios_global = correo_usuarios;
            celular_usuarios_global = celular_usuarios;
            celular_cifrado_global = celular_cifrado;
            digito_verificador_global = digito_verificador;
            usuario_usuarios_global = usuario_usuarios;
            id_rol_global = id_rol;
            id_estado_global = id_estado;
            fotografia_usuarios_global = fotografia_usuarios;
            obtenerAportes();
            obtenerCreditos();


            // desde aqui maycol

          
        }

        private async void ListaCreditos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Creditos)e.SelectedItem;
            var item = Obj.id_creditos.ToString();
            int ID = Convert.ToInt32(item);

           

            try
            {
                
               
                string action = await DisplayActionSheet("Seleccione una Opción", "Cancelar", null, "Ver Tabla Amortización", "Ver Pagos");
                Debug.WriteLine("Action: " + action);

                if (action== "Ver Tabla Amortización")
                {
                    await Navigation.PushAsync(new TablaAmortizacion(ID));
                }
                else if (action == "Ver Pagos")
                {

                    await Navigation.PushAsync(new PagosTablaAmortizacion(ID));
                }


            }
            catch (Exception ex)
            {

                throw;
            }


            


        }


        public async void obtenerCreditos()
        {

            try
            {

                var parametros = "?id_usuarios=" + id_usuarios_global;
                var Url = "http://192.168.1.232/rp_c/webservices/CreditosService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {

                  
                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.Creditos> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.Creditos>>(responseContent);
                    ObservableCollection<Capremci.Modelos.Creditos> _post = new ObservableCollection<Capremci.Modelos.Creditos>(posts);
                    ListaCreditos.ItemsSource = _post;

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

        public async void obtenerAportes()
        {


            try
            {

                var parametros = "?id_usuarios=" + id_usuarios_global;
                var Url = "http://192.168.1.232/rp_c/webservices/CuentaIndividualService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {


                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.Aportes> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.Aportes>>(responseContent);
                    ObservableCollection<Capremci.Modelos.Aportes> _post = new ObservableCollection<Capremci.Modelos.Aportes>(posts);
                    ListaAportes.ItemsSource = _post;

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

        private async void ListaAportes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Aportes)e.SelectedItem;
            var item = Obj.id_contribucion_tipo.ToString();
            int id_contribucion_tipo = Convert.ToInt32(item);

            var item1 = Obj.id_participes.ToString();
            int id_participes = Convert.ToInt32(item1);

            var tipo = Obj.tipo.ToString();
            

            try
            {


                string action = await DisplayActionSheet("Seleccione una Opción", "Cancelar", null, "Ver Detalle de Movimientos");
                Debug.WriteLine("Action: " + action);

                if (action == "Ver Detalle de Movimientos")
                {
                    await Navigation.PushAsync(new CuentaIndividualDetalle(id_contribucion_tipo, id_participes, tipo));
                }
                

            }
            catch (Exception ex)
            {

                throw;
            }
        }

       
    }
}