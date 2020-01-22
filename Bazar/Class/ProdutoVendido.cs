using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Class
{
    public class ProdutoVendido
    {
        public int Id { get; set; }
        public float PrecoPago { get; set; }
        public bool Status { get; set; }
        public int Quantidade { get; set; }
        public int IdProduto { get; set; }
        public int IdVenda { get; set; }
    }
}
