using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class CompradorViewModels
    {
        public class CompradorCreateViewModel
        {
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
        }
        public class CompradorEditViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
        }
    }
}