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
    
    public partial class TB_PV_Plantillas
    {
        public TB_PV_Plantillas()
        {
            this.TB_PV_InformesResultado = new HashSet<TB_PV_InformesResultado>();
            this.TB_PV_PlantillasDetalle = new HashSet<TB_PV_PlantillasDetalle>();
            this.TB_PV_Programaciones = new HashSet<TB_PV_Programaciones>();
        }
    
        public int PlantillaId { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int TipoPlantillaId { get; set; }
    
        public virtual ICollection<TB_PV_InformesResultado> TB_PV_InformesResultado { get; set; }
        public virtual ICollection<TB_PV_PlantillasDetalle> TB_PV_PlantillasDetalle { get; set; }
        public virtual TB_PV_TiposPlantilla TB_PV_TiposPlantilla { get; set; }
        public virtual ICollection<TB_PV_Programaciones> TB_PV_Programaciones { get; set; }
    }
}