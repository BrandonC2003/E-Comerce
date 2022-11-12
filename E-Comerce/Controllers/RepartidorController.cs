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
            List<Vw_Repartidor> listaReapartidor = (from r in E_ComerceDB.Vw_Repartidor select r).ToList();


            return View(listaReapartidor);
        }

        // GET: Repartidor/Details/5
        public ActionResult Details(int id)
        {

            Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

            return View(repar);
        }

        // GET: Repartidor/Create
        public ActionResult Create()
        {
            List<Repartidores> repartidor = (from r in E_ComerceDB.Repartidores select r).ToList();

            ViewBag.Lista = repartidor;

            return View();
        }

        // POST: Repartidor/Create
        [HttpPost]
        public ActionResult Create(Repartidores repartidor)
        {
            try
            {
                // TODO: Add insert logic here

                E_ComerceDB.SP_InsertarRepartido(repartidor.Nombre, repartidor.Apellido, repartidor.CorreoElectronico, repartidor.Telefono, repartidor.Usuario_Inserta);
                E_ComerceDB.SubmitChanges();
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
            Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

            return View(repar);
        }

        // POST: Repartidor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Repartidores rpar)
        {
            try
            {
                E_ComerceDB.SP_EditarRepartidor(id, rpar.Nombre, rpar.Apellido, rpar.CorreoElectronico, rpar.Telefono, rpar.Usuario_Inserta);
                E_ComerceDB.SubmitChanges();

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
            Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

            return View(repar);

        }

        // POST: Repartidor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                E_ComerceDB.SP_EliminarRepartidor(id);
                E_ComerceDB.SubmitChanges();


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
