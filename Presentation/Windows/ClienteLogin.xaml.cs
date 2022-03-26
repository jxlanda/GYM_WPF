using Domain.Models;
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
    /// Lógica de interacción para ClienteLogin.xaml
    /// </summary>
    public partial class ClienteLogin : Window
    {
        public ClienteLogin()
        {
            InitializeComponent();
        }

        private void EntrarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ApodoTextBox.Text != "")
            {
                if (PinTextBox.Password != "")
                {
                    ClienteModel cliente = new ClienteModel();
                    var validLogin = cliente.LoginCustomer(ApodoTextBox.Text, PinTextBox.Password);
                    if (validLogin == true)
                    {
                        ClienteDashboard window1 = new ClienteDashboard();
                        window1.Show();
                        window1.Closed += Logout;
                        this.Hide();

                    }
                    else
                        MessageBox.Show("No se encontraron coincidencias");
                }
                else
                    MessageBox.Show("Ingrese pin");
            }
            else
                MessageBox.Show("Ingrese usuario");
        }
        private void Logout(object sender, EventArgs e)
        {
            ApodoTextBox.Clear();
            PinTextBox.Clear();
            this.Show();
        }

        private void RegresarBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
