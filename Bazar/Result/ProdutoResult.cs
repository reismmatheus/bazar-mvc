using Bazar.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class ProdutoResult : BaseResult
    {
        public Produto Produto { get; set; }
        public ProdutoResult()
        {
            Produto = new Produto();
        }
    }
}
