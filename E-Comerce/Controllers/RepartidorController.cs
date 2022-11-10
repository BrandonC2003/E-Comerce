using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class RepartidorController : Controller
    {

        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();

        // GET: Repartidor
        public ActionResult Index()
        {
            List<Vw_Repartidor> listaReapartidor = (from c in E_ComerceDB.Vw_Repartidor select c).ToList();


            return View(listaReapartidor);
        }

        // GET: Repartidor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Repartidor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repartidor/Create
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

        // GET: Repartidor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Repartidor/Edit/5
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

        // GET: Repartidor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Repartidor/Delete/5
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
