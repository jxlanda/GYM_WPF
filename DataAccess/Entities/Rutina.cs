using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Rutina
    {
        public int Id;
        public string Dia;
        public int Repeticiones;
        public decimal Peso;
        public int IdEjercicio;
        public string EjercicioNombre;
        public string EjercicioDescripcion;
        public int IdCliente;
        public string ClienteNombre;
    }
}
