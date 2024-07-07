using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ActivosFijos.Models.TableViewModel
{
    public class CarrosTableViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public decimal Precio { get; set; }
        public string Transmision { get; set; }
        public string Combustible { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen1 { get; set; }
        public byte[] Imagen2 { get; set; }
        public int CantidadEnStock { get; set; }
    }
}