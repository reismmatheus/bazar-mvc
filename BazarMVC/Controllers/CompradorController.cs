using Bazar.Class;
using Bazar.Interface;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.CompradorViewModels;

namespace BazarMVC.Controllers
{
    [Authorize]
    public class CompradorController : Controller
    {
        // GET: Comprador
        public ActionResult Index()
        {
            List<CompradorModel> listaCompradores = new List<CompradorModel>();
            InterfaceBazar bazar = new InterfaceBazar();
            var getCompradores = bazar.GetCompradores();
            if (!getCompradores.ProccessOk)
            {
                return View(listaCompradores);
            }
            foreach (var item in getCompradores.ListaComprador)
            {
                CompradorModel comprador = new CompradorModel();
                comprador.Id = item.Id;
                comprador.Nome = item.Nome;
                comprador.Sobrenome = item.Sobrenome;
                listaCompradores.Add(comprador);
            }
            return View(listaCompradores);
        }

        // GET: Comprador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comprador/Create
        public ActionResult Create()
        {
            return View(new CompradorCreateViewModel());
        }

        // POST: Comprador/Create
        [HttpPost]
        public ActionResult Create(CompradorCreateViewModel model)
        {
            InterfaceBazar bazar = new InterfaceBazar();
            try
            {
                Comprador comprador = new Comprador();
                comprador.Nome = model.Nome;
                comprador.Sobrenome = model.Sobrenome;
                var adicionarComprador = bazar.AdicionarComprador(comprador);
                if (!adicionarComprador.ProccessOk)
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

        // GET: Comprador/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if(id == 0)
            {
                TempData["MensagemErro"] = "Erro ao Carregar Comprador";
                return RedirectToAction("Index");
            }
            InterfaceBazar bazar = new InterfaceBazar();
            CompradorEditViewModel model = new CompradorEditViewModel();
            var comprador = bazar.GetComprador(id);
            if (!comprador.ProccessOk)
            {
                TempData["MensagemErro"] = "Erro ao Carregar Comprador";
                return RedirectToAction("Index");
            }
            model.Id = comprador.Comprador.Id;
            model.Nome = comprador.Comprador.Nome;
            model.Sobrenome = comprador.Comprador.Sobrenome;
            return View(model);
        }

        // POST: Comprador/Edit/5
        [HttpPost]
        public ActionResult Edit(CompradorEditViewModel model)
        {
            try
            {
                InterfaceBazar bazar = new InterfaceBazar();
                Comprador comprador = new Comprador();
                comprador.Id = model.Id;
                comprador.Nome = model.Nome;
                comprador.Sobrenome = model.Sobrenome;
                var salvarComprador = bazar.EditarComprador(comprador);
                if (!salvarComprador.ProccessOk)
                {
                    TempData["MensagemErro"] = "Erro ao salvar o Comprador";
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

        // GET: Comprador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comprador/Delete/5
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
