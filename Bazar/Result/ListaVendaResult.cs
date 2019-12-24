using Bazar.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ListaVendaResult : BaseResult
    {
        [JsonProperty(PropertyName = "Vendas")]
        public List<Venda> ListaVenda { get; set; }
        public ListaVendaResult()
        {
            ListaVenda = new List<Venda>();
        }
    }
}
