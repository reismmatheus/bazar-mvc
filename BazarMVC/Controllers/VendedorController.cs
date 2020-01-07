using Bazar.Class;
using Bazar.Interface;
using BazarMVC.Models;
using BazarMVC.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.VendedorViewModels;

namespace BazarMVC.Controllers
{
    public class VendedorController : Controller
    {
        InterfaceBazar bazar = new InterfaceBazar();
        // GET: Vendedor
        public ActionResult Index()
        {
            List<VendedorModel> listaVendedores = new List<VendedorModel>();
            var getVendedores = bazar.GetVendedores();
            if (!getVendedores.ProccessOk)
            {
                return View(listaVendedores);
            }
            foreach (var item in getVendedores.ListaVendedor)
            {
                VendedorModel vendedor = new VendedorModel();
                vendedor.Id = item.Id;
                vendedor.Nome = item.Nome;
                listaVendedores.Add(vendedor);
            }
            return View(listaVendedores);
        }

        // GET: Vendedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vendedor/Create
        public ActionResult Create()
        {
            return View(new VendedorCreateViewModel());
        }

        // POST: Vendedor/Create
        [HttpPost]
        public ActionResult Create(VendedorCreateViewModel model)
        {
            try
            {
                Vendedor vendedor = new Vendedor();
                vendedor.Nome = model.Nome;
                var adicionarVendedor = bazar.AdicionarVendedor(vendedor);
                if (!adicionarVendedor.ProccessOk)
                {
                    return View(model);
                }

                TempData["MensagemSucesso"] = "Vendedor cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Vendedor/Edit/5
        public ActionResult Edit(int id)
        {
            VendedorEditViewModel model = new VendedorEditViewModel();
            var getVendedor = bazar.GetVendedor(id.ToString());
            if (!getVendedor.ProccessOk)
            {
                return View(model);
            }
            model.Id = getVendedor.Vendedor.Id;
            model.Nome = getVendedor.Vendedor.Nome;
            return View(model);
        }

        // POST: Vendedor/Edit/5
        [HttpPost]
        public ActionResult Edit(VendedorEditViewModel model)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vendedor/Delete/5
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
