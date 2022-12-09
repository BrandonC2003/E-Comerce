using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Security.Cryptography;

namespace E_Comerce.Controllers
{
    public class VentasController : Controller
    {
        E_ComerceDBDataContext ventas = new E_ComerceDBDataContext();
        // GET: Ventas
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1 || Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    List<vw_DetalleVenta> ventasV = (from v in ventas.vw_DetalleVenta where v.ID_Venta == null && v.Usuario_Inserta == Session["Usuario"].ToString() select v).ToList();
                    decimal total = 0;
                    foreach (var item in ventasV)
                    {
                        total += Convert.ToDecimal(item.Precio - item.descuento);
                    }
                    ViewBag.Total = total;

                    return View(ventasV);
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
        // GET: Ventas/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1 || Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    List<Productos> products = (from p in ventas.Productos select p).ToList();

                    ViewBag.Lista = products;

                    return View();
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

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(DetalleVenta detalleV)
        {
            try
            {
                ventas.sp_RegistrarDetalleVenta(detalleV.ID_Producto,detalleV.Cantidad, Session["Usuario"].ToString());
                ventas.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1 || Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    DetalleVenta dtV = (from dv in ventas.DetalleVenta where dv.ID_DetalleVenta == id select dv).Single();
                    List<Productos> products = (from p in ventas.Productos select p).ToList();

                    ViewBag.Lista = products;
                    return View(dtV);
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

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DetalleVenta dtV)
        {
            try
            {
                ventas.sp_EditarDetalleVenta(id,null,dtV.ID_Producto,dtV.Cantidad, null);
                ventas.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1 || Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return View();
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

        // POST: Ventas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DetalleVenta dt)
        {
            try
            {
                ventas.SP_EliminarDetalleVenta(id);
                ventas.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Finalizar()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1 || Convert.ToInt32(Session["Rol_Usuario"]) == 2)
                {
                    return View();
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

        [HttpPost]
        public ActionResult Finalizar(Ventas vent)
        {
            try
            {
                vent.ID_Usuario = 1;
                vent.Usuario_Inserta = "BC";

                ventas.sp_RegistrarVenta(vent.ID_Usuario, vent.Usuario_Inserta);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
