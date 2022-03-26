using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public class CustomerCache
    {
        public static int Id { get; set; }
        public static string Nombre { get; set; }
        public static string ApellidoPaterno { get; set; }
        public static string ApellidoMaterno { get; set; }
        public static string Apodo { get; set; }
        public static string Pin { get; set; }
        public static string ImgPath { get; set; }
        public static string Correo { get; set; }
        public static string FechaNacimiento { get; set; }
        public static decimal Peso { get; set; }
        public static int Estatura { get; set; }
        public static char Genero { get; set; }
    }
}
