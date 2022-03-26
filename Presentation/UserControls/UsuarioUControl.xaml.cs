using Domain.Models;
using Domain.ValueObjects;
using Presentation.Forms;
using Presentation.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.UserControls
{
    /// <summary>
    /// Lógica de interacción para UsuarioUControl.xaml
    /// </summary>
    public partial class UsuarioUControl : UserControl
    {
        private UsuarioModel usuario = new UsuarioModel();
        public UsuarioUControl()
        {
            InitializeComponent();
            ListUsuarios();
        }
        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuarioForm form = new UsuarioForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Usuarios • Agregar usuario");
                }
            }
        }
        private void ListUsuarios()
        {
            try
            {
                UsuarioDataGrid.ItemsSource = usuario.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EditarBtn_Click(object sender, RoutedEventArgs e)
        {

            UsuarioModel selectedModel = (UsuarioModel)UsuarioDataGrid.SelectedItem;
            UsuarioForm form = new UsuarioForm();
            form.SetData(selectedModel.Id,
                         selectedModel.ImgPath,
                         selectedModel.Apodo,
                         selectedModel.Pin,
                         selectedModel.IdTipoUsuario,
                         selectedModel.Nombre,
                         selectedModel.ApellidoPaterno,
                         selectedModel.ApellidoMaterno,
                         selectedModel.Correo,
                         selectedModel.Telefono,
                         selectedModel.Genero
                           );
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Usuarios • Editar usuario");
                }
            }
            /*form.ShowDialog();
            if (form.DialogResult.HasValue && modal.DialogResult.Value)
            {
                MessageBox.Show(modal.message);
                UsuarioDataGrid.ItemsSource = usuario.GetAll();
            }*/
        }

        private void Eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            UsuarioModel selectedModel = (UsuarioModel)UsuarioDataGrid.SelectedItem;
            MessageBoxResult response;
            response = MessageBox.Show("¿Está seguro que desea eliminar el registro seleccionado ?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {

                usuario.EntityState = EntityState.Deleted;
                usuario.Id = selectedModel.Id;
                usuario.Genero = "0";

                result = usuario.Savechanges();
                UsuarioDataGrid.ItemsSource = usuario.GetAll();
                MessageBox.Show(result);
            }
        }

        private void BuscarBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                String searchData = sender.ToString().Remove(0, 33);
                if (searchData != "")
                {
                    UsuarioDataGrid.ItemsSource = usuario.FindBy(searchData);
                }
                else
                {
                    UsuarioDataGrid.ItemsSource = usuario.GetAll();
                }
            }
            catch { UsuarioDataGrid.ItemsSource = usuario.GetAll(); }
        }
    }
}
