using Bazar.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ListaProdutoResult : BaseResult
    {
        [JsonProperty(PropertyName = "Produtos")]
        public List<Produto> ListaProduto { get; set; }
        public ListaProdutoResult()
        {
            ListaProduto = new List<Produto>();
        }
    }
}
