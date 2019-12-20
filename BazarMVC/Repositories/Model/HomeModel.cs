using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class HomeModel
    {
        public int Vendas { get; set; }
        public int Produtos { get; set; }
        public int ProdutosVendidos { get; set; }
        public int Compradores { get; set; }
        public int Vendedores { get; set; }
        public int ProdutosPagos { get; set; }
    }
}