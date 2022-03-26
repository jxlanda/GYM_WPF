using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;

namespace Domain.Models
{
    public class UsuarioModel
    {
        private readonly IUsuarioRepository usuarioRepository;
        private List<UsuarioModel> listUsuarios;

        private string id;
        private string nombre;
        private string apellidoPaterno;
        private string apellidoMaterno;
        private string apodo;
        private string pin;
        private string imgPath;
        private string correo;
        private string telefono;
        private string genero;
        private string idTipoUsuario;
        private string nombreTipoUsuario;
        private string tipoUsuario;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El Nombre es necesario")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "{El campo Apellido Paterno es necesario}")]
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
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required(ErrorMessage = "El campo genero es obligatorio")]
        public string Genero { get => genero; set => genero = value; }
        [Required(ErrorMessage = "El campo Tipo de Usuario es obligatorio")]
        public string IdTipoUsuario { get => idTipoUsuario; set => idTipoUsuario = value; }
        public string NombreTipoUsuario { get => nombreTipoUsuario; set => nombreTipoUsuario = value; }
        public string TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }

        public UsuarioModel()
        {
            usuarioRepository = new UsuarioRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var usuarioDataModel = new Usuario
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Apodo = apodo,
                    Pin = pin,
                    ImgPath = imgPath,
                    Correo = correo,
                    Telefono = telefono,
                    Genero = Convert.ToChar(genero),
                    IdTipoUsuario = Convert.ToInt32(idTipoUsuario)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        usuarioRepository.Add(usuarioDataModel);
                        message = "Usuario agregado.";
                        break;
                    case EntityState.Modified:
                        usuarioRepository.Edit(usuarioDataModel);
                        message = "Usuario editado.";
                        break;
                    case EntityState.Deleted:
                        usuarioRepository.Remove(Convert.ToInt32(id));
                        message = "Usuario eliminado.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Usuario repetido.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<UsuarioModel> GetAll()
        {
            var usuarioDataModel = usuarioRepository.GetAll();
            listUsuarios = new List<UsuarioModel>();
            foreach (Usuario item in usuarioDataModel)
            {
                listUsuarios.Add(new UsuarioModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    apellidoPaterno = item.ApellidoPaterno,
                    apellidoMaterno = item.ApellidoMaterno,
                    apodo = item.Apodo,
                    pin = item.Pin,
                    imgPath = item.ImgPath,
                    correo = item.Correo,
                    telefono = item.Telefono,
                    genero = item.Genero.ToString(),
                    idTipoUsuario = item.IdTipoUsuario.ToString(),
                    nombreTipoUsuario = item.NombreTipoUsuario,
                    tipoUsuario = item.TipoUsuario.ToString()
                });
            }
            return listUsuarios;
        }
        public bool LoginUser(string user, string pin)
        {
            return usuarioRepository.Login(user, pin);
        }
        public IEnumerable<UsuarioModel> FindBy(string filter)
        {
            return listUsuarios.FindAll(e => e.id.Contains(filter) ||
                                             e.nombre.Contains(filter) ||
                                             e.ApellidoPaterno.Contains(filter) ||
                                             e.apellidoMaterno.Contains(filter) ||
                                             e.apodo.Contains(filter) ||
                                             e.pin.Contains(filter) ||
                                             e.correo.Contains(filter) ||
                                             e.idTipoUsuario.Contains(filter) ||
                                             e.nombreTipoUsuario.Contains(filter) ||
                                             e.tipoUsuario.Contains(filter));
        }
    }
}
