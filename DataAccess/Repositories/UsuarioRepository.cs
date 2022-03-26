using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UsuarioRepository : LoginRepository, IUsuarioRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        private readonly string login;

        public UsuarioRepository()
        {
            select = "SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, telefono, genero, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id";
            insert = "INSERT INTO Usuario VALUES (@Nombre,@APaterno,@AMaterno,@Apodo,@Pin,@ImgPath,@Correo,@Telefono,@Genero,@IdTipoUsuario)";
            update = "UPDATE Usuario SET nombre=@Nombre, apaterno=@APaterno, amaterno=@AMaterno, apodo=@Apodo, pin=@Pin,imgpath=@ImgPath,correo=@Correo, telefono=@Telefono, genero=@Genero, tipousuario_id=@IdTipoUsuario WHERE id=@Id";
            delete = "DELETE FROM Usuario WHERE id=@Id";
            login = "SELECT Usuario.id, Usuario.nombre, apaterno, amaterno, apodo, pin, imgpath, correo, telefono, genero, TipoUsuario.id, TipoUsuario.nombre, tipousuario FROM Usuario INNER JOIN TipoUsuario ON Usuario.tipousuario_id = TipoUsuario.id WHERE Usuario.apodo =@Apodo AND Usuario.pin =@Pin";
        }
        public int Add(Usuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@APaterno", entity.ApellidoPaterno),
                new SqlParameter("@AMaterno", entity.ApellidoMaterno),
                new SqlParameter("@Apodo", entity.Apodo),
                new SqlParameter("@Pin", entity.Pin),
                new SqlParameter("@ImgPath",entity.ImgPath),
                new SqlParameter("@Correo", entity.Correo),
                new SqlParameter("@Telefono", entity.Telefono),
                new SqlParameter("@Genero", entity.Genero),
                new SqlParameter("@IdTipoUsuario", entity.IdTipoUsuario)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Usuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@APaterno", entity.ApellidoPaterno),
                new SqlParameter("@AMaterno", entity.ApellidoMaterno),
                new SqlParameter("@Apodo", entity.Apodo),
                new SqlParameter("@Pin", entity.Pin),
                new SqlParameter("@ImgPath",entity.ImgPath),
                new SqlParameter("@Correo", entity.Correo),
                new SqlParameter("@Telefono", entity.Telefono),
                new SqlParameter("@Genero", entity.Genero),
                new SqlParameter("@IdTipoUsuario", entity.IdTipoUsuario)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Usuario> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listUsuarios = new List<Usuario>();
            foreach (DataRow item in tableResult.Rows)
            {
                listUsuarios.Add(new Usuario
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    ApellidoPaterno = item[2].ToString(),
                    ApellidoMaterno = item[3].ToString(),
                    Apodo = item[4].ToString(),
                    Pin = item[5].ToString(),
                    ImgPath = item[6].ToString(),
                    Correo = item[7].ToString(),
                    Telefono = item[8].ToString(),
                    Genero = Convert.ToChar(item[9]),
                    IdTipoUsuario = Convert.ToInt32(item[10]),
                    NombreTipoUsuario = item[11].ToString(),
                    TipoUsuario = Convert.ToChar(item[12])

                });
            }
            return listUsuarios;
        }

        public bool Login(string apodo, string pin)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Apodo", apodo),
                new SqlParameter("@Pin", pin)
            };
            return Login(login);
        }

        public int Remove(int id)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            return ExecuteNonQuery(delete);
        }
    }
}
