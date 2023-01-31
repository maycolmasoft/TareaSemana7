using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net;

namespace Capremci.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccesoRapido : ContentPage
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

        public  AccesoRapido (int id_usuarios, string cedula_usuarios, string nombre_usuarios, string apellidos_usuarios, string correo_usuarios, string celular_usuarios, string celular_cifrado, string digito_verificador, string usuario_usuarios, int id_rol, int id_estado, byte[] fotografia_usuarios, bool biometrico, bool pin, bool face_id)
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();

            verificarStatus();

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

        }


        private async void verificarStatus() {

            var res = await CrossFingerprint.Current.IsAvailableAsync();

            if (!res)
            {

                btnBiometrico.IsEnabled= false;
                btnFaceId.IsEnabled = false; 
            }

        }

        private async void btnBiometrico_Clicked(object sender, EventArgs e)
        {

            try
            {

                var dialogConfig = new AuthenticationRequestConfiguration("Ingrese su Huella Digital!", nombre_usuarios_global+" "+ apellidos_usuarios_global)
                {
                    CancelTitle = "Cancelar",
                    FallbackTitle = "Ok"
                };
                var res = await CrossFingerprint.Current.IsAvailableAsync(true);
                if (res)
                {
                    var result = await CrossFingerprint.Current.AuthenticateAsync(dialogConfig);

                    if (result.Authenticated)
                    {
                        LblAuthenticate.Text = "HUELLA VALIDADA CORRECTAMENTE";
                        LblAuthenticate.TextColor = Color.Green;

                        var parametros = "?id_usuarios=" + id_usuarios_global;
                        var Url = "http://192.168.1.232/rp_c/webservices/AccesoRapidoService.php";

                        var request = new HttpRequestMessage();
                        request.RequestUri = new Uri(Url + parametros);
                        request.Method = HttpMethod.Get;
                        request.Headers.Add("Accpet", "application/json");

                        var client = new HttpClient();
                        HttpResponseMessage response = await client.SendAsync(request);


                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            List<Capremci.Modelos.iniciar> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.iniciar>>(responseContent);
                            ObservableCollection<Capremci.Modelos.iniciar> _post = new ObservableCollection<Capremci.Modelos.iniciar>(posts);

                            int id_usuarios = 0;
                            string cedula_usuarios = "";
                            string nombre_usuarios = "";
                            string apellidos_usuarios = "";
                            string correo_usuarios = "";
                            string celular_usuarios = "";
                            string celular_cifrado = "";
                            string digito_verificador = "";
                            string usuario_usuarios = "";
                            int id_rol = 0;
                            int id_estado = 0;
                            byte[] fotografia_usuarios = null;

                            foreach (var item in _post)
                            {
                                id_usuarios = item.id_usuarios;
                                cedula_usuarios = item.cedula_usuarios;
                                nombre_usuarios = item.nombre_usuarios;
                                apellidos_usuarios = item.apellidos_usuarios;
                                correo_usuarios = item.correo_usuarios;
                                celular_usuarios = item.celular_usuarios;
                                celular_cifrado = item.celular_cifrado;
                                digito_verificador = item.digito_verificador;
                                usuario_usuarios = item.usuario_usuarios;
                                id_rol = item.id_rol;
                                id_estado = item.id_estado;
                                fotografia_usuarios = item.fotografia_usuarios;
                               
                            }

                            if (id_rol == 2)
                            {

                                await Navigation.PushAsync(new Bienvenida(id_usuarios, cedula_usuarios, nombre_usuarios, apellidos_usuarios,
                                            correo_usuarios, celular_usuarios, celular_cifrado, digito_verificador, usuario_usuarios, id_rol, id_estado, fotografia_usuarios
                                           ));
                            }
                            else
                            {
                                await Navigation.PushAsync(new BienvenidaAdmin(id_usuarios, cedula_usuarios, nombre_usuarios, apellidos_usuarios,
                                            correo_usuarios, celular_usuarios, celular_cifrado, digito_verificador, usuario_usuarios, id_rol, id_estado, fotografia_usuarios
                                           ));
                            }

                        }
                        else if (response.StatusCode == HttpStatusCode.NoContent)
                        {

                            await Navigation.PushAsync(new Login());

                        }
                        else
                        {
                            await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");

                        }

                    }
                    else
                    {
                        LblAuthenticate.Text = "HUELLA INVALIDA";
                        LblAuthenticate.TextColor = Color.Red;
                    }
                }

            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Error al usar biometrico " + dirEx.Message, "OK");
              
            }
        }

        private async void btnFaceId_Clicked(object sender, EventArgs e)
        {
            var res = await CrossFingerprint.Current.IsAvailableAsync();

            if (!res)
            {

                await DisplayAlert("Alerta", "No hay biometrico habilitado", "ok");

                return;
            }

            var autResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Hola", "Maycol"));

            if (autResult.Authenticated)
            {

                LblFace.Text = "FACE ID VALIDADO CORRECTAMENTE";
                LblFace.TextColor = Color.Green;

            }
        }

        private async void btnPin_Clicked(object sender, EventArgs e)
        {

            try
            {


                var parametros = "?id_usuarios=" + id_usuarios_global;
                var Url = "http://192.168.1.232/rp_c/webservices/AccesoRapidoService.php";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url + parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.iniciar> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.iniciar>>(responseContent);
                    ObservableCollection<Capremci.Modelos.iniciar> _post = new ObservableCollection<Capremci.Modelos.iniciar>(posts);

                    int id_usuarios = 0;
                    string cedula_usuarios = "";
                    string nombre_usuarios = "";
                    string apellidos_usuarios = "";
                    string correo_usuarios = "";
                    string celular_usuarios = "";
                    string celular_cifrado = "";
                    string digito_verificador = "";
                    string usuario_usuarios = "";
                    int id_rol = 0;
                    int id_estado = 0;
                    byte[] fotografia_usuarios = null;

                    foreach (var item in _post)
                    {
                        id_usuarios = item.id_usuarios;
                        cedula_usuarios = item.cedula_usuarios;
                        nombre_usuarios = item.nombre_usuarios;
                        apellidos_usuarios = item.apellidos_usuarios;
                        correo_usuarios = item.correo_usuarios;
                        celular_usuarios = item.celular_usuarios;
                        celular_cifrado = item.celular_cifrado;
                        digito_verificador = item.digito_verificador;
                        usuario_usuarios = item.usuario_usuarios;
                        id_rol = item.id_rol;
                        id_estado = item.id_estado;
                        fotografia_usuarios = item.fotografia_usuarios;

                    }

                    await Navigation.PushAsync(new Validacion(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                           correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                           ));

                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {

                    await Navigation.PushAsync(new Login());

                }
                else
                {
                    await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");

                }

               


            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Error al usar biometrico " + dirEx.Message, "OK");

            }

        }
    }
}