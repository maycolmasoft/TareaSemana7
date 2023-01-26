using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capremci.VistasSQLite
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Elemento : ContentPage
	{

        public int IdSeleccionado;
        private SQLiteConnection _conn;

        IEnumerable<Estudiante> ResultadoDelete;
        IEnumerable<Estudiante> ResultadoUpdate;

        public Elemento (int id)
		{
            _conn = DependencyService.Get<SQLiteConnection> ();
            IdSeleccionado= id;

			InitializeComponent ();
		}

        public static IEnumerable<Estudiante> DELETE(SQLiteConnection db, int id)
        {

            return db.Query<Estudiante>("delete from Estudiante where Id = ?", id);

        }

        public static IEnumerable<Estudiante> UPDATE(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {

            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?, Usuario = ?, Contrasenia = ? where Id = ?", nombre, usuario, contrasenia, id);

        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {

            try {

                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "capremci.db3");
                var db = new SQLiteConnection(dataBasePath);
                ResultadoDelete = UPDATE(db, txtNombre.Text, txtUsuario.Text, txtClave.Text, IdSeleccionado);

                DisplayAlert("Mensaje", "Estudiante Actualizado Correctamente", "Ok");

            } catch (Exception ex) {


                DisplayAlert("Mensaje", ex.Message, "Ok");
            }


        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "capremci.db3");
                var db = new SQLiteConnection(dataBasePath);
                ResultadoDelete = DELETE(db, IdSeleccionado);

                DisplayAlert("Mensaje","Estudiante Eliminado","Ok");
            }
            catch (Exception ex)
            {

                DisplayAlert("Mensaje", ex.Message, "Ok");

            }
        }
    }
}