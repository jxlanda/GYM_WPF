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
    public class EjercicioRepository : MasterRepository, IEjercicioRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;

        public EjercicioRepository()
        {
            select = "SELECT * FROM Ejercicio";
            insert = "INSERT INTO Ejercicio VALUES (@Nombre,@Descripcion)";
            update = "UPDATE Ejercicio SET nombre=@Nombre,descripcion=@Descripcion WHERE id=@Id";
            delete = "DELETE FROM Ejercicio WHERE id=@Id";
        }
        public int Add(Ejercicio entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Descripcion", entity.Descripcion)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Ejercicio entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Descripcion", entity.Descripcion)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Ejercicio> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listEjercicios = new List<Ejercicio>();
            foreach (DataRow item in tableResult.Rows)
            {
                listEjercicios.Add(new Ejercicio
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Descripcion = item[2].ToString()

                }); ;
            }
            return listEjercicios;
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
