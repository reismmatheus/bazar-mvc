using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class VendedorViewModels
    {
        public class VendedorCreateViewModel
        {
            public string Nome { get; set; }
        }
        public class VendedorEditViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
    }
}