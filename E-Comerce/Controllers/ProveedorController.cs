using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{

    public class ProveedorController : Controller
    {
        E_ComerceDBDataContext dbproveedor = new E_ComerceDBDataContext();

        // GET: Proveedor
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<Proveedores> Listaproveedor = (from p in dbproveedor.Proveedores select p).ToList();
                    return View(Listaproveedor);
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

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Vw_Proveedor dtll = (from dp in dbproveedor.Vw_Proveedor
                                         where dp.ID_Proveedor == id
                                         select dp).Single();
                    return View(dtll);
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

        // GET: Proveedor/Create
        public ActionResult Create()
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

        // POST: Proveedor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Proveedores datos)
        {
            try
            {
                var usu = Session["Usuario"].ToString(); ;

                // TODO: Add insert logic here

                dbproveedor.SP_InsertarProveedor(datos.NombreEmpresa, datos.Telefono, usu);
                dbproveedor.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Proveedores editProveedor = (from p in dbproveedor.Proveedores
                                                 where p.ID_Proveedor == id
                                                 select p).Single();

                    return View(editProveedor);
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

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Proveedores datos)
        {
            try
            {
                var usuario = Session["Usuario"].ToString(); ;
                // TODO: Add update logic here

                dbproveedor.SP_ActualizarProveedor(id, datos.NombreEmpresa, datos.Telefono, usuario);
                dbproveedor.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    var dell = (from d in dbproveedor.Proveedores
                                where d.ID_Proveedor == id
                                select d).First();
                    return View(dell);
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

        // POST: Proveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                dbproveedor.SP_EliminarProveedor(id);
                dbproveedor.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
