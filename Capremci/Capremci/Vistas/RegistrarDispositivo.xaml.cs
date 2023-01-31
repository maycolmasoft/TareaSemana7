using Capremci.Modelos;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
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
    public partial class RegistrarDispositivo : ContentPage
    {
        string imei_global = null;
        string nombre_dispositivo_global = null;

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
        public RegistrarDispositivo(int id_usuarios, string cedula_usuarios, string nombre_usuarios, string apellidos_usuarios, string correo_usuarios, string celular_usuarios, string celular_cifrado, string digito_verificador, string usuario_usuarios, int id_rol, int id_estado, byte[] fotografia_usuarios)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzODU1MEAzMjMwMmUzNDJlMzBlVkxBMzU1Q1QvZnRLT3BMRktabytrM0NNcTNYWVdjeFFCK1Z5SzBybm5jPQ==");

            InitializeComponent();

            var deviceId = CrossDeviceInfo.Current.Id;
            var deviceIdResult = new Label();
            deviceIdResult.Text = deviceId;
            imei_global = deviceIdResult.Text;

            var deviceName = CrossDeviceInfo.Current.DeviceName;
            var deviceNameResult = new Label();
            deviceNameResult.Text = deviceName;
            nombre_dispositivo_global = deviceNameResult.Text;

            lbl_nombre_usuarios.Text = nombre_usuarios + " " + apellidos_usuarios;
            lbl_celular_usuarios.Text = celular_cifrado;
            lbl_fotografia_usuarios.Source = ImageSource.FromStream(() => new MemoryStream(fotografia_usuarios));
            txt_dispositivo.Text= nombre_dispositivo_global;

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

        private async void btnRegistrarDispositivo_Clicked(object sender, EventArgs e)
        {
            try
            { 

                if (imei_global != "" && nombre_dispositivo_global != "")
                {

                    DatosDispositivo log = new DatosDispositivo
                    {
                        id_usuarios = id_usuarios_global,
                        imei = imei_global,
                        nombre_dispositivo = nombre_dispositivo_global
                    };

                    Uri RequestUri = new Uri("http://192.168.1.232/rp_c/webservices/RegistrarDispositivoService.php");
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

                        await DisplayAlert("Mensaje", "No se pudo Registrar Dispositivo", "cerrar");
                        

                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "No se pudo establecer una conexión con el servidor", "cerrar");
                        

                    }


                }
                else
                {

                    await DisplayAlert("Notificación", "No se detecto dispositivo", "Cerrar");
                   
                }
            }
            catch (Exception dirEx)
            {
                await DisplayAlert("Mensaje", "Datos invalidos " + dirEx.Message, "OK");
                
            }
        }

        private void btnCancelarDispositivo_Clicked(object sender, EventArgs e)
        {
            if (id_rol_global == 2)
            {

                 Navigation.PushAsync(new Bienvenida(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                            correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                           ));
            }
            else
            {
                 Navigation.PushAsync(new BienvenidaAdmin(id_usuarios_global, cedula_usuarios_global, nombre_usuarios_global, apellidos_usuarios_global,
                            correo_usuarios_global, celular_usuarios_global, celular_cifrado_global, digito_verificador_global, usuario_usuarios_global, id_rol_global, id_estado_global, fotografia_usuarios_global
                           ));
            }
        }
    }
}