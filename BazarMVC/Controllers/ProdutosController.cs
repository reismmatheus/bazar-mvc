using Bazar.Class;
using Bazar.Interface;
using Bazar.Result;
using BazarMVC.Repositories;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.ProdutosViewModels;

namespace BazarMVC.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            List<ProdutosModel> listaProdutos = new List<ProdutosModel>();
            InterfaceBazar bazar = new InterfaceBazar();
            ListaProdutoResult getProdutos = new ListaProdutoResult();
            if (User.IsInRole("Admin"))
            {
                getProdutos = bazar.GetProdutos();
                if (!getProdutos.ProccessOk)
                {
                    return View(listaProdutos);
                }
            }
            else
            {
                var user = new AspNetUsersRepository().GetUsuarioByUsername(User.Identity.Name);
                var getVendedor = bazar.GetVendedorByIdUser(user.Id);
                getProdutos = bazar.GetProdutos(getVendedor.Vendedor.Id);
                if (!getProdutos.ProccessOk)
                {
                    return View(listaProdutos);
                }
            }
            foreach (var item in getProdutos.ListaProduto)
            {
                ProdutosModel produto = new ProdutosModel();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Preco = item.Preco;
                produto.Quantidade = item.Quantidade;
                var vendedor = bazar.GetVendedor(item.IdVendedor);
                if (!vendedor.ProccessOk)
                {
                    return View(listaProdutos);
                }
                var dadosVendedor = new AspNetUsersRepository().GetUsuario(vendedor.Vendedor.IdUser);
                produto.Vendedor = dadosVendedor.Nome + " " + dadosVendedor.Sobrenome;
                produto.IdVendedor = vendedor.Vendedor.Id;
                listaProdutos.Add(produto);
            }
            return View(listaProdutos);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int id = 0)
        {
            ProdutosDetailsViewModel model = new ProdutosDetailsViewModel();
            var getProduto = new InterfaceBazar().GetProduto(id);
            if (!getProduto.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao capturar produto";
                return RedirectToAction("Index");
            }
            model.Id = getProduto.Produto.Id;
            model.Nome = getProduto.Produto.Nome;
            model.Preco = getProduto.Produto.Preco;
            model.Quantidade = getProduto.Produto.Quantidade;
            model.Descricao = getProduto.Produto.Descricao;
            var vendedor = new InterfaceBazar().GetVendedor(getProduto.Produto.IdVendedor);
            if (!vendedor.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao capturar vendedor";
                return RedirectToAction("Index");
            }
            var dadosVendedor = new AspNetUsersRepository().GetUsuario(vendedor.Vendedor.IdUser);
            model.NomeVendedor = dadosVendedor.Nome + " " + dadosVendedor.Sobrenome;
            model.IdVendedor = vendedor.Vendedor.Id.ToString();
            return View(model);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            InterfaceBazar bazar = new InterfaceBazar();
            ProdutosCreateViewModel model = new ProdutosCreateViewModel();

            if (User.IsInRole("Admin"))
            {
                var vendedores = bazar.GetVendedores();
                if (!vendedores.ProccessOk)
                {
                    return View(model);
                }
                foreach (var item in vendedores.ListaVendedor)
                {
                    VendedorModel vendedor = new VendedorModel();
                    vendedor.Id = item.Id;
                    var dadosVendedor = new AspNetUsersRepository().GetUsuario(item.IdUser);
                    vendedor.Nome = dadosVendedor.Nome + " " + dadosVendedor.Sobrenome;
                    model.ListaVendedores.Add(vendedor);
                }
            }
            else
            {
                var user = new AspNetUsersRepository().GetUsuarioByUsername(User.Identity.Name);
                var getVendedor = bazar.GetVendedorByIdUser(user.Id);
                model.NomeVendedor = user.Nome + ' ' + user.Sobrenome;
                model.IdVendedor = getVendedor.Vendedor.Id.ToString();
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
                produto.Preco = float.Parse(model.Preco, CultureInfo.InvariantCulture.NumberFormat);
                produto.Quantidade = model.Quantidade;
                produto.IdVendedor = int.Parse(model.IdVendedor);
                produto.Descricao = string.IsNullOrEmpty(model.Descricao) ? "Sem descrição" : model.Descricao;
                var addProduto = bazar.AdicionarProduto(produto);
                if (!addProduto.ProccessOk)
                {
                    return View(model);
                }
                TempData["MensagemSucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                TempData["MensagemErro"] = "Erro ao Carregar Produto";
                return RedirectToAction("Index");
            }
            InterfaceBazar bazar = new InterfaceBazar();
            ProdutosEditViewModel model = new ProdutosEditViewModel();
            var produto = bazar.GetProduto(id);
            if (!produto.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao Carregar Produto";
                return RedirectToAction("Index");
            }
            model.Id = produto.Produto.Id;
            model.Nome = produto.Produto.Nome;
            model.Descricao = produto.Produto.Descricao;
            model.Preco = produto.Produto.Preco;
            model.Quantidade = produto.Produto.Quantidade;
            model.IdVendedor = produto.Produto.IdVendedor.ToString();
            var vendedores = bazar.GetVendedores();
            if (!vendedores.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in vendedores.ListaVendedor)
            {
                VendedorModel vendedor = new VendedorModel();
                vendedor.Id = item.Id;
                var dadosVendedor = new AspNetUsersRepository().GetUsuario(item.IdUser);
                if(vendedor.Id.ToString() == model.IdVendedor)
                {
                    model.NomeVendedor = dadosVendedor.Nome + " " + dadosVendedor.Sobrenome;
                }
                vendedor.Nome = dadosVendedor.Nome + " " + dadosVendedor.Sobrenome;
                model.ListaVendedores.Add(vendedor);
            }
            return View(model);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(ProdutosEditViewModel model)
        {
            try
            {
                InterfaceBazar bazar = new InterfaceBazar();
                Produto produto = new Produto();
                produto.Id = model.Id;
                produto.Nome = model.Nome;
                produto.IdVendedor = int.Parse(model.IdVendedor);
                produto.Preco = model.Preco;
                produto.Descricao = string.IsNullOrEmpty(model.Descricao) ? "Sem descrição" : model.Descricao ;
                produto.Quantidade = model.Quantidade;
                var salvarProduto = bazar.AtualizarProduto(produto);
                if (!salvarProduto.ProccessOk)
                {
                    TempData["MensagemErro"] = "Erro ao salvar o Produto";
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MensagemErro"] = "Erro Inesperado";
                return View(model);
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
