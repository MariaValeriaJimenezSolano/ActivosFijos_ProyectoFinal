//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal_ActivosFijos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
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
        public string ProductosEnCarrito { get; set; }
    
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
