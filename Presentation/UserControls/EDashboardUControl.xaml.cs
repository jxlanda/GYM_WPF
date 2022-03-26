using Domain.Models;
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
    /// Lógica de interacción para EDashboardUControl.xaml
    /// </summary>
    public partial class EDashboardUControl : UserControl
    {
        private UsuarioModel usuario = new UsuarioModel();
        private EjercicioModel ejercicio = new EjercicioModel();
        private ClienteModel cliente = new ClienteModel();
        public EDashboardUControl()
        {
            InitializeComponent();
            CountUsuarios();
            CountClientes();
            CountEjercicios();
        }
        private void CountUsuarios()
        {
            try
            {
                DataGrid usuarioDataGrid = new DataGrid
                {
                    ItemsSource = usuario.GetAll()
                };
                TotalUsuariosLabel.Content = (usuarioDataGrid.Items.Count -1).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CountClientes()
        {
            try
            {
                DataGrid clienteDataGrid = new DataGrid
                {
                    ItemsSource = cliente.GetAll()
                };
                TotalClientesLabel.Content = (clienteDataGrid.Items.Count -1).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CountEjercicios()
        {
            try
            {
                DataGrid ejercicioDataGrid = new DataGrid
                {
                    ItemsSource = cliente.GetAll()
                };
                TotalEjerciciosLabel.Content = (ejercicioDataGrid.Items.Count - 1).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AgregarClienteBtn_Click(object sender, RoutedEventArgs e)
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

        private void AgregarUsuarioBtn_Click(object sender, RoutedEventArgs e)
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

        private void AgregarEjercicioBtn_Click(object sender, RoutedEventArgs e)
        {
            EjercicioForm form = new EjercicioForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Ejercicios • Agregar ejercicio");
                }
            }
        }
    }
}
