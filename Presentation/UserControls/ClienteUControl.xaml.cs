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
    /// Lógica de interacción para ClienteUControl.xaml
    /// </summary>
    public partial class ClienteUControl : UserControl
    {
        private ClienteModel cliente = new ClienteModel();
        public ClienteUControl()
        {
            InitializeComponent();
            ListClientes();
        }
        private void ListClientes()
        {
            try
            {
                ClienteDataGrid.ItemsSource = cliente.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
        {
            ClienteForm form = new ClienteForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Clientes • Agregar cliente");
                }
            }
        }

        private void EditarBtn_Click(object sender, RoutedEventArgs e)
        {
            ClienteModel selectedModel = (ClienteModel)ClienteDataGrid.SelectedItem;
            ClienteForm form = new ClienteForm();
            form.SetData(selectedModel.Id,
                         selectedModel.ImgPath,
                         selectedModel.Apodo,
                         selectedModel.Pin,
                         selectedModel.Nombre,
                         selectedModel.ApellidoPaterno,
                         selectedModel.ApellidoMaterno,
                         selectedModel.Correo,
                         selectedModel.FNacimiento,
                         selectedModel.Peso,
                         selectedModel.Estatura,
                         selectedModel.Genero
                           );
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Clientes • Editar cliente");
                }
            }
        }

        private void Eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            ClienteModel selectedModel = (ClienteModel)ClienteDataGrid.SelectedItem;
            MessageBoxResult response;
            response = MessageBox.Show("¿Está seguro que desea eliminar el registro seleccionado ?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {

                cliente.EntityState = EntityState.Deleted;
                cliente.Id = selectedModel.Id;
                cliente.Genero = "0";

                result = cliente.Savechanges();
                ClienteDataGrid.ItemsSource = cliente.GetAll();
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
                    ClienteDataGrid.ItemsSource = cliente.FindBy(searchData);
                }
                else
                {
                    ClienteDataGrid.ItemsSource = cliente.GetAll();
                }
            }
            catch { ClienteDataGrid.ItemsSource = cliente.GetAll(); }
        }
    }
}
