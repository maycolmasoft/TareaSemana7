using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capremci.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Iniciar : ContentPage
	{

		string imei = null;
        string nombre_dispositivo = null;

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
        bool biometrico = false;
        bool pin = false;
        bool face_id = false;

        public Iniciar ()
		{
			InitializeComponent ();

            var deviceId = CrossDeviceInfo.Current.Id;
            var deviceIdResult = new Label();
            deviceIdResult.Text = deviceId;
            imei = deviceIdResult.Text;

            var deviceName = CrossDeviceInfo.Current.DeviceName;
            var deviceNameResult = new Label();
            deviceNameResult.Text = deviceName;
            nombre_dispositivo = deviceNameResult.Text;

            if (imei != "" && imei != null) {

                obtener();
            }

           
        }

        public async void obtener()
        {
            try
            {

                var parametros = "?imei=" + imei + "&nombre_dispositivo=" + nombre_dispositivo;
                var Url = "http://192.168.1.232/rp_c/webservices/IniciarService.php";
          
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(Url+parametros);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accpet", "application/json");
              
                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);
               
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    List<Capremci.Modelos.iniciar> posts = JsonConvert.DeserializeObject<List<Capremci.Modelos.iniciar>>(responseContent);
                    ObservableCollection<Capremci.Modelos.iniciar> _post = new ObservableCollection<Capremci.Modelos.iniciar>(posts);

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
                        biometrico = item.biometrico;
                        pin = item.pin;
                        face_id = item.face_id;

                    }


                    if ((biometrico == true) || (pin == true) || (face_id == true))
                    {
                        await Navigation.PushAsync(new AccesoRapido(id_usuarios, cedula_usuarios, nombre_usuarios, apellidos_usuarios,
                            correo_usuarios, celular_usuarios, celular_cifrado, digito_verificador, usuario_usuarios, id_rol, id_estado, fotografia_usuarios,
                            biometrico, pin, face_id
                            ));

                    }
                    else {

                        await Navigation.PushAsync(new Login());

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
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Error no pudimos conectar con el servidor " + dirEx.Message, "OK");
               
            }







        }




    }
}