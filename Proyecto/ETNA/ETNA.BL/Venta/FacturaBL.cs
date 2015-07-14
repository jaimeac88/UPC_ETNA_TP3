using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETNA.DAL.Venta;
using ETNA.Domain.Venta;

namespace ETNA.BL.Venta
{
    public class FacturaBL
    {
        FacturaDAO pd = new FacturaDAO();




        public Boolean insertFactura(FacturaBE be, DataTable dt)

        {
            return pd.insertFacturaDetalle(be, dt);
        }
        public Boolean insertFacturaDetalleXLote(FacturaBE be, DataTable dt)
        
        {
            return pd.insertFacturaDetalleXLote(be, dt);
        }


        

        public Boolean deletefactura(int codigo)
        {
            return pd.DELETEFacturas(codigo);
        }

        public Boolean uptFactura(int codigofac, int concliente, DateTime fecemision, DateTime fecentrega)
        {
            return pd.updtfactura(codigofac, concliente, fecemision, fecentrega);
        }



        public DataTable getFacturas(string strfiltro,int codigotcp)
        {
            return pd.getFacturas(strfiltro, codigotcp);
        }


        public FacturaBE getFactura(int cod)
        {
            return pd.getFactura(cod);
        }

        public DataTable getdetalleFacturas(int cod)
        {
            return pd.obtenerDetalledFacturas(cod);
        }


        public Boolean deldetallefactura(int cod)
        {
            return pd.deletedetallefactura(cod);
        }

        public Boolean updetalleFACTURA(int cod, double d)
        {
            return pd.updatedetallefactura(cod, d);
        }

        public Boolean insertDetalleFactura(FacturaDetalleBE be)
        {
            return pd.insertDetalleSDEFactura(be);

        }

                   
    }
}
