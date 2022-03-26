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
    /// Lógica de interacción para RutinaForm.xaml
    /// </summary>
    public partial class RutinaForm : UserControl
    {
        private RutinaModel rutina = new RutinaModel();
        private ClienteModel cliente = new ClienteModel();
        private EjercicioModel ejercicio = new EjercicioModel();

        bool isModifying = false;
        string idRutina;
        public string message;
        public RutinaForm()
        {
            InitializeComponent();
            FillDiaCombox();
            FillClienteCombox();
            FillEjercicioCombox();
        }
        private void FillDiaCombox()
        {
            DiaCombox.Items.Add("LUNES");
            DiaCombox.Items.Add("MARTES");
            DiaCombox.Items.Add("MIERCOLES");
            DiaCombox.Items.Add("JUEVES");
            DiaCombox.Items.Add("VIERNES");
            DiaCombox.Items.Add("SABADO");
            DiaCombox.Items.Add("DOMINGO");
        }
        private void FillClienteCombox()
        {
            ClienteCombox.ItemsSource = cliente.GetAll();
            ClienteCombox.DisplayMemberPath = "Nombre";
            ClienteCombox.SelectedValuePath = "Id";
        }
        private void FillEjercicioCombox()
        {
            EjercicioCombox.ItemsSource = ejercicio.GetAll();
            EjercicioCombox.DisplayMemberPath = "Nombre";
            EjercicioCombox.SelectedValuePath = "Id";
        }
        public void SetData(string id, string dia, string repeticiones, string peso, string idEjercicio, string idCliente)
        {
            isModifying = true;
            idRutina = id;
            DiaCombox.SelectedItem = dia;
            RepeticionesTextBox.Text = repeticiones;
            PesoTextBox.Text = peso;
            ClienteCombox.SelectedValue = idCliente;
            EjercicioCombox.SelectedValue = idEjercicio;
        }

        private void GuardarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isModifying == true)
            {
                rutina.EntityState = EntityState.Modified;
                rutina.Id = idRutina;
            }
            else
                rutina.EntityState = EntityState.Added;

            try
            {
                rutina.Dia = DiaCombox.SelectedValue.ToString();
            }
            catch { }
            rutina.Repeticiones = RepeticionesTextBox.Text;
            rutina.Peso = PesoTextBox.Text;
            try
            {
                rutina.IdEjercicio = EjercicioCombox.SelectedValue.ToString();
                rutina.IdCliente = ClienteCombox.SelectedValue.ToString();
            }
            catch { }

            bool validation = new Helps.DataValidation(rutina).Validate();

            if (validation == true)
            {
                string result = rutina.Savechanges();
                MessageBox.Show(result);

                RutinaUControl control = new RutinaUControl();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Dashboard))
                    {
                        (window as Dashboard).SwitchScreen(control, "Usuarios");
                    }
                }
                //message = result;
                //DialogResult = true;
            }
        }

        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            RutinaUControl control = new RutinaUControl();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(control, "Usuarios");
                }
            }
        }

        private void EjercicioCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EjercicioDataGrid.Items.Clear();
            EjercicioModel selectedModel = (EjercicioModel)EjercicioCombox.SelectedItem;
            EjercicioDataGrid.Items.Add(selectedModel);
        }

        private void ClienteCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClienteDataGrid.Items.Clear();
            ClienteModel selectedModel = (ClienteModel)ClienteCombox.SelectedItem;
            ClienteDataGrid.Items.Add(selectedModel);
        }
    }
}
