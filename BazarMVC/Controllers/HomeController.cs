using Bazar.Interface;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            InterfaceBazar bazar = new InterfaceBazar();
            var estatisticas = bazar.GetEstatisticas();
            if (!estatisticas.ProccessOk)
            {
                ViewBag.MensagemErro = "Erro ao Capturar Estatísticas";
                return View(model);
            }
            model.Compradores = estatisticas.Compradores;
            model.Produtos = estatisticas.Produtos;
            model.ProdutosPagos = (100 * estatisticas.ProdutosPagos) / estatisticas.ProdutosVendidos;
            model.ProdutosVendidos = estatisticas.ProdutosVendidos;
            model.Vendas = estatisticas.Vendas;
            model.Vendedores = estatisticas.Vendedores;
            return View(model);
        }
    }
}