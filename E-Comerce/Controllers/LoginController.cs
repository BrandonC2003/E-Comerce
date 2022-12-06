using E_Comerce.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Comerce.Models;
using Microsoft.Ajax.Utilities;

namespace E_Comerce.Controllers
{
    public class LoginController : Controller
    {
        E_ComerceDBDataContext comerce = new E_ComerceDBDataContext();
        Service1Client wcfLogin = new Service1Client();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Login(RespuestaUsuario login)
        {
            string controlador = "";
            try
            {
                RespuestaUsuario datos;

                datos=wcfLogin.InicioSesion(login);

                if (datos.NombreUsuario!=null)
                {
                    Session["Usuario"] = datos.NombreUsuario;
                    Session["Rol_Usuario"] = datos.ID_Rol;
                    Session["ID_Usuario"] = datos.ID_Usuario;

                    
                    switch (datos.ID_Rol)
                    {
                        case 1:
                            controlador = "Informes";
                            break;
                        case 2:
                            controlador = "Ventas";
                            break;
                        case 3:
                            controlador = "Tienda";
                            break;
                    }
                    return RedirectToAction("Index", controlador);
                }
                else
                {
                    return View("Index");
                }
            }
            catch
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearCuenta(CrearCuenta datos)
        {
            try
            {
                comerce.sp_RegistrarUsuario(datos.Nombre,datos.Apellido,datos.Correo,datos.Usuario,datos.Clave);
                comerce.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Cambiarclave()
        {
            if (Session["ID_Usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(CrearCuenta resp)
        {
            try
            {
                string mensaje = String.Empty;
                CambiarClave datos = new CambiarClave
                {
                    ID_Usuario = Convert.ToInt32(Session["ID_Usuario"]),
                    ClaveAct = resp.ClaveA,
                    ClaveNew = resp.Clave
                };

                bool result = wcfLogin.CambiarClave(datos,out mensaje);

                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index","Tienda");
        }
    }
}
