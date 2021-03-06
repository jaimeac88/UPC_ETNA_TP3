﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ETNA.Domain.Venta;
using ETNA.DAL.Venta;

namespace ETNA.BL.Venta
{
    public class ProductoBL
    {

        ProductosDAO c = new ProductosDAO();
        public DataTable ObtenerProductos()
        {

            return c.ObtenerProductos();
        }

        public DataTable ObtenerProductosFiltro(String filtro)
        {

            return c.ObtenerProductosFiltro(filtro);
        }


        public DataTable ObtenerProductos_SERV()
        {

            return c.ObtenerProductos_SERV();
        }

        public DataTable ObtenerProductosFiltro_SERV(String filtro)
        {

            return c.ObtenerProductosFiltro_SERV(filtro);
        }

        public  int obteneridproducto (String strcodigo)
        {

            return c.ObtenerCodigoIDProducto(strcodigo);
        }

    }
}
