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
    /// Lógica de interacción para TipoUsuarioUControl.xaml
    /// </summary>
    public partial class TipoUsuarioUControl : UserControl
    {
        private TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
        public TipoUsuarioUControl()
        {
            InitializeComponent();
            ListTiposUsuarios();
        }
        private void ListTiposUsuarios()
        {
            try
            {
                TipoUsuarioDataGrid.ItemsSource = tipoUsuario.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
        {
            TipoUsuarioForm form = new TipoUsuarioForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Tipos de Usuarios • Agregar tipo de usuario");
                }
            }
        }

        private void EditarBtn_Click(object sender, RoutedEventArgs e)
        {
            TipoUsuarioModel selectedModel = (TipoUsuarioModel)TipoUsuarioDataGrid.SelectedItem;
            TipoUsuarioForm form = new TipoUsuarioForm();
            form.SetData(selectedModel.Id,
                         selectedModel.Nombre,
                         selectedModel.TipoUsuario
                           );
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Tipos de Usuarios • Editar tipo de usuario");
                }
            }
        }

        private void Eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            TipoUsuarioModel selectedModel = (TipoUsuarioModel)TipoUsuarioDataGrid.SelectedItem;
            MessageBoxResult response;
            response = MessageBox.Show("¿Está seguro que desea eliminar el registro seleccionado ?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {
                tipoUsuario.EntityState = EntityState.Deleted;
                tipoUsuario.Id = selectedModel.Id;
                tipoUsuario.TipoUsuario = "0";

                result = tipoUsuario.Savechanges();
                TipoUsuarioDataGrid.ItemsSource = tipoUsuario.GetAll();
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
                    TipoUsuarioDataGrid.ItemsSource = tipoUsuario.FindBy(searchData);
                }
                else
                {
                    TipoUsuarioDataGrid.ItemsSource = tipoUsuario.GetAll();
                }
            }
            catch { TipoUsuarioDataGrid.ItemsSource = tipoUsuario.GetAll(); }
        }
    }
}
