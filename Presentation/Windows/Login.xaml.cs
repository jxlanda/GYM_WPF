using Common.Cache;
using Domain.Models;
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

namespace Presentation
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void EntrarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UsuarioTextBox.Text != "")
            {
                if (PinTextBox.Password != "")
                {
                    UsuarioModel usuario = new UsuarioModel();
                    var validLogin = usuario.LoginUser(UsuarioTextBox.Text, PinTextBox.Password);
                    if (validLogin == true)
                    {
                        if (UserCache.TipoUsuario == Convert.ToChar(UserType.Administrador))
                        {
                            Dashboard window1 = new Dashboard();
                            window1.Show();
                            window1.Closed += Logout;
                            this.Hide();

                        }
                        else
                        {
                            Dashboard window1 = new Dashboard();
                            window1.Show();
                            window1.Closed += Logout;
                            this.Hide();
                        }
                    }
                    else
                        MessageBox.Show("No se encontraron coincidencias");
                }
                else
                    MessageBox.Show("Ingrese contrasena");
            }
            else
                MessageBox.Show("Ingrese usuario");
        }
        private void Logout(object sender, EventArgs e)
        {
            UsuarioTextBox.Clear();
            PinTextBox.Clear();
            this.Show();
        }

        private void SalirBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClienteLoginLbl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClienteLogin clienteLogin = new ClienteLogin();
            clienteLogin.Show();
            this.Close();
        }

        private void ClienteLoginIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClienteLogin clienteLogin = new ClienteLogin();
            clienteLogin.Show();
            this.Close();
        }
    }
}
