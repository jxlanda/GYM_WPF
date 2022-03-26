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
    /// Lógica de interacción para ClienteDashboard.xaml
    /// </summary>
    public partial class ClienteDashboard : Window
    {
        public ClienteDashboard()
        {
            InitializeComponent();
            LoadCustomerData();
            CRutinasUControl control = new CRutinasUControl();
            SwitchScreen(control, "Tus rutinas");
        }
        private void LoadCustomerData()
        {
            try
            {
                ClienteBrush.ImageSource = new BitmapImage(new Uri(CustomerCache.ImgPath));
                ApodoLbl.Content = CustomerCache.Apodo;
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
        private void CerrarSesionBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Rutina_Click(object sender, RoutedEventArgs e)
        {
            CRutinasUControl control = new CRutinasUControl();
            SwitchScreen(control, "Tus rutinas");
        }
    }
}
