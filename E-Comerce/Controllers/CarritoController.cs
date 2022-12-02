using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class CarritoController : Controller
    {

        E_ComerceDBDataContext carrito = new E_ComerceDBDataContext();

        // GET: Carrito
        public ActionResult Index()
        {
            //consultar detalle venta
            List<vw_Carrito> ventasV = (from v in carrito.vw_Carrito where v.ID_Venta == null && v.Usuario_Inserta == "" select v).ToList();
            decimal total = 0;
            foreach (var item in ventasV)
            {
                total += Convert.ToDecimal(item.Precio - item.descuento);
            }
            ViewBag.Total = total;

            return View(ventasV);           
        }

        [HttpPost]
        public ActionResult AgregarCarrito(int id,Productos producto)
        {
            try
            {

                var usu = "jmartinez";

                carrito.sp_RegistrarCarrito(id,producto.cantidadDisponible, usu);
                carrito.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // POST: Carrito/sumar
        [HttpPost]
        public ActionResult sumar(int Id) 
        {
            try
            {
                carrito.sp_SumarCarrito(Id);
                carrito.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Carrito/sumar
        [HttpPost]
        public ActionResult restar(int Id)
        {
            try
            {
                carrito.sp_RestarCarrito(Id);
                carrito.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Carrito/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carrito/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                carrito.SP_EliminarCarrito(Id);
                carrito.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
