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
    public class EjercicioModel
    {
        private readonly IEjercicioRepository ejercicioRepository;
        private List<EjercicioModel> listEjercicios;

        private string id;
        private string nombre;
        private string descripcion;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public EjercicioModel()
        {
            ejercicioRepository = new EjercicioRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var ejercicioDataModel = new Ejercicio
                {
                    Id = Convert.ToInt32(id),
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        ejercicioRepository.Add(ejercicioDataModel);
                        message = "Ejercicio agregado.";
                        break;
                    case EntityState.Modified:
                        ejercicioRepository.Edit(ejercicioDataModel);
                        message = "Ejercicio editado.";
                        break;
                    case EntityState.Deleted:
                        ejercicioRepository.Remove(Convert.ToInt32(id));
                        message = "Ejercicio eliminado.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Ejercicio repetido.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<EjercicioModel> GetAll()
        {
            var ejercicioDataModel = ejercicioRepository.GetAll();
            listEjercicios = new List<EjercicioModel>();
            foreach (Ejercicio item in ejercicioDataModel)
            {
                listEjercicios.Add(new EjercicioModel
                {
                    id = item.Id.ToString(),
                    nombre = item.Nombre,
                    descripcion = item.Descripcion
                });
            }
            return listEjercicios;
        }
        public IEnumerable<EjercicioModel> FindBy(string filter)
        {
            return listEjercicios.FindAll(e => e.id.Contains(filter) ||
                                               e.nombre.Contains(filter) ||
                                               e.descripcion.Contains(filter));
        }
    }
}
