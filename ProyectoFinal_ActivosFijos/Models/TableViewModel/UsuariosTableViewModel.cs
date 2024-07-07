using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.TableViewModel
{
    public class UsuariosTableViewModel
    {

        public int Id { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int Edad { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public int TipoDeUsuario { get; set; }
        public string Contrasena { get; set; }
        public string ProductosEnCarrito { get; set; }

    }
}