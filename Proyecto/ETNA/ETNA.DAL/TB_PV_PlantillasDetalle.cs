//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETNA.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_PV_PlantillasDetalle
    {
        public TB_PV_PlantillasDetalle()
        {
            this.TB_PV_Encuestas = new HashSet<TB_PV_Encuestas>();
        }
    
        public int PlantillaDetalleId { get; set; }
        public string CodigoPregunta { get; set; }
        public string DescripcionPregunta { get; set; }
        public string Respuesta1 { get; set; }
        public string Respuesta2 { get; set; }
        public string Respuesta3 { get; set; }
        public string Respuesta4 { get; set; }
        public string Respuesta5 { get; set; }
        public int PlantillaId { get; set; }
    
        public virtual TB_PV_Plantillas TB_PV_Plantillas { get; set; }
        public virtual ICollection<TB_PV_Encuestas> TB_PV_Encuestas { get; set; }
    }
}