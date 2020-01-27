using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class VendasViewModels
    {
        public class VendasCreateViewModel
        {
            public float ValorTotal { get; set; }
            public string NomeComprador { get; set; }
            public string Comprador { get; set; }
            public int ProdutoSelecionado { get; set; }
            public ProdutosModel ProdutoEscolhido { get; set; }
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

        public class VendasDetailsViewModel
        {
            public int Id { get; set; }
            public float ValorTotal { get; set; }
            public string NomeComprador { get; set; }
            public string IdComprador { get; set; }
            public List<ProdutosVendidosModel> ListaProdutos { get; set; }
            public VendasDetailsViewModel()
            {
                ListaProdutos = new List<ProdutosVendidosModel>();
            }
        }
    }
}