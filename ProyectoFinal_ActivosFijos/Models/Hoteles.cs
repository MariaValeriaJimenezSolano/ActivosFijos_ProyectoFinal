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
    
    public partial class Hoteles
    {
        public int IDHotel { get; set; }
        public string TipoDeHabitacion { get; set; }
        public Nullable<int> CantidadDePersonas { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public string NombreHotel { get; set; }
        public byte[] Imagen { get; set; }
        public byte[] Imagen2 { get; set; }
        public byte[] Imagen3 { get; set; }
        public string web { get; set; }
        public string ComentarioHotel { get; set; }
        public Nullable<int> CalificacionHotel { get; set; }
    }
}
