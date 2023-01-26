using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capremci.VistasSQLite
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{

        private SQLiteAsyncConnection _conn;

        public Registro ()
		{
			InitializeComponent ();
            _conn = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {

            var DatosRegistros = new Estudiante { Nombre=txtNombre.Text, Usuario=txtUsuario.Text, Contrasenia=txtClave.Text};
            _conn.InsertAsync(DatosRegistros);
            limpiarFormulario();

        }

        void limpiarFormulario() {

            txtNombre.Text = "";
            txtUsuario.Text = "";
            txtClave.Text = "";
            DisplayAlert("Alerta", "Se agrego correctamente", "Ok");


        }




    }
}