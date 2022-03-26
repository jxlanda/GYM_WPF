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
    public class RutinaRepository : MasterRepository, IRutinaRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        public RutinaRepository()
        {
            select = "SELECT Rutina.id, dia, repeticiones, Rutina.peso, Ejercicio.id, Ejercicio.nombre, Ejercicio.descripcion, Cliente.id, Cliente.nombre FROM Rutina INNER JOIN Ejercicio ON Rutina.ejercicio_id = Ejercicio.id INNER JOIN Cliente ON Rutina.cliente_id = Cliente.id";
            insert = "INSERT INTO Rutina VALUES (@Dia,@Repeticiones,@Peso,@IdEjercicio,@IdCliente)";
            update = "UPDATE Rutina SET dia=@Dia,repeticiones=@Repeticiones,peso=@Peso,ejercicio_id=@IdEjercicio,cliente_id=@IdCliente WHERE id=@Id";
            delete = "DELETE FROM Rutina WHERE id=@Id";
        }
        public int Add(Rutina entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Dia", entity.Dia),
                new SqlParameter("@Repeticiones", entity.Repeticiones),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@IdEjercicio", entity.IdEjercicio),
                new SqlParameter("@IdCliente",entity.IdCliente)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Rutina entity)
        {
            parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Dia", entity.Dia),
                new SqlParameter("@Repeticiones", entity.Repeticiones),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@IdEjercicio", entity.IdEjercicio),
                new SqlParameter("@IdCliente",entity.IdCliente)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Rutina> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listRutinas= new List<Rutina>();
            foreach (DataRow item in tableResult.Rows)
            {
                listRutinas.Add(new Rutina
                {
                    Id = Convert.ToInt32(item[0]),
                    Dia = item[1].ToString(),
                    Repeticiones = Convert.ToInt32(item[2]),
                    Peso = Convert.ToDecimal(item[3]),
                    IdEjercicio = Convert.ToInt32(item[4]),
                    EjercicioNombre = item[5].ToString(),
                    EjercicioDescripcion = item[6].ToString(),
                    IdCliente = Convert.ToInt32(item[7]),
                    ClienteNombre = item[8].ToString()
                }); ;
            }
            return listRutinas;
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
