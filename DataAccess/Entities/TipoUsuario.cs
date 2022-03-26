using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TipoUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public char Tipo { get; set; }
    }
}
