using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class ProdutosVendidosModel
    {
        public int Id { get; set; }
        public float PrecoPago { get; set; }
        public bool Status { get; set; }
        public int Quantidade { get; set; }
        public string Produto { get; set; }
        public string Vendedor { get; set; }
        public string Comprador { get; set; }
    }
}