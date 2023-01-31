using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Capremci.Modelos;
using System.Net.Http;
using System.Net;
using System.Collections.ObjectModel;
using System.Linq;

namespace Capremci.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

       
        public Login()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();
        }

        private async void Loguear_Clicked(object sender, EventArgs e)
        {
            var usu = txtUsuario.Text;
            var clav = txtClave.Text;

            if (usu == "" || clav == "")
            {
                await DisplayAlert("Mensaje", "Ingrese Usuario o Contraseña", "cerrar");
                return;
            }


            try
            {

                LoginC log = new LoginC
                {
                    cedula_usuarios = txtUsuario.Text,
                    clave_usuarios = txtClave.Text
                };



                Uri RequestUri = new Uri("http://192.168.1.232/rp_c/webservices/LoginService.php");
                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(log);
                var contentJson =  new StringContent(json, Encoding.UTF8, "application/json");
                var response =  await client.PostAsync(RequestUri, contentJson);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();


                    List<Capremci.Modelos.ObtenerDatos> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.ObtenerDatos>>(responseContent);
                    ObservableCollection<Capremci.Modelos.ObtenerDatos> _post = new ObservableCollection<Capremci.Modelos.ObtenerDatos>(posts);


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
                    byte[] fotografia_usuarios= null;



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

                  

                    if (response.Content != null)
                    {
                        await Navigation.PushAsync(new Validacion(id_usuarios, cedula_usuarios, nombre_usuarios, apellidos_usuarios,
                            correo_usuarios, celular_usuarios, celular_cifrado, digito_verificador, usuario_usuarios, id_rol, id_estado, fotografia_usuarios
                            ));
                    }
                   
                }else if (response.StatusCode == HttpStatusCode.NoContent)
                {


                    await DisplayAlert("Mensaje", "Usuario o Contraseña Incorrecta", "cerrar");
                    txtUsuario.Text = "";
                    txtClave.Text = "";
                }
                else
                {
                    await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");
                    txtUsuario.Text = "";
                    txtClave.Text = "";
                }

            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
                txtUsuario.Text = "";
                txtClave.Text = "";
            }
            
          
          




        }
    }
}