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
    public class ClienteRepository : ClienteLoginRepository, IClienteRepository
    {
        private readonly string select;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        private readonly string login;
        public ClienteRepository()
        {
            select = "SELECT * FROM Cliente";
            insert = "INSERT INTO Cliente VALUES (@Nombre,@APaterno,@AMaterno,@Apodo,@Pin,@ImgPath,@Correo,@FNacimiento,@Peso,@Estatura,@Genero)";
            update = "UPDATE Cliente SET nombre=@Nombre,apaterno=@APaterno,amaterno=@AMaterno,apodo=@Apodo,pin=@Pin,imgpath=@ImgPath,correo=@Correo,fnacimiento=@FNacimiento,peso=@Peso,estatura=@Estatura,genero=@Genero WHERE id=@Id";
            delete = "DELETE FROM Cliente WHERE id=@Id";
            login = "SELECT * FROM Cliente WHERE apodo =@Apodo AND pin =@Pin";
        }
        public int Add(Cliente entity)
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
                new SqlParameter("@FNacimiento", entity.FechaNacimiento),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@Estatura", entity.Estatura),
                new SqlParameter("@Genero", entity.Genero)
            };
            return ExecuteNonQuery(insert);
        }

        public int Edit(Cliente entity)
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
                new SqlParameter("@FNacimiento", entity.FechaNacimiento),
                new SqlParameter("@Peso", entity.Peso),
                new SqlParameter("@Estatura", entity.Estatura),
                new SqlParameter("@Genero", entity.Genero)
            };
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Cliente> GetAll()
        {
            var tableResult = ExecuteReader(select);
            var listClientes = new List<Cliente>();
            foreach (DataRow item in tableResult.Rows)
            {
                listClientes.Add(new Cliente
                {
                    Id = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    ApellidoPaterno = item[2].ToString(),
                    ApellidoMaterno = item[3].ToString(),
                    Apodo = item[4].ToString(),
                    Pin = item[5].ToString(),
                    ImgPath = item[6].ToString(),
                    Correo = item[7].ToString(),
                    FechaNacimiento = item[8].ToString(),
                    Peso = Convert.ToDecimal(item[9]),
                    Estatura = Convert.ToInt32(item[10]),
                    Genero = Convert.ToChar(item[11])

                }); ;
            }
            return listClientes;
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
