using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFLogin
{
    // NOTe: puede usar el comando "Rename" del menú "efactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);


        [OperationContract]
        RespuestaUsuario InicioSesion(RespuestaUsuario datos);

        [OperationContract]
        bool CambiarClave(CambiarClave resp, out string mensaje);

        // TODO: agregue aquí sus operaciones de servicio
    }


    [DataContract]
    public class RespuestaUsuario
    {
        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }

        [DataMember]
        public string NombreUsuario { get; set; }

        [DataMember]
        public int ID_Rol { get; set; }

        [DataMember]
        public int ID_Usuario { get; set; }

    }

    [DataContract]
    public class CambiarClave
    {
        [DataMember]
        public int ID_Usuario { get; set; }
        [DataMember]
        public string ClaveAct { get; set; }
        [DataMember]
        public string ClaveNew { get; set; }
    }
        // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
