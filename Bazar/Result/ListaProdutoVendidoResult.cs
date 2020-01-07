using Bazar.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ListaProdutoVendidoResult : BaseResult
    {
        [JsonProperty(PropertyName = "ProdutosVendidos")]
        public List<ProdutoVendido> ListaProdutoVendido { get; set; }
        public ListaProdutoVendidoResult()
        {
            ListaProdutoVendido = new List<ProdutoVendido>();
        }
    }
}
