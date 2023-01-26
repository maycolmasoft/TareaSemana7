using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capremci.VistasSQLite
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private SQLiteAsyncConnection _conn;

		public Login ()
		{
			InitializeComponent ();
            _conn = DependencyService.Get<DataBase>().GetConnection();

        }


        public static IEnumerable<Estudiante>SELECT_WHERE(SQLiteConnection db, string usuario, string contrasenia) {

            return db.Query<Estudiante>("select * from Estudiante where Usuario = ? and Contrasenia = ?", usuario, contrasenia);
        
        }


        private void btnIniciar_Clicked(object sender, EventArgs e)
        {

            try {

                var dataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "capremci.db3");
                var db = new SQLiteConnection(dataBasePath);

                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtClave.Text);

                if (resultado.Count() > 0)
                {

                    Navigation.PushAsync(new ConsultaRegistro());

                }
                else {

                    DisplayAlert("Alerta","Verifique su Usuario o Contraseña","Ok");
                
                }


            } catch(Exception ex) {

                DisplayAlert("Alerta", ex.Message, "Ok");
            }

        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }
    }
}