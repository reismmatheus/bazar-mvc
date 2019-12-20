using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Repositories.Model
{
    public class VendasModel
    {
        public int Id { get; set; }
        public float ValorTotal { get; set; }
        public string Comprador { get; set; }
    }
}