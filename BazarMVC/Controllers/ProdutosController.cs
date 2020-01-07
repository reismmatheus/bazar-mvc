using Bazar.Class;
using Bazar.Interface;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.ProdutosViewModels;

namespace BazarMVC.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            List<ProdutosModel> listaProdutos = new List<ProdutosModel>();
            InterfaceBazar bazar = new InterfaceBazar();
            var getProdutos = bazar.GetProdutos();
            if (!getProdutos.ProccessOk)
            {
                return View(listaProdutos);
            }
            foreach (var item in getProdutos.ListaProduto)
            {
                ProdutosModel produto = new ProdutosModel();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Preco = item.Preco;
                produto.Quantidade = item.Quantidade;
                var vendedor = bazar.GetVendedor(item.IdVendedor.ToString());
                if (!vendedor.ProccessOk)
                {
                    return View(listaProdutos);
                }
                produto.Vendedor = vendedor.Vendedor.Nome;
                listaProdutos.Add(produto);
            }
            return View(listaProdutos);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            InterfaceBazar bazar = new InterfaceBazar();
            ProdutosCreateViewModel model = new ProdutosCreateViewModel();
            var vendedores = bazar.GetVendedores();
            if (!vendedores.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in vendedores.ListaVendedor)
            {
                VendedorModel vendedor = new VendedorModel();
                vendedor.Id = item.Id;
                vendedor.Nome = item.Nome;
                model.ListaVendedores.Add(vendedor);
            }
            return View(model);
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(ProdutosCreateViewModel model)
        {
            InterfaceBazar bazar = new InterfaceBazar();
            try
            {
                Produto produto = new Produto();
                produto.Nome = model.Nome;
                produto.Preco = float.Parse(model.Preco);
                produto.Quantidade = model.Quantidade;
                produto.IdVendedor = int.Parse(model.Vendedor);
                var venda = bazar.AdicionarProduto(produto);
                if (!venda.ProccessOk)
                {
                    return View(model);
                }
                TempData["MensagemSucesso"] = "Comprador cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produtos/Edit/5
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

        // GET: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produtos/Delete/5
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
