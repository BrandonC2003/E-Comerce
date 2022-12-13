using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.Mvc;

namespace E_Comerce.Controllers
{
    public class InformesController : Controller
    {
        E_ComerceDBDataContext informe = new E_ComerceDBDataContext();
        // GET: Informes
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Rol_Usuario"]) == 1)
                {
                    List<SP_RetornarVentasResult> rv = (from v in informe.SP_RetornarVentas() select v).ToList();
                    ViewBag.RetorV = rv;

                    List<sp_ProMasVResult> pmv = (from c in informe.sp_ProMasV() select c).ToList();

                    return View(pmv);
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
        public ActionResult Grafico()
        {

            ISingleResult<sp_ProMasVResult> result =
         informe.sp_ProMasV();


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