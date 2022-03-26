using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClienteModel
    {
        private readonly IClienteRepository clienteRepository;
        private List<ClienteModel> listClientes;

        private string id;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string apodo;
        private string pin;
        private string imgPath;
        private string correo;
        private string fnacimiento;
        private string peso;
        private string estatura;
        private string genero;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El campo Nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string ApellidoPaterno { get => apellidoPaterno; set => apellidoPaterno = value; }
        [Required(ErrorMessage = "El campo Apellido Materno es obligatorio")]
        public string ApellidoMaterno { get => apellidoMaterno; set => apellidoMaterno = value; }
        [Required(ErrorMessage = "El campo Apodo es obligatorio")]
        public string Apodo { get => apodo; set => apodo = value; }
        [Required(ErrorMessage = "El campo Pin es obligatorio")]
        public string Pin { get => pin; set => pin = value; }
        public string ImgPath { get => imgPath; set => imgPath = value; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Correo { get => correo; set => correo = value; }
        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio")]
        public string FNacimiento { get => fnacimiento; set => fnacimiento = value; }
        [Required(ErrorMessage = "El campo Peso es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Peso solo permite numeros")]
        public string Peso { get => peso; set => peso = value; }
        [Required(ErrorMessage = "El campo Estatura es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Estatura solo permite numeros")]
        public string Estatura { get => estatura; set => estatura = value; }
        [Required(ErrorMessage = "El campo Genero es obligatorio")]
        public string Genero { get => genero; set => genero = value; }

        public ClienteModel()
        {
            clienteRepository = new ClienteRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var clienteDataModel = new Cliente
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Apodo = apodo,
                    Pin = pin,
                    ImgPath = imgPath,
                    Correo = correo,
                    FechaNacimiento = fnacimiento,
                    Peso = Convert.ToDecimal(peso),
                    Estatura = Convert.ToInt32(estatura),
                    Genero = Convert.ToChar(genero)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        clienteRepository.Add(clienteDataModel);
                        message = "Cliente agregado.";
                        break;
                    case EntityState.Modified:
                        clienteRepository.Edit(clienteDataModel);
                        message = "Cliente editado.";
                        break;
                    case EntityState.Deleted:
                        clienteRepository.Remove(Convert.ToInt32(id));
                        message = "Cliente eliminado.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Cliente repetido.";
                else if (ex is System.Data.SqlClient.SqlException sqlEx2 && sqlEx2.Number == 547)
                    message = "No se puede eliminar ya que tiene una rutina asignada.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<ClienteModel> GetAll()
        {
            var clienteDataModel = clienteRepository.GetAll();
            listClientes = new List<ClienteModel>();
            foreach (Cliente item in clienteDataModel)
            {
                listClientes.Add(new ClienteModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    apellidoPaterno = item.ApellidoPaterno,
                    apellidoMaterno = item.ApellidoMaterno,
                    apodo = item.Apodo,
                    pin = item.Pin,
                    imgPath = item.ImgPath,
                    correo = item.Correo,
                    fnacimiento = item.FechaNacimiento.Substring(0,10),
                    peso = item.Peso.ToString(),
                    estatura = item.Estatura.ToString(),
                    genero = item.Genero.ToString()
                });
            }
            return listClientes;
        }
        public bool LoginCustomer(string apodo, string pin)
        {
            return clienteRepository.Login(apodo, pin);
        }
        public IEnumerable<ClienteModel> FindBy(string filter)
        {
            return listClientes.FindAll(e => e.id.Contains(filter) ||
                                             e.nombre.Contains(filter) ||
                                             e.ApellidoPaterno.Contains(filter) ||
                                             e.apellidoMaterno.Contains(filter) ||
                                             e.apodo.Contains(filter) ||
                                             e.pin.Contains(filter) ||
                                             e.correo.Contains(filter) ||
                                             e.fnacimiento.Contains(filter) ||
                                             e.peso.Contains(filter) ||
                                             e.estatura.Contains(filter) ||
                                             e.genero.Contains(filter));
        }
    }
}
