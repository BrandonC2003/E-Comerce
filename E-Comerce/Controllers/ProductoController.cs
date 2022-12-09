using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class ProductoController : Controller
    {
        E_ComerceDBDataContext comerce = new E_ComerceDBDataContext();
        // GET: Producto
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {

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
            List<V_Producto> Lista = (from c in comerce.V_Producto
                                      select c).ToList();
            return View(Lista);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Productos objproductos = (from cp in comerce.Productos
                                              where cp.ID_Producto == id
                                              select cp).Single();
                    return View(objproductos);
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
        
        // GET: Producto/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    var listacate = (from c in comerce.Categorias
                                     select c).ToList();

                    ViewBag.listacate = listacate;

                    var listaprov = (from c in comerce.Proveedores
                                     select c).ToList();

                    ViewBag.listaprov = listaprov;
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

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Productos datos)
        {
            datos.Usuario_Inserta = Session["Usuario"].ToString(); ;
            datos.Fecha_Inserta = DateTime.Now;

            comerce.Productos.InsertOnSubmit(datos);
            comerce.SubmitChanges();

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

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Productos objproductos = (from cp in comerce.Productos
                                              where cp.ID_Producto == id
                                              select cp).Single();

                    var listacate = (from c in comerce.Categorias
                                     select c).ToList();

                    ViewBag.listacate = listacate;

                    var listaprov = (from c in comerce.Proveedores
                                     select c).ToList();

                    ViewBag.listaprov = listaprov;
                    return View(objproductos);
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

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Productos datos)
        {
            try
            {
                string usuario = Session["Usuario"].ToString(); ;
                comerce.SP_ACTUALIZAR_PRODUCTOS1(id, datos.ID_Categoria, datos.ID_Proveedor, datos.PrecioCompra, datos.PrecioVenta, datos.Descuento, datos.Imagen, datos.Descripcion, datos.cantidadDisponible, usuario);
                comerce.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Productos objproductos = (from cp in comerce.Productos
                                              where cp.ID_Producto == id
                                              select cp).Single();
                    return View(objproductos);
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

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                Productos objproductos = (from cp in comerce.Productos
                                          where cp.ID_Producto == id
                                          select cp).Single();

                comerce.Productos.DeleteOnSubmit(objproductos);
                comerce.SubmitChanges();

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
