using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache
{
    public static class UserCache
    {
        public static int Id { get; set; }
        public static string Nombre { get; set; }
        public static string ApellidoPaterno { get; set; }
        public static string ApellidoMaterno { get; set; }
        public static string Apodo { get; set; }
        public static string ImgPath { get; set; }
        public static int IdTipoUsuario { get; set; }
        public static string NombreTipoUsuario { get; set; }
        public static char TipoUsuario { get; set; }
    }
}
