using Capremci.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
	public partial class CodigoVerificacion : ContentPage
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
        string pin_verificacion_global = "";

        public CodigoVerificacion (int id_usuarios, string cedula_usuarios, string nombre_usuarios, string apellidos_usuarios, string correo_usuarios, string celular_usuarios, string celular_cifrado, string digito_verificador, string usuario_usuarios, int id_rol, int id_estado, byte[] fotografia_usuarios, string pin_verificacion)
		{
			InitializeComponent ();

            lbl_nombre_usuarios.Text = nombre_usuarios + " " + apellidos_usuarios;
            lbl_celular_usuarios.Text = celular_cifrado;
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
            pin_verificacion_global = pin_verificacion;

        }

        private async void btnVerificarPin_Clicked(object sender, EventArgs e)
        {
            try
            {
                string pin_verificacion = txt_pin_verificacion.Text;

                if (pin_verificacion == pin_verificacion_global)
                {

                    ActualizarPinRestWs log = new ActualizarPinRestWs
                    {
                        id_usuarios = id_usuarios_global,
                        celular_usuarios = celular_usuarios_global,
                        pin_validado = pin_verificacion
                    };

                    Uri RequestUri = new Uri("http://192.168.1.232/rp_c/webservices/ValidarPinService.php");
                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(log);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                       
                        if (response.Content != null)
                        {

                            if (id_rol_global == 2)
                            {

                                await Navigation.PushAsync(new Bienvenida(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                                            correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                                           ));
                            }
                            else
                            {
                                await Navigation.PushAsync(new BienvenidaAdmin(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                                            correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                                           ));
                            }

                        }

                    }
                    else if (response.StatusCode == HttpStatusCode.NoContent)
                    {

                        await DisplayAlert("Mensaje", "No se pudo Validar el PIN", "cerrar");
                        txt_pin_verificacion.Text = "".ToString();

                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");
                        txt_pin_verificacion.Text = "".ToString();

                    }


                }
                else
                {

                    await DisplayAlert("Notificación", "PIN Incorrecto", "Cerrar");
                    txt_pin_verificacion.Text = "".ToString();
                }
            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
                txt_pin_verificacion.Text = "".ToString();
            }
        }

        private void txt_pin_verificacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string dato = txt_pin_verificacion.Text;

                if (dato.Length > 4)
                {
                    DisplayAlert("Validación", "PIN 4 Dígítos", "cerrar");
                    txt_pin_verificacion.Text = "".ToString();
                }

            }
            catch (Exception dirEx)
            {
                DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
            }
        }

        private void btnNuevoPin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Validacion(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                            correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                ));
        }
    }
}