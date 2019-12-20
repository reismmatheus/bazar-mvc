using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class ProdutosModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public int Quantidade { get; set; }
        public string Vendedor { get; set; }
    }
}