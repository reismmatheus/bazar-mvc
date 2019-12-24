using Bazar.Interface;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.VendasViewModels;

namespace BazarMVC.Controllers
{
    public class VendasController : Controller
    {
        // GET: Vendas
        public ActionResult Index()
        {
            List<VendasModel> listaVendas = new List<VendasModel>();
            InterfaceBazar bazar = new InterfaceBazar();
            var getVendas = bazar.GetVendas();
            if (!getVendas.ProccessOk)
            {
                return View(listaVendas);
            }
            foreach (var item in getVendas.ListaVenda)
            {
                VendasModel vendas = new VendasModel();
                vendas.Id = item.Id;
                vendas.ValorTotal = item.ValorTotal;
                var nomeComprador = bazar.GetComprador(item.IdComprador.ToString());
                if (!nomeComprador.ProccessOk)
                {
                    return View(listaVendas);
                }
                vendas.Comprador = nomeComprador.Comprador.Nome;
                listaVendas.Add(vendas);
            }
            return View(listaVendas);
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            InterfaceBazar bazar = new InterfaceBazar();
            VendasCreateViewModel model = new VendasCreateViewModel();
            var compradores = bazar.GetCompradores();
            if (!compradores.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in compradores.ListaComprador)
            {
                CompradorModel comprador = new CompradorModel();
                comprador.Id = item.Id;
                comprador.Nome = item.Nome;
                model.ListaCompradores.Add(comprador);
            }

            var produtos = bazar.GetProdutos();
            if (!produtos.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in produtos.ListaProduto)
            {
                ProdutosModel produto = new ProdutosModel();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Preco = item.Preco;
                produto.Quantidade = item.Quantidade;
                var vendedor = bazar.GetVendedor(item.IdVendedor.ToString());
                if (!produtos.ProccessOk)
                {
                    return View(model);
                }
                produto.Vendedor = vendedor.Vendedor.Nome;
                model.ListaProdutos.Add(produto);
            }

            return View(model);
        }

        // POST: Vendas/Create
        [HttpPost]
        public ActionResult Create(VendasCreateViewModel model)
        {
            InterfaceBazar bazar = new InterfaceBazar();
            var compradores = bazar.GetCompradores();
            if (!compradores.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in compradores.ListaComprador)
            {
                CompradorModel comprador = new CompradorModel();
                comprador.Id = item.Id;
                comprador.Nome = item.Nome;
                model.ListaCompradores.Add(comprador);
            }

            var produtos = bazar.GetProdutos();
            if (!produtos.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in produtos.ListaProduto)
            {
                ProdutosModel produto = new ProdutosModel();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Preco = item.Preco;
                produto.Quantidade = item.Quantidade;
                var vendedor = bazar.GetVendedor(item.IdVendedor.ToString());
                if (!produtos.ProccessOk)
                {
                    return View(model);
                }
                produto.Vendedor = vendedor.Vendedor.Nome;
                model.ListaProdutos.Add(produto);
            }
            var compradorEscolhido = model.ListaCompradores.Where(a => a.Id.ToString() == model.Comprador);
            if(compradorEscolhido.FirstOrDefault() != null)
                model.Comprador = compradorEscolhido.FirstOrDefault().Nome;
            
            var produtoEscolhido = model.ListaProdutos.Where(a => a.Id == model.ProdutoSelecionado);
            model.ListaProdutosEscolhidos.Add(produtoEscolhido.First());

            var produtosDisponiveis = model.ListaProdutos.Where(y => y.Id.ToString() != produtoEscolhido.First().Id.ToString());
            model.ListaProdutos = produtosDisponiveis.ToList();

            return View(model);
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vendas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vendas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
