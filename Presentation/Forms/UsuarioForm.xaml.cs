using Domain.Models;
using Domain.ValueObjects;
using Microsoft.Win32;
using Presentation.UserControls;
using Presentation.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para UsuarioForm.xaml
    /// </summary>
    public partial class UsuarioForm : UserControl
    {
        private UsuarioModel usuario = new UsuarioModel();
        private TipoUsuarioModel tipoUsuario = new TipoUsuarioModel();
        private string imagenpath;
        bool isModifying = false;
        string idUsuario;
        public string message;
        
        public UsuarioForm()
        {
            InitializeComponent();
            SetMaxValues();
            ListTipoUsuarios();
            FillGenero();
        }
        private void SetMaxValues()
        {
            NombreTextBox.MaxLength = 20;
            APaternoTextBox.MaxLength = 20;
            AMaternoTextBox.MaxLength = 20;
            ApodoTextBox.MaxLength = 20;
            PinTextBox.MaxLength = 20;
            CorreoTextBox.MaxLength = 150;
            TelefonoTextBox.MaxLength = 20;

        }
        private void ListTipoUsuarios()
        {
            TipoUsuarioCombox.ItemsSource = tipoUsuario.GetAll();
            TipoUsuarioCombox.DisplayMemberPath = "Nombre";
            TipoUsuarioCombox.SelectedValuePath = "Id";
            //TipoUsuarioCombox.SelectedIndex = 1;
        }
        private void FillGenero()
        {
            GeneroCombox.Items.Add("Masculino");
            GeneroCombox.Items.Add("Femenino");
        }
        public void SetData(string id, string imgPath, string apodo, string pin, string idTipoUsuario, string nombre, string aPaterno, string aMaterno, string correo, string telefono, string genero)
        {
            isModifying = true;
            idUsuario = id;
            imagenpath = imgPath;
            try
            {
                ImagenBrush.ImageSource = new BitmapImage(new Uri(imgPath));
            }
            catch
            {
                MessageBox.Show("No se encontro imagen");
            }
            ApodoTextBox.Text = apodo;
            PinTextBox.Password = pin;
            TipoUsuarioCombox.SelectedValue = idTipoUsuario;
            NombreTextBox.Text = nombre;
            APaternoTextBox.Text = aPaterno;
            AMaternoTextBox.Text = aMaterno;
            CorreoTextBox.Text = correo;
            TelefonoTextBox.Text = telefono;
            if (genero.Equals("M"))
            {
                GeneroCombox.SelectedIndex = 0;
            }
            else
                GeneroCombox.SelectedIndex = 1;
            /*try
            {
                FNacimientoPicker.SelectedDate = Convert.ToDateTime(fNacimiento);
            }
            catch
            {
                MessageBox.Show("Es administrador no cliente");
            }
            PesoTextBox.Text = peso;
            EstaturaTextBox.Text = estatura;*/
        }

        private void GuardarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isModifying == true)
            {
                usuario.EntityState = EntityState.Modified;
                usuario.Id = idUsuario;
            }
            else
                usuario.EntityState = EntityState.Added;

            usuario.Apodo = ApodoTextBox.Text;
            usuario.Pin = PinTextBox.Password;
            try 
            { 
                usuario.IdTipoUsuario = TipoUsuarioCombox.SelectedValue.ToString();
            }
            catch { }
            usuario.Nombre = NombreTextBox.Text;
            usuario.ApellidoPaterno = APaternoTextBox.Text;
            usuario.ApellidoMaterno = AMaternoTextBox.Text;
            usuario.Correo = CorreoTextBox.Text;
            usuario.ImgPath = imagenpath;
            usuario.Telefono = TelefonoTextBox.Text;
            try
            {
                usuario.Genero = GeneroCombox.Text.Substring(0, 1);
            }
            catch { }

            bool validation = new Helps.DataValidation(usuario).Validate();

            if (validation == true)
            {
                string result = usuario.Savechanges();
                MessageBox.Show(result);

                UsuarioUControl control = new UsuarioUControl();
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

        private void SubirImagenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Selecciona una imagen",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };

            string appPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (op.ShowDialog() == true)
            {
                try
                {
                    string iName = op.SafeFileName;
                    string filepath = op.FileName;
                    if (!File.Exists(appPath + iName))
                    {
                        File.Copy(filepath, appPath + iName);
                        imagenpath = appPath + iName;
                        ImagenBrush.ImageSource = new BitmapImage(new Uri(op.FileName));
                    }
                    else
                    {
                        MessageBox.Show("El archivo ya existe");
                        imagenpath = appPath + iName;
                        ImagenBrush.ImageSource = new BitmapImage(new Uri(op.FileName));
                    }
                    //ImgPathBox.Text = appPath + iName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("No es posible abrir el archivo " + exp.Message);
                }
            }
        }

        private void CancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuarioUControl control = new UsuarioUControl();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(control, "Usuarios");
                }
            }
        }
    }
}
