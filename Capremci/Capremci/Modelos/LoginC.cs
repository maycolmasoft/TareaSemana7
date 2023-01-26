using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Capremci.Modelos
{

    public class iniciar
    {
        public int id_usuarios { get; set; }
        public string cedula_usuarios { get; set; }
        public string nombre_usuarios { get; set; }
        public string apellidos_usuarios { get; set; }
        public string correo_usuarios { get; set; }
        public string celular_usuarios { get; set; }
        public string celular_cifrado { get; set; }
        public string digito_verificador { get; set; }
        public string usuario_usuarios { get; set; }
        public int id_rol { get; set; }
        public int id_estado { get; set; }
        public byte[] fotografia_usuarios { get; set; }
        public bool biometrico { get; set; }
        public bool pin { get; set; }
        public bool face_id { get; set; }

    }

    public class LoginC
    {
        public string cedula_usuarios { get; set; }
        public string clave_usuarios { get; set; }
    }

    public class ObtenerDatos
    {
        public int id_usuarios { get; set; }
        public string cedula_usuarios { get; set; }
        public string nombre_usuarios { get; set; }
        public string apellidos_usuarios { get; set; }
        public string correo_usuarios { get; set; }
        public string celular_usuarios { get; set; }

        public string celular_cifrado { get; set; }
        
        public string digito_verificador { get; set; }
        public string usuario_usuarios { get; set; }
        public int id_rol { get; set; }
        public int id_estado { get; set; }
        public byte[] fotografia_usuarios { get; set; }

    }


    public class PinWs
    {
        public int id_usuarios { get; set; }
        public string celular_usuarios { get; set; }
        public string nombre_usuarios { get; set; }
        public string apellidos_usuarios { get; set; }

        public string usuario_usuarios { get; set; }

        public string correo_usuarios { get; set; }
    }

    public class PinRestWs
    {
        public string pin_verificacion { get; set; }
    }

    public class ActualizarPinRestWs
    {
        public int id_usuarios { get; set; }
        public string celular_usuarios { get; set; }
        public string pin_validado { get; set; }
    }



}
