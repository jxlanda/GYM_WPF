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
    /// Lógica de interacción para RutinaUControl.xaml
    /// </summary>
    public partial class RutinaUControl : UserControl
    {
        private RutinaModel rutina = new RutinaModel();
        public RutinaUControl()
        {
            InitializeComponent();
            ListRutinas();
        }

        private void ListRutinas()
        {
            try
            {
                RutinaDataGrid.ItemsSource = rutina.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
        {
            RutinaForm form = new RutinaForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Rutinas • Agregar rutina");
                }
            }
        }

        private void EditarBtn_Click(object sender, RoutedEventArgs e)
        {
            RutinaModel selectedModel = (RutinaModel)RutinaDataGrid.SelectedItem;
            RutinaForm form = new RutinaForm();
            form.SetData(selectedModel.Id,
                         selectedModel.Dia,
                         selectedModel.Repeticiones,
                         selectedModel.Peso,
                         selectedModel.IdEjercicio,
                         selectedModel.IdCliente
                           );
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Rutina • Editar rutina");
                }
            }
        }

        private void Eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            RutinaModel selectedModel = (RutinaModel)RutinaDataGrid.SelectedItem;
            MessageBoxResult response;
            response = MessageBox.Show("¿Está seguro que desea eliminar el registro seleccionado ?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {
                rutina.EntityState = EntityState.Deleted;
                rutina.Id = selectedModel.Id;

                result = rutina.Savechanges();
                RutinaDataGrid.ItemsSource = rutina.GetAll();
                MessageBox.Show(result);
            }
        }
    }
}
