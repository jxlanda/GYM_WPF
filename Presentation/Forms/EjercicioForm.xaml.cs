using Domain.Models;
using Domain.ValueObjects;
using Presentation.UserControls;
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

namespace Presentation.Forms
{
    /// <summary>
    /// Lógica de interacción para EjercicioForm.xaml
    /// </summary>
    public partial class EjercicioForm : UserControl
    {
        private EjercicioModel ejercicio = new EjercicioModel();

        bool isModifying = false;
        string idEjercicio;
        public string message;
        public EjercicioForm()
        {
            InitializeComponent();
            SetMaxValues();
        }
        private void SetMaxValues()
        {
            NombreTextBox.MaxLength = 20;
            DescripcionTextBox.MaxLength = 250;

        }
        public void SetData(string id, string nombre, string descripcion)
        {
            isModifying = true;
            idEjercicio = id;
            NombreTextBox.Text = nombre;
            DescripcionTextBox.Text = descripcion;
        }

        private void GuardarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isModifying == true)
            {
                ejercicio.EntityState = EntityState.Modified;
                ejercicio.Id = idEjercicio;
            }
            else
                ejercicio.EntityState = EntityState.Added;

            ejercicio.Nombre = NombreTextBox.Text;
            ejercicio.Descripcion = DescripcionTextBox.Text;

            bool validation = new Helps.DataValidation(ejercicio).Validate();

            if (validation == true)
            {
                string result = ejercicio.Savechanges();
                MessageBox.Show(result);

                EjercicioUControl control = new EjercicioUControl();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Dashboard))
                    {
                        (window as Dashboard).SwitchScreen(control, "Ejercicios");
                    }
                }
            }
        }

        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            EjercicioUControl control = new EjercicioUControl();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(control, "Ejercicios");
                }
            }
        }
    }
}
