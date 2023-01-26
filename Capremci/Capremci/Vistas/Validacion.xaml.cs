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
using System.IO;

namespace Capremci.Vistas
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Validacion : ContentPage
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

        public Validacion(int id_usuarios, string cedula_usuarios, string nombre_usuarios, string apellidos_usuarios, string correo_usuarios, string celular_usuarios, string celular_cifrado, string digito_verificador, string usuario_usuarios, int id_rol, int id_estado, byte[] fotografia_usuarios)
		{
			InitializeComponent ();
            lbl_nombre_usuarios.Text = nombre_usuarios +" "+apellidos_usuarios;
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

        }

        private async void btnVerificar_Clicked(object sender, EventArgs e)
        {
            try
            {
                string codigo_verificacion = txt_codigo_verificacion.Text;

                if (codigo_verificacion == digito_verificador_global) {

                        PinWs log = new PinWs
                        {
                            id_usuarios = id_usuarios_global,
                            celular_usuarios = celular_usuarios_global,
                            nombre_usuarios = nombre_usuarios_global,
                            apellidos_usuarios = apellidos_usuarios_global,
                            usuario_usuarios =   usuario_usuarios_global,
                            correo_usuarios = correo_usuarios_global
                        };

                        Uri RequestUri = new Uri("http://192.168.1.232/rp_c/webservices/SmsService.php");
                        var client = new HttpClient();
                        var json = JsonConvert.SerializeObject(log);
                        var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(RequestUri, contentJson);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            List<Capremci.Modelos.PinRestWs> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.PinRestWs>>(responseContent);
                            ObservableCollection<Capremci.Modelos.PinRestWs> _post = new ObservableCollection<Capremci.Modelos.PinRestWs>(posts);

                            string pin_verificacion = "";
                           
                            foreach (var item in _post)
                            {
                                pin_verificacion = item.pin_verificacion;
                            }

                            if (response.Content != null)
                            {
                                await Navigation.PushAsync(new CodigoVerificacion(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                                    correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global,
                                    pin_verificacion
                                    ));
                            }

                        }
                        else if (response.StatusCode == HttpStatusCode.NoContent)
                        {


                            await DisplayAlert("Mensaje", "No se pudo Enviar el SMS", "cerrar");
                            txt_codigo_verificacion.Text = "".ToString();
                           
                        }
                        else
                        {
                            await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");
                            txt_codigo_verificacion.Text = "".ToString();
                           
                        }


                }
                else {

                    await DisplayAlert("Notificación", "PIN Incorrecto", "Cerrar");
                    txt_codigo_verificacion.Text = "".ToString();
                }
            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
                txt_codigo_verificacion.Text = "".ToString();
            }

        }

        private void txt_codigo_verificacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string dato = txt_codigo_verificacion.Text;

                if (dato.Length  > 4)
                {
                    DisplayAlert("Validación", "PIN 4 Dígítos", "cerrar");
                    txt_codigo_verificacion.Text = "".ToString();
                }

            }
            catch (Exception dirEx)
            {
                 DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
            }
        }
    }
}