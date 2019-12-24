using Bazar.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Result
{
    public class CompradorResult : BaseResult
    {
        public Comprador Comprador { get; set; }

        public CompradorResult()
        {
            Comprador = new Comprador();
        }
    }
}
