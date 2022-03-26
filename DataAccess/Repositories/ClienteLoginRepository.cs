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
    public abstract class ClienteLoginRepository : MasterRepository
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
                            CustomerCache.Id = reader.GetInt32(0);
                            CustomerCache.Nombre = reader.GetString(1);
                            CustomerCache.ApellidoPaterno = reader.GetString(2);
                            CustomerCache.ApellidoMaterno = reader.GetString(3);
                            CustomerCache.Apodo = reader.GetString(4);
                            CustomerCache.Pin = reader.GetString(5);
                            CustomerCache.ImgPath = reader.GetString(6);
                            CustomerCache.Correo = reader.GetString(7);
                            CustomerCache.FechaNacimiento = reader.GetDateTime(8).ToString().Substring(0,10);
                            CustomerCache.Peso = reader.GetDecimal(9);
                            CustomerCache.Estatura = reader.GetInt32(10);
                            CustomerCache.Genero = Convert.ToChar(reader.GetString(11));
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
