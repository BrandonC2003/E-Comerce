using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class ModificarVentasController : Controller
    {
        E_ComerceDBDataContext ventas = new E_ComerceDBDataContext();
        // GET: ModificarVentas
        public ActionResult Index()
        {
            List<DetalleVenta> lista = (from dv in ventas.DetalleVenta select dv).ToList();

            return View(lista);
        }

        // GET: ModificarVentas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModificarVentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModificarVentas/Create
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

        // GET: ModificarVentas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModificarVentas/Edit/5
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

        // GET: ModificarVentas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModificarVentas/Delete/5
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
