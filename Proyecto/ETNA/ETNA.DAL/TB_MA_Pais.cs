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
    
    public partial class TB_MA_Pais
    {
        public TB_MA_Pais()
        {
            this.TB_EX_IATA = new HashSet<TB_EX_IATA>();
            this.TB_Requerimiento = new HashSet<TB_Requerimiento>();
        }
    
        public int CodigoPais { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<TB_EX_IATA> TB_EX_IATA { get; set; }
        public virtual ICollection<TB_Requerimiento> TB_Requerimiento { get; set; }
    }
}