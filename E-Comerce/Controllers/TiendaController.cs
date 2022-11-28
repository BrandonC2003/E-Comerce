using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class TiendaController : Controller
    {
        E_ComerceDBDataContext E_ComerceDB = new E_ComerceDBDataContext();

        // GET: Tienda
        public ActionResult Index()
        {
            IEnumerable<Productos> lista = (from l in E_ComerceDB.Productos
                                            select l
                                            
                                            ).ToList();
            return View(lista);
        }
    }
}