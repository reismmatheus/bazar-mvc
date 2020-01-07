using Bazar.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class VendaResult : BaseResult
    {
        public Venda Venda { get; set; } 
        public VendaResult()
        {
            Venda = new Venda();
        }
    }
}
