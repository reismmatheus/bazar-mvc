﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class ProdutosViewModels
    {
        public class ProdutosCreateViewModel
        {
            public string Nome { get; set; }
            public float Preco { get; set; }
            public int Quantidade { get; set; }
            public string Vendedor { get; set; }
            public string Descricao { get; set; }
            public List<string> ListaVendedores { get; set; }
            public ProdutosCreateViewModel()
            {
                ListaVendedores = new List<string>();
            }
         }
    }
}