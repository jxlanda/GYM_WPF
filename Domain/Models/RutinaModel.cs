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
    public class RutinaModel
    {
        private readonly IRutinaRepository rutinaRepository;
        private List<RutinaModel> listRutinas;

        private string id;
        private string dia;
        private string repeticiones;
        private string peso;
        private string idEjercicio;
        private string ejercicioNombre;
        private string ejercicioDescripcion;
        private string idCliente;
        private string clienteNombre;

        public EntityState EntityState { private get; set; }
        public string Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El campo Dia es obligatorio")]
        public string Dia { get => dia; set => dia = value; }
        [Required(ErrorMessage = "El campo Repeticiones es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Repeticiones solo permite numeros")]
        public string Repeticiones { get => repeticiones; set => repeticiones = value; }
        [Required(ErrorMessage = "El campo Peso es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Peso solo permite numeros")]
        public string Peso { get => peso; set => peso = value; }
        [Required(ErrorMessage = "El campo Ejercicio es obligatorio")]
        public string IdEjercicio { get => idEjercicio; set => idEjercicio = value; }
        public string EjercicioNombre { get => ejercicioNombre; set => ejercicioNombre = value; }
        public string EjercicioDescripcion { get => ejercicioDescripcion; set => ejercicioDescripcion = value; }
        [Required(ErrorMessage = "El campo Cliente es obligatorio")]
        public string IdCliente { get => idCliente; set => idCliente = value; }
        public string ClienteNombre { get => clienteNombre; set => clienteNombre = value; }

        public RutinaModel()
        {
            rutinaRepository = new RutinaRepository();
        }
        public string Savechanges()
        {
            string message = null;
            try
            {
                var rutinaDataModel = new Rutina
                {
                    Id = Convert.ToInt32(id),
                    Dia = dia,
                    Repeticiones = Convert.ToInt32(repeticiones),
                    Peso = Convert.ToDecimal(peso),
                    IdEjercicio = Convert.ToInt32(idEjercicio),
                    IdCliente = Convert.ToInt32(idCliente)
                };

                switch (EntityState)
                {
                    case EntityState.Added:
                        rutinaRepository.Add(rutinaDataModel);
                        message = "Rutina agregada.";
                        break;
                    case EntityState.Modified:
                        rutinaRepository.Edit(rutinaDataModel);
                        message = "Rutina agregada.";
                        break;
                    case EntityState.Deleted:
                        rutinaRepository.Remove(Convert.ToInt32(id));
                        message = "Rutina agregada.";
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 2627)
                    message = "Rutina repetida.";
                else
                    message = ex.ToString();
            }
            return message;
        }
        public List<RutinaModel> GetAll()
        {
            var rutinaDataModel = rutinaRepository.GetAll();
            listRutinas = new List<RutinaModel>();
            foreach (Rutina item in rutinaDataModel)
            {
                listRutinas.Add(new RutinaModel
                {
                    id = item.Id.ToString(),
                    dia = item.Dia,
                    repeticiones = item.Repeticiones.ToString(),
                    peso = item.Peso.ToString(),
                    idEjercicio = item.IdEjercicio.ToString(),
                    ejercicioNombre = item.EjercicioNombre.ToString(),
                    ejercicioDescripcion = item.EjercicioDescripcion.ToString(),
                    idCliente = item.IdCliente.ToString(),
                    clienteNombre = item.ClienteNombre.ToString()

                });
            }
            return listRutinas;
        }
        public IEnumerable<RutinaModel> FindBy(string filter)
        {
            return listRutinas.FindAll(e => e.id.Contains(filter) ||
                                             e.dia.Contains(filter) ||
                                             e.repeticiones.Contains(filter) ||
                                             e.peso.Contains(filter) ||
                                             e.idEjercicio.Contains(filter) ||
                                             e.idCliente.Contains(filter));
        }
        public IEnumerable<RutinaModel> FindByClienteDia(string idCliente, string dia)
        {
            return listRutinas.FindAll(e => e.idCliente.Equals(idCliente) && e.dia.Equals(dia));
        }
    }
}
