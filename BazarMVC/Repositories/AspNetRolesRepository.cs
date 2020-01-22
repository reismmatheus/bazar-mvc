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
    public class AspNetRolesRepository
    {
        private readonly string _connectionString;

        public AspNetRolesRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IEnumerable<AspNetRolesModel> GetTiposUsuario()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"select * from AspNetRoles";

                return connection.Query<AspNetRolesModel>(query);
            }
        }
    }
}