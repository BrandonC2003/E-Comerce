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
            List<Proveedores> Listaproveedor = (from p in dbproveedor.Proveedores select p).ToList();
            return View(Listaproveedor);
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {

            Vw_Proveedor dtll = (from dp in dbproveedor.Vw_Proveedor
                                 where dp.ID_Proveedor == id
                                 select dp).First();
            return View(dtll);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Proveedores datos)
        {
            try
            {
                var usu = "jmartinez";

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
            Proveedores editProveedor = (from p in dbproveedor.Proveedores
                                         where p.ID_Proveedor == id
                                         select p).Single();

            return View(editProveedor);
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Proveedores datos)
        {
            try
            {
                var usuario = "jmartinez";
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
            var dell = (from d in dbproveedor.Proveedores
                        where d.ID_Proveedor == id
                        select d).First();
            return View(dell);
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
