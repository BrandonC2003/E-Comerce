using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class DetalleComprasController : Controller
    {
        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();
        // GET: DetalleCompras
        public ActionResult Index()
        {
            return View();
        }

        public void AgregarACompra(V_DetalleCompra datos)
        {
            Productos producto = (from c in E_ComerceDB.Productos
                             where c.ID_Producto == datos.ID_Producto
                             select c).Single();
            if (Session["Compra"] == null)
            {
                List<V_DetalleCompra> detcompra = new List<V_DetalleCompra>();
                detcompra.Add(new V_DetalleCompra
                {
                    ID_Producto = Convert.ToInt32(datos.ID_Producto),
                    NombreProducto = producto.NombreProducto,
                    Cantidad = Convert.ToInt32(datos.Cantidad),
                    PrecioUnitario = datos.PrecioUnitario,
                    Total = datos.Total
                });
                Session["Compra"] = detcompra;
            }
            else
            {
                List<V_DetalleCompra> detcompra = (List<V_DetalleCompra>)Session["Compra"];                
                int existeIndex = ObtenerIdPpoducto(Convert.ToInt32(datos.ID_Producto));
                if (existeIndex == -1)
                    detcompra.Add(new V_DetalleCompra
                    {
                        ID_Producto = Convert.ToInt32(datos.ID_Producto),
                        NombreProducto = producto.NombreProducto,
                        Cantidad = Convert.ToInt32(datos.Cantidad),
                        PrecioUnitario = datos.PrecioUnitario,
                        Total = datos.Total
                    });
                else
                    detcompra[existeIndex].Cantidad= detcompra[existeIndex].Cantidad + datos.Cantidad;
                Session["Compra"] = detcompra;                
            }
        }       
        private int ObtenerIdPpoducto(int id)
        {
            List<V_DetalleCompra> detcompra = (List<V_DetalleCompra>)Session["Compra"];        
            for (int i = 0; i < detcompra.Count; i++)
            {
                if(detcompra[i].ID_Producto == id)
                    return i;
            }
            return -1;
        }
        // GET: DetalleCompras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetalleCompras/Create
        public ActionResult Create()
        {
            List<Productos> products = (from p in E_ComerceDB.Productos select p).ToList();

            ViewBag.Lista = products;
            return View();
        }

        // POST: DetalleCompras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, V_DetalleCompra datos)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    AgregarACompra(datos);
                }
                return RedirectToAction("Create", "Compras");
            }
            catch
            {
                return View();
            }
        }

        // GET: DetalleCompras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleCompras/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DetalleCompras/Delete/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                List<V_DetalleCompra> detCompra = (List<V_DetalleCompra>)Session["Compra"];
                detCompra.RemoveAt(ObtenerIdPpoducto(id));
                return RedirectToAction("Create", "Compras");
            }
            catch
            {
                return RedirectToAction("Create", "Compras");
            }

        }
        // POST: DetalleCompras/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
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
