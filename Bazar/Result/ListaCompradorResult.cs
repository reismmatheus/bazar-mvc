using Bazar.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ListaCompradorResult : BaseResult
    {
        [JsonProperty(PropertyName = "Compradores")]
        public List<Comprador> ListaComprador { get; set; }
        public ListaCompradorResult()
        {
            ListaComprador = new List<Comprador>();
        }
    }
}
