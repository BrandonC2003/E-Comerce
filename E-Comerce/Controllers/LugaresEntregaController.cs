using E_Comerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace E_Comerce.Controllers
{
    public class LugaresEntregaController : Controller
    {
        E_ComerceDBDataContext db = new E_ComerceDBDataContext();
        // GET: LugaresEntrega
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<vw_LugaresEntrega> lugares = (from l in db.vw_LugaresEntrega select l).ToList();
                    return View(lugares);
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

        // GET: LugaresEntrega/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<Departamentos> dep = (from d in db.Departamentos select d).ToList();
                    ViewBag.Departamentos = dep;

                    List<SelectRepartidor> rep = (from r in db.Repartidores
                                                  select new SelectRepartidor
                                                  {
                                                      Id = r.ID_Repartidor,
                                                      Name = r.Nombre + " " + r.Apellido
                                                  }).ToList();

                    ViewBag.Repartidores = rep;

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

        [HttpGet]
        public JsonResult LlenarMun(int id)
        {
            List<ElementJson> list = (from m in db.Municipios 
                                      where m.ID_Departamento==id
                                      select new ElementJson
                                      {
                                          Id=m.ID_Municipio,
                                          Name=m.Nombre
                                      }).ToList();

            return Json(list,JsonRequestBehavior.AllowGet);
        }

        // POST: LugaresEntrega/Create
        [HttpPost]
        public ActionResult Create(Lugares_Entrega datos)
        {
            try
            {
                db.sp_InsertLugaresEntrega(datos.ID_Repartidor, datos.ID_Municipio, datos.MontoEntrega, Session["Usuario"].ToString());
                db.SubmitChanges();
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Lugares_Entrega le = (from l in db.Lugares_Entrega where l.ID_Entrega == id select l).Single();

                    List<Departamentos> dep = (from d in db.Departamentos select d).ToList();
                    ViewBag.Departamentos = dep;

                    List<SelectRepartidor> rep = (from r in db.Repartidores
                                                  select new SelectRepartidor
                                                  {
                                                      Id = r.ID_Repartidor,
                                                      Name = r.Nombre + " " + r.Apellido
                                                  }).ToList();

                    ViewBag.Repartidores = rep;

                    return View(le);
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

        // POST: LugaresEntrega/Edit/5
        [HttpPost]
        public ActionResult Edit(Lugares_Entrega datos)
        {
            try
            {
                db.sp_ModificarLugaresEntrega(datos.ID_Entrega, datos.ID_Repartidor, datos.ID_Municipio, datos.MontoEntrega, Session["Usuario"].ToString());
                db.SubmitChanges();

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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Lugares_Entrega le = (from l in db.Lugares_Entrega where l.ID_Entrega == id select l).Single();

                    return View(le);
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

        // POST: LugaresEntrega/Delete/5
        [HttpPost]
        public ActionResult Delete(Lugares_Entrega datos)
        {
            try
            {
                db.sp_EliminarLugaresEntrega(datos.ID_Entrega);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
