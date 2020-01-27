using Bazar.Class;
using Bazar.Interface;
using BazarMVC.Models;
using BazarMVC.Repositories;
using BazarMVC.Repositories.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BazarMVC.Models.VendedorViewModels;

namespace BazarMVC.Controllers
{
    [Authorize]
    public class VendedorController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        InterfaceBazar bazar = new InterfaceBazar();
        // GET: Vendedor
        public ActionResult Index()
        {
            List<AspNetUsersModel> model = new List<AspNetUsersModel>();
            model = new AspNetUsersRepository().GetUsuarios().Where(x => x.Tipo.ToLower() == "vendedor").ToList();
            return View(model);
        }

        // GET: Vendedor/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: Vendedor/Create
        public ActionResult Create()
        {
            return View(new RegisterViewModel());
        }

        // POST: Vendedor/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.Perfil = "Vendedor";

                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, DataCadastro = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Senha);
                if (!result.Succeeded)
                {
                    return View(model);
                }
                UserManager.AddToRole(user.Id, model.Perfil);

                Vendedor vendedor = new Vendedor();
                vendedor.IdUser = user.Id;
                var adicionarVendedor = bazar.AdicionarVendedor(vendedor);
                if (!adicionarVendedor.ProccessOk)
                {
                    return View(model);
                }

                TempData["MensagemSucesso"] = "Vendedor cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        // GET: Vendedor/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["MensagemErro"] = "Erro ao Capturar vendedor";
                return RedirectToAction("Index");
            }
            VendedorEditViewModel model = new VendedorEditViewModel();
            try
            {
                var getVendedor = new AspNetUsersRepository().GetUsuario(id);
                model.Id = getVendedor.IdUser;
                model.Nome = getVendedor.Nome;
                model.Sobrenome = getVendedor.Sobrenome;
                model.Email = getVendedor.Email;
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao Capturar vendedor";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Vendedor/Edit/5
        [HttpPost]
        public ActionResult Edit(VendedorEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AspNetUsersModel vendedor = new AspNetUsersModel();
            try
            {
                vendedor.IdUser = model.Id;
                vendedor.Nome = model.Nome;
                vendedor.Sobrenome = model.Sobrenome;
                vendedor.Email = model.Email;
                var getVendedor = new AspNetUsersRepository().AtualizarUsuario(vendedor);
                if (!getVendedor)
                {
                    TempData["MensagemErro"] = "Erro ao atualizar vendedor";
                    return View(model);
                }
                TempData["MensagemSucesso"] = "Vendedor atualizado com sucesso";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["MensagemErro"] = "Erro ao capturar vendedor";
                return View(model);
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
