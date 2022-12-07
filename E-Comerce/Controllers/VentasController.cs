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
            int rol = Convert.ToInt32(Session["Usuario"]);
            if (rol==1 || rol==2)
            {
                List<vw_DetalleVenta> ventasV = (from v in ventas.vw_DetalleVenta where v.ID_Venta == null select v).ToList();
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
                return RedirectToAction("Index","Login");
            }
        }
        // GET: Ventas/Create
        public ActionResult Create()
        {
            List<Productos> products = (from p in ventas.Productos select p).ToList();

            ViewBag.Lista = products;

            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(DetalleVenta detalleV)
        {
            try
            {
                ventas.sp_RegistrarDetalleVenta(detalleV.ID_Producto,detalleV.Cantidad);
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
            DetalleVenta dtV = (from dv in ventas.DetalleVenta where dv.ID_DetalleVenta==id select dv).Single();
            List<Productos> products = (from p in ventas.Productos select p).ToList();

            ViewBag.Lista = products;
            return View(dtV);
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

            return View();
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
            return View();
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
