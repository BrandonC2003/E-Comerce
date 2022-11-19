using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class DetalleComprasController : Controller
    {
        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();
        // GET: DetalleCompras
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetalleCompras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetalleCompras/Create
        public ActionResult Create()
        {
            List<Productos> products = (from p in E_ComerceDB.Productos select p).ToList();

            ViewBag.Lista = products;
            return View();
        }

        // POST: DetalleCompras/Create
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

        // GET: DetalleCompras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleCompras/Edit/5
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

        // GET: DetalleCompras/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleCompras/Delete/5
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
