using Common.Cache;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.UserControls
{
    /// <summary>
    /// Lógica de interacción para ClienteRutinasUControl.xaml
    /// </summary>
    public partial class CRutinasUControl : UserControl
    {
        private RutinaModel rutina = new RutinaModel();
        public CRutinasUControl()
        {
            InitializeComponent();
            ListRutinas();
        }
        private void ListRutinas()
        {
            try
            {
                DataGrid dataGrid = new DataGrid
                {
                    ItemsSource = rutina.GetAll()
                };

                LUNESItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(),"LUNES");
                MARTESItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "MARTES");
                MIERCOLESItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "MIERCOLES");
                JUEVESItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "JUEVES");
                VIERNESItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "VIERNES");
                SABADOItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "SABADO");
                DOMINGOItemsControl.ItemsSource = rutina.FindByClienteDia(CustomerCache.Id.ToString(), "DOMINGO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
