using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarMVC.Models
{
    public class ProdutosViewModels
    {
        public class ProdutosCreateViewModel
        {
            public string Nome { get; set; }
            public string Preco { get; set; }
            public int Quantidade { get; set; }
            public string IdVendedor { get; set; }
            public string NomeVendedor { get; set; }
            public string Descricao { get; set; }
            public List<VendedorModel> ListaVendedores { get; set; }
            public ProdutosCreateViewModel()
            {
                ListaVendedores = new List<VendedorModel>();
            }
         }

        public class ProdutosDetailsViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public float Preco { get; set; }
            public int Quantidade { get; set; }
            public string Descricao { get; set; }
            public string IdVendedor { get; set; }
            public string NomeVendedor { get; set; }
        }


        public class ProdutosEditViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public float Preco { get; set; }
            public int Quantidade { get; set; }
            public string Descricao { get; set; }
            public string IdVendedor { get; set; }
            public string NomeVendedor { get; set; }
            public List<VendedorModel> ListaVendedores { get; set; }
            public ProdutosEditViewModel()
            {
                ListaVendedores = new List<VendedorModel>();
            }
        }
    }
}