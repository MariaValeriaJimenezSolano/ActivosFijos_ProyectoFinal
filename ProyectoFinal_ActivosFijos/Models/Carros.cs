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
    
    public partial class Carros
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public string Transmision { get; set; }
        public string Combustible { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen1 { get; set; }
        public byte[] Imagen2 { get; set; }
        public Nullable<int> CantidadEnStock { get; set; }
    }
}
