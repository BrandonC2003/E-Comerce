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
            List<vw_DetalleVenta> lista = (from dv in ventas.vw_DetalleVenta orderby dv.ID_Venta descending select dv ).ToList();

            return View(lista);
        }

        // GET: ModificarVentas/Details/5
        public ActionResult Details(int id)
        {
            vw_DetalleVenta obj = (from dv in ventas.vw_DetalleVenta where dv.ID_DetalleVenta==id select dv).First();
            return View(obj);
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
            DetalleVenta obj = (from dv in ventas.DetalleVenta where dv.ID_DetalleVenta == id select dv).First();

            List<Productos> lista = (from p in ventas.Productos select p).ToList();

            ViewBag.lista = lista;
            return View(obj);
        }

        // POST: ModificarVentas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DetalleVenta dtVenta)
        {
            try
            {
                string usuario = "BrandonCC";
                ventas.sp_EditarDetalleVenta(id, dtVenta.ID_Venta, dtVenta.ID_Producto, dtVenta.Cantidad, usuario);
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
            DetalleVenta obj = (from dv in ventas.DetalleVenta where dv.ID_DetalleVenta == id select dv).First();
            return View(obj);
        }

        // POST: ModificarVentas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
