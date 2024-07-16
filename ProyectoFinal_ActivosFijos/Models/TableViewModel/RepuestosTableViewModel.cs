using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.TableViewModel
{
    public class RepuestosTableViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnStock { get; set; }
        public byte[] Imagen1 { get; set; }
        public byte[] Imagen2 { get; set; }
        public string Estado { get; set; }
    }
}