using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            InitializeComponent();

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
    }
}