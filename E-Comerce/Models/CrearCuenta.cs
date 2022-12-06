using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace E_Comerce.Models
{
    public class CrearCuenta
    {
        public string Usuario { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [Compare("Clave", ErrorMessage = "Las contraseñas deben coincidir")]
        [DataType(DataType.Password)]
        public string ClaveN { get; set; }

        [DataType(DataType.Password)]
        public string ClaveA { get; set; }

    }
}