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

        public IEnumerable<AspNetUsersModel> GetUsuarios()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"select *, AspNetRoles.Name AS Tipo from AspNetUsers
                            inner join AspNetUserRoles
                            on AspNetUsers.Id = AspNetUserRoles.UserId
                            inner join AspNetRoles
                            on AspNetRoles.Id = AspNetUserRoles.RoleId";
                
                return connection.Query<AspNetUsersModel>(query);
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