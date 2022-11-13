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
            List<Categorias> Lista = (from c in comerce.Categorias
                                      select c).ToList();

            return View(Lista);
        }

        // GET: Categoria/Details/5 ------------------------------------------------
        public ActionResult Details(int id)
        {
            Categorias objCategoria = (from ca in comerce.Categorias
                                       where ca.ID_Categoria == id
                                       select ca).Single();

            return View(objCategoria);
        }

        // GET: Categoria/Create ---------------------------------------------------
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Categorias datos)
        {
            try
            {

                string usuario = "Fasuncion";

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
            Categorias objCategoria = (from ca in comerce.Categorias
                                       where ca.ID_Categoria == id
                                       select ca).Single();

            return View(objCategoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Categorias datos)
        {
            try
            {
                // TODO: Add update logic here

                string usuario = "Fasuncion";

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
            Categorias objCategoria = (from ca in comerce.Categorias
                                       where ca.ID_Categoria == id
                                       select ca).Single();

            return View(objCategoria);
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
