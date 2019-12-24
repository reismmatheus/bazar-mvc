using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class VendasViewModels
    {
        public class VendasCreateViewModel
        {
            public string ValorTotal { get; set; }
            public string Comprador { get; set; }
            public int ProdutoSelecionado { get; set; }
            public List<ProdutosModel> ListaProdutosEscolhidos { get; set; }
            public List<ProdutosModel> ListaProdutos { get; set; }
            public List<CompradorModel> ListaCompradores { get; set; }
            public VendasCreateViewModel()
            {
                ListaProdutosEscolhidos = new List<ProdutosModel>();
                ListaProdutos = new List<ProdutosModel>();
                ListaCompradores = new List<CompradorModel>();
            }
        }
    }
}