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
            if (Session["Usuario"]!=null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<vw_Usuario> lista = (from u in E_ComerceDB.vw_Usuario
                                              select u).ToList();

                    return View(lista);
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

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {

            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                            where u.ID_Usuario == id
                                            select u).Single();

                    return View(objUsuarios);
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

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    var rol = (from r in E_ComerceDB.Rol
                               select r).ToList();

                    ViewBag.rol = rol;

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

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Usuarios datos)
        {
            try
            {
                datos.Fecha_Inserta = DateTime.Now;
                datos.Usuario_Inserta = Session["Usuario"].ToString();

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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    var rol = (from r in E_ComerceDB.Rol
                               select r).ToList();

                    ViewBag.rol = rol;

                    Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                            where u.ID_Usuario == id
                                            select u).Single();
                    return View(objUsuarios);
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

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Usuarios datos)
        {
            try
            {
                // TODO: Add update logic here

                datos.Usuario = Session["Usuario"].ToString();
                E_ComerceDB.Sp_EditarUsuarioss(datos.ID_Rol,datos.CorreoElectronico, datos.Usuario, datos.Nombre, datos.Apellido,datos.Clave,id);
               

                datos.Fecha_Actualiza = DateTime.Now;
                datos.Usuario_Actualiza = "admin";

              
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
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Usuarios objUsuarios = (from u in E_ComerceDB.Usuarios
                                            where u.ID_Usuario == id
                                            select u).Single();
                    return View(objUsuarios);
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

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
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
