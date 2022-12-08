using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
            if (Session["Usuario"] != null)
            {
                //consultar detalle venta
                List<vw_Carrito> ventasV = (from v in carrito.vw_Carrito where v.ID_Venta == null && v.Usuario_Inserta == Session["Usuario"].ToString() select v).ToList();
                decimal total = 0, entrega=0;
                foreach (var item in ventasV)
                {
                    total += Convert.ToDecimal(item.Precio - item.descuento);
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
            int idUsuario = Convert.ToInt32(Session["ID_Usuario"]);
            int idEntrega = Convert.ToInt32(Session["ID_Entrega"]);
            string usuario = Session["Usuario"].ToString();
            Session["Monto"] = null;
            Session["ID_Entrega"] = null;
            carrito.sp_RegistrarVentaCliente(idUsuario,idEntrega,usuario);
            carrito.SubmitChanges();
            return RedirectToAction("Index","Tienda");
        }
    }
}
