using Bazar.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ListaVendedorResult : BaseResult
    {
        [JsonProperty(PropertyName = "Vendedores")]
        public List<Vendedor> ListaVendedor { get; set; }
        public ListaVendedorResult()
        {
            ListaVendedor = new List<Vendedor>();
        }
    }
}
