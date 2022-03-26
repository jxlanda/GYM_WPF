using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Usuario
    {
        public int Id;
        public string Nombre;
        public string ApellidoPaterno;
        public string ApellidoMaterno;
        public string Apodo;
        public string Pin;
        public string ImgPath;
        public string Correo;
        public string Telefono;
        public char Genero;
        public int IdTipoUsuario;
        public string NombreTipoUsuario;
        public char TipoUsuario;
    }
}
