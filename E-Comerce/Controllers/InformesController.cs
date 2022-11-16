using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class InformesController : Controller
    {
        E_ComerceDBDataContext informe = new E_ComerceDBDataContext();
        // GET: Informes
        public ActionResult Index()
        {
            DateTime fem = new DateTime(2016, 01, 06);
            DateTime fema = new DateTime(2022, 09, 30);
            List<SP_RetornarVentasResult> rv = (from v in informe.SP_RetornarVentas() select v).ToList();
            ViewBag.RetorV = rv;

            List<sp_ProMasVResult> pmv = (from c in informe.sp_ProMasV(fem, fema) select c).ToList();

            return View(pmv);
        }
        public ActionResult Grafico()
        {
            DateTime fem = new DateTime(2016, 01, 06);
            DateTime fema = new DateTime(2022, 09, 30);
            ISingleResult<sp_ProMasVResult> result =
         informe.sp_ProMasV(fem, fema);


            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Graficobar()
        {
            
            ISingleResult<SP_RetornarVentasResult> result =
         informe.SP_RetornarVentas();


            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}