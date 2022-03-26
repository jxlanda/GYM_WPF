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
    public class TipoUsuarioRepository : MasterRepository, ITipoUsuarioRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        public TipoUsuarioRepository()
        {
            select = "SELECT * FROM TipoUsuario";
            insert = "INSERT INTO TipoUsuario values(@Nombre, @TipoUsuario)";
            update = "UPDATE TipoUsuario SET nombre=@Nombre, tipousuario=@TipoUsuario WHERE id=@Id";
            delete = "DELETE FROM TipoUsuario WHERE id=@Id";
        }
        public int Add(TipoUsuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@TipoUsuario", entity.Tipo),
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(TipoUsuario entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@TipoUsuario", entity.Tipo)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<TipoUsuario> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listTiposUsuarios = new List<TipoUsuario>();
            foreach (DataRow item in tableResult.Rows)
            {
                listTiposUsuarios.Add(new TipoUsuario
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Tipo = Convert.ToChar(item[2])
                });
            }
            return listTiposUsuarios;
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
