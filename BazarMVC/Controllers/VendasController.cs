using Bazar.Class;
using Bazar.Interface;
using BazarMVC.Repositories;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.VendasViewModels;

namespace BazarMVC.Controllers
{
    [Authorize]
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
                var nomeComprador = bazar.GetComprador(item.IdComprador);
                if (!nomeComprador.ProccessOk)
                {
                    return View(listaVendas);
                }
                vendas.Comprador = nomeComprador.Comprador.Nome + " " + nomeComprador.Comprador.Sobrenome;
                listaVendas.Add(vendas);
            }
            return View(listaVendas);
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int id = 0)
        {
            if(id == 0)
            {
                TempData["MensagemErro"] = "Venda não existe!";
                return RedirectToAction("Index");
            }
            VendasDetailsViewModel model = new VendasDetailsViewModel();
            InterfaceBazar bazar = new InterfaceBazar();
            var getVendas = bazar.GetVenda(id);
            if (!getVendas.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao capturar venda!";
                return RedirectToAction("Index");
            }
            model.Id = getVendas.Venda.Id;
            model.ValorTotal = getVendas.Venda.ValorTotal;
            var comprador = bazar.GetComprador(getVendas.Venda.IdComprador);
            if (!comprador.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao capturar comprador da venda!";
                return RedirectToAction("Index");
            }
            model.NomeComprador = comprador.Comprador.Nome + " " + comprador.Comprador.Sobrenome;
            foreach (var item in getVendas.Venda.ListaProdutoVendido)
            {
                ProdutosVendidosModel produto = new ProdutosVendidosModel();
                produto.Id = item.Id;
                produto.PrecoPago = item.PrecoPago;
                var getProduto = bazar.GetProduto(item.IdProduto);
                if (!getProduto.ProccessOk)
                {
                    TempData["MensagemErro"] = "Erro ao capturar produto da venda!";
                    return RedirectToAction("Index");
                }
                produto.Produto = getProduto.Produto.Nome;
                produto.Quantidade = item.Quantidade;
                produto.Status = item.Status;
                var getVendedor = bazar.GetVendedor(getProduto.Produto.IdVendedor);
                if (!getVendedor.ProccessOk)
                {
                    TempData["MensagemErro"] = "Erro ao capturar vendedor do produto!";
                    return RedirectToAction("Index");
                }
                try
                {
                    var getUser = new AspNetUsersRepository().GetUsuario(getVendedor.Vendedor.IdUser);
                    produto.Vendedor = getUser.Nome + " " + getUser.Sobrenome;
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao capturar nome do vendedor do produto!";
                    return RedirectToAction("Index");
                }
                model.ListaProdutos.Add(produto);
            }
            return View(model);
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
                comprador.Sobrenome = item.Sobrenome;
                model.ListaCompradores.Add(comprador);
            }

            var produtos = bazar.GetProdutos();
            if (!produtos.ProccessOk)
            {
                return View(model);
            }
            foreach (var item in produtos.ListaProduto)
            {
                if(item.Quantidade < 1)
                {
                    continue;
                }
                ProdutosModel produto = new ProdutosModel();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Preco = item.Preco;
                produto.Quantidade = item.Quantidade;
                var vendedor = bazar.GetVendedor(item.IdVendedor);
                if (!produtos.ProccessOk)
                {
                    return View(model);
                }
                //produto.Vendedor = vendedor.Vendedor.Nome;
                produto.IdVendedor = vendedor.Vendedor.Id;
                model.ListaProdutos.Add(produto);
            }

            return View(model);
        }

        // POST: Vendas/Create
        [HttpPost]
        public ActionResult Create(VendasCreateViewModel model)
        {
            InterfaceBazar bazar = new InterfaceBazar();

            //Botão Cadastrar
            if (model.ProdutoSelecionado == 0)
            {
                Venda venda = new Venda();
                List<ProdutoVendido> listaProdutos = new List<ProdutoVendido>();
                if (model.Comprador == null)
                {
                    return View(model);
                }
                venda.IdComprador = int.Parse(model.Comprador);
                foreach (var item in model.ListaProdutosEscolhidos)
                {
                    ProdutoVendido produto = new ProdutoVendido();
                    produto.IdProduto = item.Id;
                    produto.PrecoPago = item.Preco;
                    produto.Quantidade = item.Quantidade;
                    listaProdutos.Add(produto);
                }

                venda.ValorTotal = model.ValorTotal;
                venda.ListaProdutoVendido = listaProdutos;
                var cadastroVenda = bazar.AdicionarVenda(venda);
                if (!cadastroVenda.ProccessOk)
                {
                    return View(model);
                }

                foreach (var item in listaProdutos)
                {
                    ProdutoVendido produtoVendido = new ProdutoVendido();
                    item.Quantidade = 1;
                    var produto = bazar.GetProduto(item.IdProduto);
                    if (!produto.ProccessOk)
                    {
                        return View(model);
                    }
                    
                    var diminuirProduto = bazar.DiminuirQuantidadeProduto(produto.Produto, item.Quantidade);
                }

                TempData["MensagemSucesso"] = "Venda cadastrada com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                int quantidadeFront = 1;
                // Adicionar da Lista de Compras
                if (model.ListaProdutos.Any(x => x.Id == model.ProdutoSelecionado))
                {
                    var produto = model.ListaProdutos.Where(a => a.Id == model.ProdutoSelecionado).ToList().First();
                    produto.Quantidade = quantidadeFront;
                    model.ListaProdutos.Remove(produto);
                    model.ListaProdutosEscolhidos.Add(produto);
                    model.ValorTotal += produto.Preco;
                }
                //Removar da Lista de Compras
                else
                {
                    var produto = model.ListaProdutosEscolhidos.Where(a => a.Id == model.ProdutoSelecionado).ToList().First();
                    produto.Quantidade = quantidadeFront;
                    model.ListaProdutos.Add(produto);
                    model.ListaProdutosEscolhidos.Remove(produto);
                    model.ValorTotal -= produto.Preco;
                }

                ModelState.Clear();

                return View(model);
            }
        }

        #region depois
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

        #endregion

        public ActionResult ListarProdutos(string id)
        {
            InterfaceBazar bazar = new InterfaceBazar();
            Venda venda = new Venda();
            venda.Id = int.Parse(id);
            var listaProdutos = bazar.GetProdutosVendidos(venda);
            if (!listaProdutos.ProccessOk)
            {
                return View();
            }
            List<ProdutosVendidosModel> model = new List<ProdutosVendidosModel>();
            foreach (var item in listaProdutos.ListaProdutoVendido)
            {
                ProdutosVendidosModel produto = new ProdutosVendidosModel();
                produto.Id = item.Id;
                produto.PrecoPago = item.PrecoPago;
                produto.Quantidade = item.Quantidade;
                var nomeProduto = bazar.GetProduto(item.IdProduto);
                if (!nomeProduto.ProccessOk)
                {
                    return View();
                }
                produto.Produto = nomeProduto.Produto.Nome;
                model.Add(produto);
            }

            return PartialView(model);
        }
    }
}
