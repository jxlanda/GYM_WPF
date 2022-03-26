using Common.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class LoginRepository : MasterRepository
    {
        protected bool Login(string transactSql)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = transactSql;
                    command.CommandType = CommandType.Text;
                    foreach (SqlParameter item in parameters)
                    {
                        command.Parameters.Add(item);
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserCache.Id = reader.GetInt32(0);
                            UserCache.Nombre = reader.GetString(1);
                            UserCache.ApellidoPaterno = reader.GetString(2);
                            UserCache.ApellidoMaterno = reader.GetString(3);
                            UserCache.Apodo = reader.GetString(4);
                            UserCache.ImgPath = reader.GetString(6);
                            UserCache.IdTipoUsuario = reader.GetInt32(10);
                            UserCache.NombreTipoUsuario = reader.GetString(11);
                            UserCache.TipoUsuario = Convert.ToChar(reader.GetString(12));
                        }
                        parameters.Clear();
                        return true;
                    }
                    else
                    {
                        parameters.Clear();
                        return false;
                    }
                }
            }
        }
    }
}
