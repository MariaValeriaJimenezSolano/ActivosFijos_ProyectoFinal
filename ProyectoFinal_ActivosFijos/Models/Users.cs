//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal_ActivosFijos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int Id { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> TipoDeUsuario { get; set; }
        public string Contrasena { get; set; }
        public string MarcaVendedor { get; set; }
        public string ProductosEnCarrito { get; set; }
        public string socialMediaID { get; set; }
        public string PreguntaSeguridad { get; set; }
        public string RespuestaPreguntaSeguridad { get; set; }
    
        public virtual UserType UserType { get; set; }
    }
}
