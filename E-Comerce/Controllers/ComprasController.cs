using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class ComprasController : Controller
    {
        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();
        
        public void AgregarDetalles()
        {
            DataTable detalles = new DataTable();
            detalles.Clear();
            detalles.Columns.Add("ID_Compra");
            detalles.Columns.Add("ID_Producto");
            detalles.Columns.Add("Cantidad");
            detalles.Columns.Add("Total");
            DataRow row = detalles.NewRow();
            row["ID_Compra"] = 1;
            row["ID_Producto"] = 1;
            row["Cantidad"] = 1;
            row["Total"] = 10;
            detalles.Rows.Add(row);

            DataTable DetCompra = new DataTable();
            DetCompra.Columns.Add("ID_Compra", typeof(Int64));
            DetCompra.Columns.Add("ID_Producto", typeof(Int64));
            DetCompra.Columns.Add("Cantidad", typeof(Int64));
            DetCompra.Columns.Add("Total", typeof(decimal));
            //Get student name of string type
             var courseList = E_ComerceDB.ExecuteCommand("SP_DetalleCompra", DetCompra);
            //Database.SqlQuery<Course>("exec GetCoursesByStudentId @StudentId ", idParam).ToList<Course>();

        }
        // GET: Compras
        public ActionResult Index()
        {
            List<V_Compras> listaCompras = (from p in E_ComerceDB.V_Compras
                                          select p).ToList(); 
            return View(listaCompras);
        }

        // GET: Compras/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Compras/Create
        public ActionResult Prueba()
        {
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var items = E_ComerceDB.SP_GuardarCompra(1,10,DateTime.Now);
                AgregarDetalles();
                //var id = items[0].
                var id = items.Select(p => p.Column1).FirstOrDefault();

                E_ComerceDB.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Compras/Edit/5
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

        // GET: Compras/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Compras/Delete/5
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
