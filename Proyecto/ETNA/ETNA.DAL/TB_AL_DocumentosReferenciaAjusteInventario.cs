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
    
    public partial class TB_AL_DocumentosReferenciaAjusteInventario
    {
        public int Estado { get; set; }
        public int DocumentoReferenciaId { get; set; }
    
        public virtual TB_AL_DocumentosReferencia TB_AL_DocumentosReferencia { get; set; }
    }
}