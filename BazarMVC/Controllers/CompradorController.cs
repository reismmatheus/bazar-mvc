using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarMVC.Controllers
{
    public class CompradorController : Controller
    {
        // GET: Comprador
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comprador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comprador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comprador/Create
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

        // GET: Comprador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comprador/Edit/5
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
