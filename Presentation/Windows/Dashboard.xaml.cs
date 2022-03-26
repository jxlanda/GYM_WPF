using Common.Cache;
using Presentation.UserControls;
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
using System.Windows.Shapes;

namespace Presentation.Windows
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadUserData();

            EDashboardUControl control = new EDashboardUControl();
            SwitchScreen(control, "Dashboard");
        }
        private void LoadUserData()
        {
            try
            {
                UsuarioBrush.ImageSource = new BitmapImage(new Uri(UserCache.ImgPath));
                ApodoLbl.Content = UserCache.Apodo;
                TipoUsuarioLbl.Content = UserCache.NombreTipoUsuario;
            }
            catch
            {
                MessageBox.Show("No se encontro imagen");
            }
        }
        public void SwitchScreen(object sender, string title)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                ContainerGrid.Children.Clear();
                ContainerGrid.Children.Add(screen);
                OptionLbl.Content = title;
            }
        }

        private void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuarioUControl control = new UsuarioUControl();
            SwitchScreen(control, "Usuarios");
        }

        private void TiposUsuarios_Click(object sender, RoutedEventArgs e)
        {
            TipoUsuarioUControl control = new TipoUsuarioUControl();
            SwitchScreen(control, "Tipos de usuarios");
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            ClienteUControl control = new ClienteUControl();
            SwitchScreen(control, "Clientes");
        }

        private void CerrarSesionBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ejercicios_Click(object sender, RoutedEventArgs e)
        {
            EjercicioUControl control = new EjercicioUControl();
            SwitchScreen(control, "Ejercicios");
        }

        private void Rutinas_Click(object sender, RoutedEventArgs e)
        {
            RutinaUControl control = new RutinaUControl();
            SwitchScreen(control, "Rutinas");
        }

        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            EDashboardUControl control = new EDashboardUControl();
            SwitchScreen(control, "Dashboard");
        }
    }
}
