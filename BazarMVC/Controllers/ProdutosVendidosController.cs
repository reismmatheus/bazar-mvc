using Bazar.Interface;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarMVC.Controllers
{
    [Authorize]
    public class ProdutosVendidosController : Controller
    {
        // GET: ProdutosVendidos
        public ActionResult Index()
        {
            List<ProdutosVendidosModel> listaProdutosVendidos = new List<ProdutosVendidosModel>();
            InterfaceBazar bazar = new InterfaceBazar();
            var getProdutosVendidos = bazar.GetProdutosVendidos();
            if (!getProdutosVendidos.ProccessOk)
            {
                return View(listaProdutosVendidos);
            }
            foreach (var item in getProdutosVendidos.ListaProdutoVendido)
            {
                ProdutosVendidosModel produto = new ProdutosVendidosModel();
                produto.Id = item.Id;
                produto.PrecoPago = item.PrecoPago;
                produto.Quantidade = item.Quantidade;
                produto.Status = item.Status;
                var nomeProduto = bazar.GetProduto(item.IdProduto);
                if (!nomeProduto.ProccessOk)
                {
                    return View(listaProdutosVendidos);
                }
                produto.Produto = nomeProduto.Produto.Nome;
                var venda = bazar.GetVenda(item.IdVenda.ToString());
                if (!venda.ProccessOk)
                {
                    return View(listaProdutosVendidos);
                }
                var comprador = bazar.GetComprador(venda.Venda.IdComprador.ToString());
                if (!comprador.ProccessOk)
                {
                    return View(listaProdutosVendidos);
                }
                produto.Comprador = comprador.Comprador.Nome;
                listaProdutosVendidos.Add(produto);
            }
            return View(listaProdutosVendidos);
        }

        // GET: ProdutosVendidos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProdutosVendidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdutosVendidos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutosVendidos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProdutosVendidos/Edit/5
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

        // GET: ProdutosVendidos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProdutosVendidos/Delete/5
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
