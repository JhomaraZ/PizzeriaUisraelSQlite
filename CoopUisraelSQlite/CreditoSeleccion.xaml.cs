using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoopUisraelSQlite.Models;
using System.IO;

namespace CoopUisraelSQlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoSeleccion : ContentPage
    {
        public int IdSeleccionado;

        private SQLiteAsyncConnection _conn;      // creo al coneccion
        IEnumerable<Pedido> ResultadoDelete; // contenedor a estudiante  eleminiar
        IEnumerable<Pedido> ResultadoUpdate; // contenedor a estudiante  actualizar
        public PedidoSeleccion(int id, string usuario,string nombre, string apellido, string direccion,string monto, string plazo)
        {
            _conn = DependencyService.Get<DataBase>().GetConnection();
            IdSeleccionado = id;
            InitializeComponent();
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtDireccion.Text = direccion;
            txtMonto.Text = monto;
            txtPlazo.Text = plazo;
            lbUsuario.Text = usuario;
        }
       

        void btn_actualizar_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var DataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(DataBasePath);
                ResultadoUpdate = Update(db, txtNombre.Text, txtApellido.Text, txtDireccion.Text,txtMonto.Text,txtPlazo.Text ,IdSeleccionado);
                DisplayAlert("Alerta", "Se actualizo correctamente", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "ERROR" + ex.Message, "OK");
            }
        }

        private IEnumerable<Pedido> Update(SQLiteConnection db, object nombre, object apellido, object direccion,string monto,string plazo ,int idSeleccionado)
        {
            return db.Query<Pedido>("UPDATE pedido SET Nombres=?, Apellidos=? ,Direccion=?, monto=?, plazo=? where Id=?", nombre, apellido, direccion,monto,plazo ,idSeleccionado);
            //throw new NotImplementedException();
        }

        void btn_eliminar_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var DataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(DataBasePath);
                ResultadoDelete = Delete(db, IdSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro(lbUsuario.Text));
                DisplayAlert("Alerta", "Se elimino correctamente", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Error" + ex.Message, "OK");
            }
        }

        private IEnumerable<Pedido> Delete(SQLiteConnection db, int idSeleccionado)
        {
            return db.Query<Pedido>("DELETE FROM pedido where Id = ?", idSeleccionado);
            //throw new NotImplementedException();
        }

    }
}
