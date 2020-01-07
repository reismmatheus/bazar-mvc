using Bazar.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class VendedorResult : BaseResult
    {
        public Vendedor Vendedor { get; set; }
        public VendedorResult()
        {
            Vendedor = new Vendedor();
        }
    }
}
