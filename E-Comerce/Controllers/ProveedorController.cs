using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{

    public class ProveedorController : Controller
    {
        E_ComerceDBDataContext dbproveedor = new E_ComerceDBDataContext();

        // GET: Proveedor
        public ActionResult Index()
        {
            List<Proveedores> Listaproveedor = (from p in dbproveedor.Proveedores select p).ToList();
            return View(Listaproveedor);
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {

            Vw_Proveedor dtll = (from dp in dbproveedor.Vw_Proveedor
                                 where dp.ID_Proveedor == id
                                 select dp).First();
            return View(dtll);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
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

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proveedor/Edit/5
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

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedor/Delete/5
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
