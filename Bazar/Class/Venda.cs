using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Class
{
    public class Venda
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "Produtos")]
        public List<ProdutoVendido> ListaProdutoVendido { get; set; }
        public float ValorTotal { get; set; }
        public int IdComprador { get; set; }
        public Venda()
        {
            ListaProdutoVendido = new List<ProdutoVendido>();
        }
    }
}
