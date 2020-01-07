using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class EstatisticaResult : BaseResult
    {
        public int Compradores { get; set; }
        public int Vendedores { get; set; }
        public int Produtos { get; set; }
        public int ProdutosVendidos { get; set; }
        public int Vendas { get; set; }
        public int ProdutosPagos { get; set; }
    }
}
