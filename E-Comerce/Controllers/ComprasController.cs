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
            Session["Compra"] = null;
            if (Session["Compra"] == null)
            {
                List<V_DetalleCompra> detcompra = (from p in E_ComerceDB.V_DetalleCompra
                                                   where p.ID_Compra == id
                                                   select p).ToList();
                V_Compras Compra = (from c in E_ComerceDB.V_Compras 
                               where c.Id == id 
                               select c).Single();
                Session["Compra"] = detcompra;
                return View(Compra);
            }
            return RedirectToAction("Index", "Compras");
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Nuevacompra()
        {
            Session["Compra"] = null;
            return RedirectToAction("Create", "Compras");
        }

        // POST: Compras/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Compras compras)
        {
            try
            {
                // TODO: Add insert logic here
                if (Session["Compra"] != null)
                {
                    List<Detalle_Compra> detCompras = (List<Detalle_Compra>)Session["Compra"];
                    if (detCompras.Count > 0)
                    {
                        compras.ID_Usuario = 1;//int.Parse(Session[""].ToString());
                        compras.Usuario_Inserta = "Eduardo"; //Session[""].ToString();
                        compras.PrecioTotal = detCompras.Sum(x => x.Cantidad * x.PrecioUnitario);

                        var resultado = E_ComerceDB.SP_GuardarCompra(compras.ID_Usuario, compras.PrecioTotal, compras.Usuario_Inserta).Single();
                        var ListaDetalleCompra = (from d in detCompras
                                                  where d.ID_Compra == null
                                                  select new Detalle_Compra
                                                  {
                                                      ID_Compra = resultado.IdTransaccion,
                                                      ID_Producto = d.ID_Producto,
                                                      Cantidad = d.Cantidad,
                                                      PrecioUnitario = d.PrecioUnitario,
                                                      Total = d.Cantidad * d.PrecioUnitario,
                                                      Usuario_Inserta = compras.Usuario_Inserta,
                                                      Fecha_Inserta = DateTime.Now,
                                                  }).ToList();

                        E_ComerceDB.Detalle_Compra.InsertAllOnSubmit(ListaDetalleCompra);

                        E_ComerceDB.SubmitChanges();
                        Session["Compra"] = null;
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Create", "Compras");
                }
                return RedirectToAction("Create", "Compras");
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
