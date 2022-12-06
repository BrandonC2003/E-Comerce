using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class UsuariosController : Controller
    {

        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();

        // GET: Usuarios
        public ActionResult Index()
        {

            List<Usuarios> lista = (from u in E_ComerceDB.Usuarios
                                            select u).ToList();

            return View(lista);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            Usuarios objUsuarios= (from u in E_ComerceDB.Usuarios
                                   where u.ID_Usuario ==id
                                    select u).Single();

            return View(objUsuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            var rol = (from r in E_ComerceDB.Rol
                       select r).ToList();

            ViewBag.rol = rol;

            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Usuarios datos)
        {
            try
            {
                datos.Fecha_Inserta = DateTime.Now;
                datos.Usuario_Inserta = "admin";

                E_ComerceDB.Usuarios.InsertOnSubmit(datos);
                E_ComerceDB.SubmitChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                    where u.ID_Usuario == id
                                    select u).Single();
            return View(objUsuarios);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Usuarios datos)
        {
            try
            {
                // TODO: Add update logic here

                //string Usuario = "admin";
                E_ComerceDB.Actualizar_Usuarios(datos.ID_Usuario,datos.CorreoElectronico, datos.Usuario, datos.Nombre, datos.Apellido, datos.Usuario_Actualiza);
                E_ComerceDB.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                    where u.ID_Usuario == id
                                    select u).Single();
            return View(objUsuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                // TODO: Add delete logic here
                Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                        where u.ID_Usuario == id
                                        select u).Single();

                E_ComerceDB.Usuarios.DeleteOnSubmit(objUsuarios);
                E_ComerceDB.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
