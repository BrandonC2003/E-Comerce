using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class DetalleCompraController : Controller
    {
        // GET: DetalleCompra
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetalleCompra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: DetalleCompra/Create
        public ActionResult CreaDetalle()
        {
            return View();
        }

        // GET: DetalleCompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetalleCompra/Create
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

        // GET: DetalleCompra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleCompra/Edit/5
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

        // GET: DetalleCompra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleCompra/Delete/5
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
