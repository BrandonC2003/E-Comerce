using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Comerce.Controllers
{
    public class DetCompras
    {
        public int ID_Compra { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
    };
    public class ComprasController : Controller
    {
        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();

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
                //var items = E_ComerceDB.SP_GuardarCompra(1,10,DateTime.Now);
                //var id = items[0].
                //var id = items.Select(p => p.Column1).FirstOrDefault();


                /*DetCompras dt = new DetCompras();
                dt.ID_Compra = 1;
                dt.ID_Producto = 1;
                dt.Cantidad = 12;
                dt.Total = 3;*/

               /* DataTable d1t = new DataTable();
                d1t.Clear();
                d1t.Columns.Add("ID_Compra", typeof(int));
                d1t.Columns.Add("ID_Producto", typeof(int));
                d1t.Columns.Add("Cantidad",typeof(int));
                d1t.Columns.Add("Total", typeof(Decimal));
                DataRow _ravi = d1t.NewRow();
                _ravi["ID_Compra"] = 1;
                _ravi["ID_Producto"] = 1;
                _ravi["Cantidad"] = 2;
                _ravi["Total"] = 500.00;
                d1t.Rows.Add(_ravi);*/


                var table = new DataTable();
                table.Columns.Add("ID_Compra", typeof(int));
                table.Columns.Add("ID_Producto", typeof(int));
                table.Columns.Add("Cantidad", typeof(int));
                table.Columns.Add("Total", typeof(decimal));
                DataRow _ravi = table.NewRow();
                _ravi["ID_Compra"] = 1;
                _ravi["ID_Producto"] = 1;
                _ravi["Cantidad"] = 2;
                _ravi["Total"] = 500.00;
                table.Rows.Add(_ravi);
                var pList = new SqlParameter("@DetCompras", SqlDbType.Structured);
                var resultado = E_ComerceDB.ExecuteCommand("EXEC SP_GuardarDetalleCompra", pList);


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
