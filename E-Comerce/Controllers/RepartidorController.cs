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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<Vw_Repartidor> listaReapartidor = (from r in E_ComerceDB.Vw_Repartidor select r).ToList();


                    return View(listaReapartidor);
                }
                else if (Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else
                {
                    return RedirectToAction("Index", "Tienda");
                }
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }
        }

        // GET: Repartidor/Details/5
        public ActionResult Details(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

                    return View(repar);
                }
                else if (Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else
                {
                    return RedirectToAction("Index", "Tienda");
                }
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }
        }

        // GET: Repartidor/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<Repartidores> repartidor = (from r in E_ComerceDB.Repartidores select r).ToList();

                    ViewBag.Lista = repartidor;

                    return View();
                }
                else if (Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else
                {
                    return RedirectToAction("Index", "Tienda");
                }
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }
        }

        // POST: Repartidor/Create
        [HttpPost]
        public ActionResult Create(Repartidores repartidor)
        {
            try
            {
                repartidor.Usuario_Inserta = Session["Usuario"].ToString();

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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

                    return View(repar);
                }
                else if (Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else
                {
                    return RedirectToAction("Index", "Tienda");
                }
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }
        }

        // POST: Repartidor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Repartidores rpar)
        {
            try
            {
                rpar.Usuario_Actualiza= Session["Usuario"].ToString();
                E_ComerceDB.SP_EditarRepartidor(id, rpar.Nombre, rpar.Apellido, rpar.CorreoElectronico, rpar.Telefono, rpar.Usuario_Actualiza);
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Repartidores repar = (from r in E_ComerceDB.Repartidores where r.ID_Repartidor == id select r).Single();

                    return View(repar);
                }
                else if (Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else
                {
                    return RedirectToAction("Index", "Tienda");
                }
            }
            else
            {
                return RedirectToAction("Index", "Tienda");
            }

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
