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
    public class TipoUsuarioModel
    {
        private readonly ITipoUsuarioRepository tipoUsuarioRepository;
        private List<TipoUsuarioModel> listTiposUsuarios;

        private string id;
        private string nombre;
        private string tipoUsuario;

        public EntityState EntityState { private get; set; }

        public string Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "El campo Tipo de Usuario es obligatorio")]
        public string TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }

        public TipoUsuarioModel()
        {
            tipoUsuarioRepository = new TipoUsuarioRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var tipoUsuarioDataModel = new TipoUsuario
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    Tipo = Convert.ToChar(tipoUsuario)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        tipoUsuarioRepository.Add(tipoUsuarioDataModel);
                        message = "Registro agregado.";
                        break;
                    case EntityState.Modified:
                        tipoUsuarioRepository.Edit(tipoUsuarioDataModel);
                        message = "Registro editado.";
                        break;
                    case EntityState.Deleted:
                        tipoUsuarioRepository.Remove(Convert.ToInt32(id));
                        message = "Registro eliminado.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Registro repetido.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<TipoUsuarioModel> GetAll()
        {
            var tipoUsuarioDataModel = tipoUsuarioRepository.GetAll();
            listTiposUsuarios = new List<TipoUsuarioModel>();
            foreach (TipoUsuario item in tipoUsuarioDataModel)
            {
                listTiposUsuarios.Add(new TipoUsuarioModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    tipoUsuario = item.Tipo.ToString()
                });
            }
            return listTiposUsuarios;
        }
        public IEnumerable<TipoUsuarioModel> FindBy(string filter)
        {
            return listTiposUsuarios.FindAll(e => e.id.Contains(filter) || 
                                                  e.nombre.Contains(filter) || 
                                                  e.tipoUsuario.Contains(filter));
        }
    }
}
