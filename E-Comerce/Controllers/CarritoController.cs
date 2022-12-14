using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Rotativa;

namespace E_Comerce.Controllers
{
    public class CarritoController : Controller
    {

        E_ComerceDBDataContext carrito = new E_ComerceDBDataContext();
        // GET: Carrito
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                //consultar detalle venta
                List<vw_Carrito> ventasV = (from v in carrito.vw_Carrito where v.ID_Venta == null && v.Usuario_Inserta == Session["Usuario"].ToString() select v).ToList();
                decimal total = 0, entrega=0;
                foreach (var item in ventasV)
                {
                    total += Convert.ToDecimal(item.Precio);
                }

                if (Session["Monto"] != null)
                    entrega = Convert.ToDecimal(Session["Monto"]);

                ViewBag.Total = (total+entrega).ToString("0.00");

                int cantidad = 0;
                foreach (var item in ventasV)
                {
                    cantidad += Convert.ToInt32(item.cantidad);
                }
                
                Session["Carrito"] = cantidad;
                return View(ventasV);
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }

        [HttpGet]
        public ActionResult AgregarCarrito(int id,Productos producto)
        {
            try
            {
                if (Session["Usuario"]!= null)
                {
                    carrito.sp_RegistrarCarrito(id, 1, Session["Usuario"].ToString());
                    carrito.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index","Login");
                }
            }
            catch
            {
                return View();
            }
        }

        // POST: Carrito/sumar
        [HttpGet]
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
        [HttpGet]
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

        // POST: Carrito/Delete/5
        [HttpGet]
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

        [HttpGet]
        public ActionResult SeleccionarEntrega()
        {
            List<Departamentos> dep = (from d in carrito.Departamentos select d).ToList();
            ViewBag.Departamentos = dep;

            return View();
        }

        [HttpPost]
        public ActionResult SeleccionarEntrega(int ID_Municipio)
        {
            try
            {
                Lugares_Entrega entrega = (from e in carrito.Lugares_Entrega where e.ID_Municipio==ID_Municipio select e).First();
                if (entrega != null)
                {
                    Session["ID_Entrega"] = entrega.ID_Entrega;
                    Session["Monto"] = entrega.MontoEntrega;
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult FinalizarCompra()
        {
            int? idVenta = 0;
            int idUsuario = Convert.ToInt32(Session["ID_Usuario"]);
            int idEntrega = Convert.ToInt32(Session["ID_Entrega"]);
            string usuario = Session["Usuario"].ToString();
            Session["Monto"] = null;
            Session["ID_Entrega"] = null;
            carrito.sp_RegistrarVentaCliente(idUsuario,idEntrega,usuario,ref idVenta);
            Session["ID_Venta"] = idVenta;
            carrito.SubmitChanges();
            return RedirectToAction("ConfirmacionCompra");
        }

        public ActionResult ImprimirFactura(int ID)
        {
            return new ActionAsPdf("GenerarVista", new { id = ID }) { FileName = "Factura.pdf" };
        }
        public ActionResult GenerarVista(int id)
        {
            List<vw_Facturacion> obj = (from dv in carrito.vw_Facturacion where dv.ID_Venta == id select dv).ToList();
            Ventas objVenta = (from v in carrito.Ventas where v.ID_Venta == id select v).Single();
            ViewBag.ListV = objVenta;

            var montoEnvio = (from e in carrito.Lugares_Entrega
                              where e.ID_Entrega == objVenta.ID_Entrega
                              select e).Single();

            Usuarios usuarios = (from u in carrito.Usuarios where u.ID_Usuario == objVenta.ID_Usuario select u).Single();

            ViewBag.MontoEntrega = montoEnvio.MontoEntrega;

            ViewBag.PrecioCompra = objVenta.PrecioTotal - montoEnvio.MontoEntrega;

            ViewBag.NombreUsuario = usuarios.Nombre + " " + usuarios.Apellido;
            ViewBag.Total = objVenta.PrecioTotal;

            return View(obj);
        }

        public ActionResult ConfirmacionCompra()
        {
            if (Session["ID_Venta"]!=null)
            {
                int id = Convert.ToInt32(Session["ID_Venta"]);
                List<vw_DetalleVenta> obj = (from dv in carrito.vw_DetalleVenta where dv.ID_Venta == id select dv).ToList();
                Ventas objVenta = (from v in carrito.Ventas where v.ID_Venta == id select v).Single();
                ViewBag.ListV = objVenta;

                var montoEnvio = (from e in carrito.Lugares_Entrega
                                  where e.ID_Entrega == objVenta.ID_Entrega
                                  select e).Single();

                ViewBag.MontoEntrega = montoEnvio.MontoEntrega;
                ViewBag.PrecioCompra = objVenta.PrecioTotal + montoEnvio.MontoEntrega;
                Session["ID_Venta"] = null;
                return View(obj);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
