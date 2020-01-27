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
using static BazarMVC.Models.UsuariosViewModels;

namespace BazarMVC.Controllers
{
    public class UsuariosController : Controller
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
        // GET: Usuarios
        public ActionResult Index()
        {
            List<AspNetUsersModel> model = new List<AspNetUsersModel>();
            model = new AspNetUsersRepository().GetUsuarios().ToList();
            return View(model);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios
        public ActionResult Create()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.TiposUsuario = new AspNetRolesRepository().GetTiposUsuario().ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, DataCadastro = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Senha);
                if (!result.Succeeded)
                {
                    return View(model);
                }
                UserManager.AddToRole(user.Id, model.Perfil);

                if (model.Perfil == "Vendedor")
                {
                    UserManager.AddToRole(user.Id, model.Perfil);

                    Vendedor vendedor = new Vendedor();
                    vendedor.IdUser = user.Id;
                    var adicionarVendedor = new InterfaceBazar().AdicionarVendedor(vendedor);
                    if (!adicionarVendedor.ProccessOk)
                    {
                        TempData["MensagemErro"] = "Erro ao Adicionar Vendedor";
                        return RedirectToAction("Index");
                    }
                }

                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["MensagemErro"] = "Erro ao Carregar Usuário";
                return RedirectToAction("Index");
            }
            UsuariosEditViewModel model = new UsuariosEditViewModel();
            var usuario = new AspNetUsersRepository().GetUsuario(id);
            try
            {
                model.Id = usuario.Id;
                model.Nome = usuario.Nome;
                model.Sobrenome = usuario.Sobrenome;
                model.Email = usuario.Email;
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao Carregar Usuário";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuariosEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                AspNetUsersModel user = new AspNetUsersModel();
                user.IdUser = model.Id;
                user.Nome = model.Nome;
                user.Sobrenome = model.Sobrenome;
                user.Email = model.Email;
                var usuario = new AspNetUsersRepository().AtualizarUsuario(user);
                if (usuario)
                {
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Erro ao Atualizar Usuário";
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = "Erro Inesperado";
                return View(model);
            }
        }
    }
}