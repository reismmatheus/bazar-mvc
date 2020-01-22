using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
}