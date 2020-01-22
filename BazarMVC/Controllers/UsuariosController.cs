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
            try
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Nome = model.Nome, Sobrenome = model.Sobrenome, DataCadastro = DateTime.Now };
                var result = await UserManager.CreateAsync(user, model.Senha);
                if (!result.Succeeded)
                {
                    return View(model);
                }
                UserManager.AddToRole(user.Id, model.Perfil);

                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}