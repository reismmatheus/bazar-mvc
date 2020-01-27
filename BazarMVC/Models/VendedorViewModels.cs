using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class VendedorViewModels
    {
        public class VendedorEditViewModel
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