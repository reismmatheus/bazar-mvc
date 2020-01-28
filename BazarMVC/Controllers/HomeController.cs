using Bazar.Interface;
using Bazar.Result;
using BazarMVC.Repositories;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            InterfaceBazar bazar = new InterfaceBazar();
            EstatisticaResult estatisticas = new EstatisticaResult();
            if (User.IsInRole("Admin"))
            {
                estatisticas = bazar.GetEstatisticas();
                if (!estatisticas.ProccessOk)
                {
                    ViewBag.MensagemErro = "Erro ao Capturar Estatísticas";
                    return View(model);
                }
            }
            else
            {
                var user = new AspNetUsersRepository().GetUsuarioByUsername(User.Identity.Name);
                var getVendedor = bazar.GetVendedorByIdUser(user.Id);
                estatisticas = bazar.GetEstatisticas(getVendedor.Vendedor.Id);
                if (!estatisticas.ProccessOk)
                {
                    ViewBag.MensagemErro = "Erro ao Capturar Estatísticas";
                    return View(model);
                }
            }
            model.Compradores = estatisticas.Compradores;
            model.Produtos = estatisticas.Produtos;
            if(estatisticas.ProdutosPagos != 0 && estatisticas.ProdutosVendidos != 0)
                model.ProdutosPagos = (100 * estatisticas.ProdutosPagos) / estatisticas.ProdutosVendidos;
            model.ProdutosVendidos = estatisticas.ProdutosVendidos;
            model.Vendas = estatisticas.Vendas;
            model.Vendedores = estatisticas.Vendedores;
            return View(model);
        }
    }
}