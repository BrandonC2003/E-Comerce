using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFLogin
{
    //Comentario de prueba del wcf
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {

        DBEcomerceDataContext db = new DBEcomerceDataContext();

        public string GetData(int value)
        {
            value = Convert.ToInt32(value); 
            return string.Format("You1 entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public RespuestaUsuario InicioSesion (RespuestaUsuario datos)
        {
            var usuario = (from usu in db.Usuarios
                           where usu.Usuario == datos.Usuario
                           && usu.Clave == datos.Contrasena
                           select usu).ToList();

            if (usuario.Count>0)
            {
                datos.ID_Usuario = Convert.ToInt32(usuario[0].ID_Usuario);
                datos.ID_Rol = Convert.ToInt32(usuario[0].ID_Rol);
                datos.NombreUsuario = usuario[0].Usuario;

            }

            return datos;
        }

        public bool CambiarClave(CambiarClave resp,out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                sp_CambiarClaveResult result= db.sp_CambiarClave(resp.ID_Usuario,resp.ClaveAct,resp.ClaveNew).Single();
                db.SubmitChanges();
                mensaje = result.ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
