using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class AspNetUsersModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Tipo { get; set; }
    }
}