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
    /// Lógica de interacción para EjercicioUControl.xaml
    /// </summary>
    public partial class EjercicioUControl : UserControl
    {
        private EjercicioModel ejercicio = new EjercicioModel();
        public EjercicioUControl()
        {
            InitializeComponent();
            ListEjercicios();
        }
        private void ListEjercicios()
        {
            try
            {
                EjercicioDataGrid.ItemsSource = ejercicio.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
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

        private void EditarBtn_Click(object sender, RoutedEventArgs e)
        {
            EjercicioModel selectedModel = (EjercicioModel)EjercicioDataGrid.SelectedItem;
            EjercicioForm form = new EjercicioForm();
            form.SetData(selectedModel.Id,
                         selectedModel.Nombre,
                         selectedModel.Descripcion
                           );
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Ejercicios • Editar ejercicio");
                }
            }
        }

        private void Eliminarbtn_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            EjercicioModel selectedModel = (EjercicioModel)EjercicioDataGrid.SelectedItem;
            MessageBoxResult response;
            response = MessageBox.Show("¿Está seguro que desea eliminar el registro seleccionado ?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
            {
                ejercicio.EntityState = EntityState.Deleted;
                ejercicio.Id = selectedModel.Id;

                result = ejercicio.Savechanges();
                EjercicioDataGrid.ItemsSource = ejercicio.GetAll();
                MessageBox.Show(result);
            }
        }
    }
}
