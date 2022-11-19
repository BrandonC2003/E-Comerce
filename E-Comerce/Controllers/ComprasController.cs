using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Comerce.Controllers
{
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
        public ActionResult Create(FormCollection collection, Compras compras, List<Detalle_Compra> detcompras)
        {
            try
            {
                // TODO: Add insert logic here
                compras.ID_Usuario = 1;//int.Parse(Session[""].ToString());
                compras.Usuario_Inserta = "Eduardo"; //Session[""].ToString();
                var resultado = E_ComerceDB.SP_GuardarCompra(compras.ID_Usuario, compras.PrecioTotal, compras.Usuario_Inserta).Single();
                var ListaDetalleCompra = (from per in detcompras
                          where per.ID_Compra == null
                          select new Detalle_Compra
                          {
                              ID_Compra = resultado.IdTransaccion,
                              ID_Producto = per.ID_Producto,
                              Cantidad = per.Cantidad,
                              Total = per.Total,
                              Usuario_Inserta = compras.Usuario_Inserta,
                              Fecha_Inserta = compras.Fecha_Inserta
                          }).ToList();

                E_ComerceDB.Detalle_Compra.InsertAllOnSubmit(ListaDetalleCompra);
                
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
