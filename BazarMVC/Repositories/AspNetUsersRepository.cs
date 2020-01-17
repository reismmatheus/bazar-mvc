using Bazar.Class;
using BazarMVC.Repositories.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories
{
    public class AspNetUsersRepository
    {
        private readonly string _connectionString;

        public AspNetUsersRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<AspNetUsersModel> GetUsuarios(List<Vendedor> listaVendedor = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM AspNetUsers";

                var result = connection.Query<AspNetUsersModel>(query);
                List<AspNetUsersModel> listaUsuariosVendedor = new List<AspNetUsersModel>();

                if(listaVendedor != null)
                {
                    foreach (var item in listaVendedor)
                    {
                        var usuarioVendedor = result.Where(x => x.Id == item.IdUser);
                        listaUsuariosVendedor.AddRange(usuarioVendedor.ToList());
                    }
                    return listaUsuariosVendedor;
                }
                return result;
            }
        }
        public AspNetUsersModel GetUsuario(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @" SELECT * FROM AspNetUsers WHERE id='" + id + "'";
                
                return connection.Query<AspNetUsersModel>(query).FirstOrDefault();
            }
        }
    }
}