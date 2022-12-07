using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class LugaresEntregaController : Controller
    {
        E_ComerceDBDataContext db = new E_ComerceDBDataContext();
        // GET: LugaresEntrega
        public ActionResult Index()
        {
            List<vw_LugaresEntrega> lugares = (from l in db.vw_LugaresEntrega select l).ToList(); 
            return View(lugares);
        }

        // GET: LugaresEntrega/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LugaresEntrega/Create
        public ActionResult Create()
        {
            List<Departamentos> dep = (from d in db.Departamentos select d).ToList();
            ViewBag.Departamentos = dep;
            return View();
        }

        [HttpGet]
        public JsonResult LlenarMun(int id)
        {
            List<Municipios> list = (from m in db.Municipios where m.ID_Departamento==id select m).ToList();

            return Json(list,JsonRequestBehavior.AllowGet);
        }

        // POST: LugaresEntrega/Create
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

        // GET: LugaresEntrega/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LugaresEntrega/Edit/5
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

        // GET: LugaresEntrega/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LugaresEntrega/Delete/5
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
