using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ETNA.Domain.Venta;
using ETNA.BL.Venta;
    using System.Threading;
using System.Globalization;

namespace ETNA.SGI.Presentacion.Venta
{
    public partial class frmCuotaVenta : DevExpress.XtraEditors.XtraForm
    {

        public DataTable dtzona = new DataTable();
        public DataTable dtdetalle = new DataTable();
        public DataTable dtvendedor = new DataTable();
        public DataTable dtproducto = new DataTable();

        CuentaVentaBL cbl = new CuentaVentaBL();

        DataTable dtdetallescuotas = new DataTable();

    



        public frmCuotaVenta()
        {
            InitializeComponent();


         

            dtzona.Columns.Add("Codigo", typeof(Int32));
            dtzona.Columns.Add("Nombre", typeof(String));

            dtvendedor.Columns.Add("Codigo", typeof(Int32));
            dtvendedor.Columns.Add("Nombre", typeof(String));
            dtvendedor.Columns.Add("Zona", typeof(String));

            dtproducto.Columns.Add("Codigo", typeof(Int32));
            dtproducto.Columns.Add("Nombre", typeof(String));
            dtproducto.Columns.Add("CodVendedor", typeof(Int32));
            dtproducto.Columns.Add("Cuota", typeof(Double));

            dtdetalle.Columns.Add("cuotaid", typeof(Int32));
            dtdetalle.Columns.Add("producid", typeof(Int32));
            dtdetalle.Columns.Add("idVendedor", typeof(Int32));
            dtdetalle.Columns.Add("zonaId", typeof(Int32));
            dtdetalle.Columns.Add("cuota", typeof(Decimal));


        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            
                new frmConsultaxZona(this).Show();

           
        }


        public void actualizarZonas()
        {
            dtgZona.Rows.Clear();

            //dtzona.DataSet = dtgZona;

            if (dtzona.Rows.Count >= 1)
            {
                foreach (DataRow row in dtzona.Rows)
                {

                    dtgZona.Rows.Add(row[0].ToString(),
                        row[1].ToString());


                }
            }




        }


        public void eliminadetallevendedor(String strzona){
            if (dtvendedor.Rows.Count >= 1)
            {

                for (int i = dtvendedor.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtvendedor.Rows[i];
                    if (dr["Zona"].ToString() == strzona)
                        { dr.Delete();
                            string codigo = dr["Codigo"].ToString();
                        eliminadetalleproducts(codigo);
                        }

                }

            }
        }




        public void eliminadetalleproducts(String strvende)
        {
            if (dtvendedor.Rows.Count >= 1)
            {

                for (int i = dtproducto.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtproducto.Rows[i];
                    if (dr["CodVendedor"].ToString() == strvende)
                        dr.Delete();

                }

            }
        }


        public void eliminadetalleproductsxpro(String strcodpro)
        {
            if (dtvendedor.Rows.Count >= 1)
            {

                for (int i = dtproducto.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtproducto.Rows[i];
                    if (dr["Codigo"].ToString() == strcodpro)
                        dr.Delete();

                }

            }
        }


        public void actualizaVendedor(String strZona)
        {

            dtgVendedor.Rows.Clear();
            if (dtvendedor.Rows.Count >= 1)
            {
                foreach (DataRow row in dtvendedor.Rows)
                {
                    if (row[2].ToString() == strZona)  {
                        dtgVendedor.Rows.Add(row[0].ToString(),
                                            row[1].ToString(), row[2].ToString()
                                            );
                    };

                


                }
            }




        }

        public void actualizamonto()
        {
            if (dtproducto.Rows.Count >= 1)
            {
                decimal monto = 0;
                foreach (DataRow row in dtproducto.Rows)
                {
                    decimal de = decimal.Parse(row[3].ToString());
                    monto = monto + de;
                }
                txtCuota.Text = monto.ToString();
            }
        }



        public void actualizaProductos(string strcodven )

        {
            dtgProdu.Rows.Clear();
             if (dtvendedor.Rows.Count >= 1)
            {
                foreach (DataRow row in dtproducto.Rows)
                {
                    string codigovende = row[2].ToString();
                    if (codigovende == strcodven)  {
                        dtgProdu.Rows.Add(row[0].ToString(),
                                            row[1].ToString(),
                                            row[2].ToString(),
                                            row[3].ToString() );
                    }

                


                }
                actualizamonto();
            }



        }


        private void dtgZona_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dtgZona.Rows.Count >= 1)
            {
                String cod = this.dtgZona.SelectedCells[0].Value.ToString();
                actualizaVendedor(cod);
            }
            
        }

        private void dtgVendedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgVendedor.Rows.Count >= 1)
            {
                String codven = this.dtgVendedor.SelectedCells[0].Value.ToString();
                actualizaProductos(codven);
            }

        }


        private void recolectardatabledetalle ()
        {
            
            CuentaDetalleVentaBE cte = new CuentaDetalleVentaBE();

            foreach (DataRow row in dtproducto.Rows)
            {
            
                   Decimal valor = Convert.ToDecimal((row[3].ToString()));
               
              
                cte.CuotaId = 0;
                cte.IdVendedor = int.Parse(row[2].ToString());
                cte.ProductoId = int.Parse(row[0].ToString());
                foreach (DataRow row2 in dtvendedor.Rows)
            {

        

                int codven = int.Parse(row2[0].ToString());
                int zonaid = int.Parse(row2[2].ToString());

                    if (codven == cte.IdVendedor)
                    {
                        //dtdetalle.Rows.Add(0, cte.ProductoId, cte.IdVendedor, zonaid, double.Parse(Cuota.ToString()));

                        dtdetalle.Rows.Add(0, cte.ProductoId, cte.IdVendedor, zonaid, valor);
                   
                    }
              
            }
                
            }

        }

        private void dtgProdu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dtgZona.RowCount >0)
            {
                 String codigo = this.dtgZona.SelectedCells[0].Value.ToString();

                if (codigo != "")
                {

                    MessageBox.Show("Se selecciono la Zona : " + codigo);
                new frmConsultarVendedor(this,codigo).Show();
                      }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar una Zona");
            }

          
        }


        public void eliminar_depenciasdezonas(String strCod )
        {

                   if (dtgZona.Rows.Count >= 1)
                {
             
                string codigo = this.dtgZona.SelectedCells[0].Value.ToString();

                    for (int i = dtzona.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dtzona.Rows[i];
                        if (dr["codigo"].ToString() == strCod)
                            dr.Delete();

                    }
                   
                    eliminadetallevendedor(codigo);

                }

            if (dtgZona.Rows.Count >= 1)
            {
               
                string cod = this.dtgZona.SelectedCells[0].Value.ToString();
                
                actualizaVendedor(cod);
                 if (dtgVendedor.Rows.Count >= 1)
                  {
                string codemp = this.dtgZona.SelectedCells[0].Value.ToString();

                actualizaProductos(codemp);
                     }

            }
            else
            {

                dtgZona.Rows.Clear();
                dtgVendedor.Rows.Clear();
                dtgProdu.Rows.Clear();

                dtzona.Rows.Clear();
                dtvendedor.Rows.Clear();
                dtproducto.Rows.Clear();

            }




            
           // actualizaProductos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtgZona.Rows.Count >= 1) {
                   
                string cod= this.dtgZona.SelectedCells[0].Value.ToString();

                eliminar_depenciasdezonas(cod);
                actualizarZonas();
            }
        }

        public static string CapitalizeFirstLetter(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        private void frmCuotaVenta_Load(object sender, EventArgs e)
        {

            // Put the following code before InitializeComponent()
            // Sets the culture to French (France)
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            // Sets the UI culture to French (France)
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");


            foreach (DataGridViewColumn dc in dtgProdu.Columns)
            {
                if (dc.Index.Equals(3))
                {
                    dc.ReadOnly = false;
                }
                else if (dc.Index.Equals(0) || dc.Index.Equals(1) || dc.Index.Equals(2))
                {
                    dc.ReadOnly = true;
                }

            }


            cboanio.Items.Add("2015");
            cboanio.Items.Add("2014");
            cboanio.Items.Add("2013");
            cboanio.Items.Add("2012");
            cboanio.Items.Add("2011");
            cboanio.Items.Add("2010");


            cboanio.SelectedIndex = 0;


            int codigo = int.Parse(cboanio.SelectedItem.ToString());
            actualizaperiodo(codigo);
            
           


        }

        public void actualizaperiodo(int codanio)
        {

            dtdetallescuotas = cbl.validaporanio(codanio);
            cboperio.Items.Clear();
            if (dtdetallescuotas.Rows.Count == 0 ) {


                for (int i = 1; i <= 12; i++)
                {

                    string data = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                    cboperio.Items.Add(CapitalizeFirstLetter(data));
                }
              


   

                
            }
            else
            {
           
                 
                     
                    
                         for (int i = 1; i <= 12; i++)
                         {
                             Boolean considera = true;
                             foreach (DataRow row in dtdetallescuotas.Rows)
                             {
                                   int anio = int.Parse(row[0].ToString());
                                         if (i==anio)
                                         {
                                             considera = false;
                                         } 
                             }

                                if (considera)
                                 {
                                     string data = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                                     cboperio.Items.Add(CapitalizeFirstLetter(data)); 
                                 }
                         }
                      
                    
                  
                     
                     }




            cboperio.SelectedIndex = 0;
                           


            }
            

          
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dtgVendedor.RowCount > 0)
            {
                String codigo = this.dtgVendedor.SelectedCells[0].Value.ToString();

                if (codigo != "")
                {

                    MessageBox.Show("Se selecciono el vendedpr : " + codigo);
                    new frmConsultarProducto(this, codigo).Show();
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un Vendedor");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtgVendedor.Rows.Count >= 1)
            {

                string cod = this.dtgVendedor.SelectedCells[0].Value.ToString();

                eliminadetallevendedor(cod);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dtgProdu.Rows.Count >= 1)
            {

                string cod = this.dtgProdu.SelectedCells[0].Value.ToString();

                eliminadetalleproductsxpro(cod);
               if (dtgVendedor.Rows.Count >= 1)
            {
                string codven = this.dtgVendedor.SelectedCells[0].Value.ToString();

                actualizaProductos(codven);
            }
            }
        }

        

        public int transformames(string v)
        {
            int codigo = 0;
            
             for (int i = 1; i <= 12; i++)
                {

                    string data = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                    if (CapitalizeFirstLetter(data) == v)
                    {
                        codigo = i;
                    }
                }

            return codigo;
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (dtgProdu.Rows.Count>=1)
            {

               
                
                CuentaVentaBL cbl = new CuentaVentaBL();
                CuentaVentaBE cbe = new CuentaVentaBE();
                cbe.Año = int.Parse(cboanio.SelectedItem.ToString());
                cbe.Dias_Habiles = txtDiasHabiles.Text;
                cbe.Mes = transformames(cboperio.SelectedItem.ToString());

                cbe.Monto_Cuota = decimal.Parse(txtCuota.Text);
                cbe.CuotaId = 0;

                recolectardatabledetalle();

                if (cbl.insertcuentaventadet(cbe, dtdetalle))
                {
                    MessageBox.Show("Los datos fueron registrados correctamente");
                    this.Close();
                 
                }
                else
                {
                    MessageBox.Show("No se registraron los datos , por favor comunicarse con el administrador de sistemas");
                    this.Close();
                }
                    
            }
        }

        public decimal ConvertStringDecimal(string stringVal)
        {
            decimal decimalVal = 0;

            try
            {
                decimalVal = System.Convert.ToDecimal(stringVal);
                System.Console.WriteLine(
                    "The string as a decimal is {0}.", decimalVal);
            }
            catch (System.OverflowException)
            {
                System.Console.WriteLine(
                    "The conversion from string to decimal overflowed.");
            }
            catch (System.FormatException)
            {
                System.Console.WriteLine(
                    "The string is not formatted as a decimal.");
            }
            catch (System.ArgumentNullException)
            {
                System.Console.WriteLine(
                    "The string is null.");
            }

            // Decimal to string conversion will not overflow.
            stringVal = System.Convert.ToString(decimalVal);
            System.Console.WriteLine(
                "The decimal as a string is {0}.", stringVal);

            return decimalVal;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboanio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int codigo = int.Parse(cboanio.SelectedItem.ToString());

            actualizaperiodo(codigo);
        }

        private void cboanio_Click(object sender, EventArgs e)
        {
           
       
        }

        private void cboanio_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cboanio_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void dtgProdu_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            actualizamonto();
        }

        private void dtgProdu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            actualizamonto();
        }	

    }
}