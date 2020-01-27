using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class UsuariosViewModels
    {
        public class UsuariosCreateViewModel
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string ConfirmarSenha { get; set; }
        }
        public class UsuariosEditViewModel
        {
            public string Id { get; set; }
            [Required]
            public string Nome { get; set; }
            [Required]
            public string Sobrenome { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}