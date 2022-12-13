using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<vw_Venta> lista = (from dv in ventas.vw_Venta select dv).ToList();

                    return View(lista);
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

        public ActionResult DetaislVenta()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
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
        // GET: ModificarVentas/Details/5
        public ActionResult Details(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<vw_DetalleVenta> obj = (from dv in ventas.vw_DetalleVenta where dv.ID_Venta == id select dv).ToList();
                    Ventas objVenta = (from v in ventas.Ventas where v.ID_Venta == id select v).Single();
                    ViewBag.ListV = objVenta;

                    var montoEnvio = (from e in ventas.Lugares_Entrega 
                                      where e.ID_Entrega==objVenta.ID_Entrega 
                                      select e).Single();

                    ViewBag.MontoEntrega=montoEnvio.MontoEntrega;

                    ViewBag.PrecioCompra = objVenta.PrecioTotal - montoEnvio.MontoEntrega;

                    return View(obj);
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

        public ActionResult DetailsVenta(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    vw_DetalleVenta obj = (from i in ventas.vw_DetalleVenta where i.ID_DetalleVenta == id select i).Single();
                    return View(obj);
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<vw_DetalleVenta> obj = (from dv in ventas.vw_DetalleVenta where dv.ID_Venta == id select dv).ToList();
                    return View(obj);
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

        public ActionResult EditarDetalle(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    DetalleVenta obj = (from dt in ventas.DetalleVenta where dt.ID_DetalleVenta == id select dt).Single();
                    List<Productos> lista = (from p in ventas.Productos select p).ToList();

                    ViewBag.lista = lista;
                    return View(obj);
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
        // POST: ModificarVentas/Edit/5
        [HttpPost]
        public ActionResult EditarDetalle(int id, DetalleVenta dtVenta)
        {
            try
            {
                string usuario = Session["Usuario"].ToString(); ;
                ventas.sp_EditarDetalleVenta(id, dtVenta.ID_Venta, dtVenta.ID_Producto, dtVenta.Cantidad, usuario);
                ventas.SubmitChanges();

                return RedirectToAction("Edit", new {id = dtVenta.ID_Venta});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EliminarVenta(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Ventas obj = (from v in ventas.Ventas where v.ID_Venta == id select v).First();

                    return View(obj);
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

        [HttpPost]
        public ActionResult EliminarVenta(int id, Ventas obj)
        {
            try
            {
                ventas.sp_EliminarVenta(id);
                ventas.SubmitChanges();
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    DetalleVenta obj = (from dv in ventas.DetalleVenta where dv.ID_DetalleVenta == id select dv).First();
                    return View(obj);
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

        // POST: ModificarVentas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DetalleVenta dtV)
        {
            try
            {
                ventas.SP_EliminarDetalleVenta(id);
                ventas.SubmitChanges();

                return RedirectToAction("Edit", new {id=dtV.ID_Venta});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GenerarVista(int id)
        {
            List<vw_Facturacion> obj = (from dv in ventas.vw_Facturacion where dv.ID_Venta == id select dv).ToList();
            Ventas objVenta = (from v in ventas.Ventas where v.ID_Venta == id select v).Single();
            ViewBag.ListV = objVenta;

            var montoEnvio = (from e in ventas.Lugares_Entrega
                              where e.ID_Entrega == objVenta.ID_Entrega
                              select e).Single();

            Usuarios usuarios = (from u in ventas.Usuarios where u.ID_Usuario==objVenta.ID_Usuario select u).Single();

            ViewBag.MontoEntrega = montoEnvio.MontoEntrega;

            ViewBag.PrecioCompra = objVenta.PrecioTotal - montoEnvio.MontoEntrega;

            ViewBag.NombreUsuario = usuarios.Nombre + " " + usuarios.Apellido;

            return View(obj);
        }

        public ActionResult ImprimirFactura(int ID)
        {
            return new ActionAsPdf("GenerarVista", new { id = ID }) { FileName="Factura.pdf"};
        }
    }
}
