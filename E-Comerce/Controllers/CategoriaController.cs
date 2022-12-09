using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class CategoriaController : Controller
    {
        E_ComerceDBDataContext comerce = new E_ComerceDBDataContext();

        //git prueba 
        // GET: Categoria
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<Categorias> Lista = (from c in comerce.Categorias
                                              select c).ToList();

                    return View(Lista);
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

        // GET: Categoria/Details/5 ------------------------------------------------
        public ActionResult Details(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Categorias objCategoria = (from ca in comerce.Categorias
                                               where ca.ID_Categoria == id
                                               select ca).Single();

                    return View(objCategoria);
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

        // GET: Categoria/Create ---------------------------------------------------
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

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Categorias datos)
        {
            try
            {

                string usuario = Session["Usuario"].ToString();

                comerce.InsertarCategorias(datos.Categoria, datos.Descripcion, usuario);

                comerce.SubmitChanges(); //actualiza la base de datos


                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5 --------------------------------------------------------
        public ActionResult Edit(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Categorias objCategoria = (from ca in comerce.Categorias
                                               where ca.ID_Categoria == id
                                               select ca).Single();

                    return View(objCategoria);
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

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Categorias datos)
        {
            try
            {
                // TODO: Add update logic here

                string usuario = Session["Usuario"].ToString(); ;

                comerce.EditarCategorias(id, datos.Categoria, datos.Descripcion, usuario); //procedimiento almacenado 

                comerce.SubmitChanges(); //actualiza la base de datos

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5 ---------------------------------------------------------
        public ActionResult Delete(int id)
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    Categorias objCategoria = (from ca in comerce.Categorias
                                               where ca.ID_Categoria == id
                                               select ca).Single();

                    return View(objCategoria);
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

        // POST: Categoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                comerce.EliminarCategoria(id); //procedimiento almacenado 

                comerce.SubmitChanges(); //actualiza la base de datos

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
