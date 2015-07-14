﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using ETNA.SGI.Data.Ventas;
using System.Threading.Tasks;
//using ETNA.SGI.Bussiness.Ventas;
using DevExpress.XtraPrinting.Export.Pdf;
using ETNA.Domain.Venta;
using ETNA.BL.Venta;

namespace ETNA.SGI.Presentacion.Venta

{

    public partial class frmComprobanteVenta :  DevExpress.XtraEditors.XtraForm

    {

        public EmpleadosBE emp = new EmpleadosBE();
        public ClienteBE cli = new ClienteBE();


        FacturaBE ped = new FacturaBE();

        MonedaBL MON = new MonedaBL();

       public DataTable dtdetalle = new DataTable();

        double total = 0;

        public frmComprobanteVenta()
        {
            InitializeComponent();
        }

        public frmComprobanteVenta(EmpleadosBE be)
        {
            emp = be;
            InitializeComponent();

           // txtCodVendedor.Text = be.Id.ToString();
            //txtNomVendedor.Text = be.Nombres + " " + be.Apellidos;

            dtdetalle.Columns.Add("codigo", typeof(String));
            dtdetalle.Columns.Add("nombre", typeof(String));
            dtdetalle.Columns.Add("precio", typeof(Double));
            dtdetalle.Columns.Add("cantidad", typeof(Int32));
            emp.Id = 0;

        }
        //BTransaccion objtra = new BTransaccion();

        private void Button7_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }


        private void dtgDetalleProd_CellEnter(object sender, System.EventArgs e)
        {
            actualizarmontos();
        }

        private void dtgDetalleProd_CellValueChanged(object sender, System.EventArgs e)
        {
            actualizarmontos();
        }



        private void frmFactura_Load(object sender, EventArgs e)
        {
            
            
            // Cargar Monedas
        
            //

         

            /*
            dtDetPedido.Columns.AddRange(new DataColumn[8] 
                          { new DataColumn("idproducto", typeof(int)),
                            new DataColumn("codigoproducto", typeof(string)),  
                            new DataColumn("descripcion", typeof(string)),  
                            new DataColumn("cantidad",typeof(decimal)),
                            new DataColumn("unidad",typeof(string)),
                            new DataColumn("preciounitario",typeof(decimal)),
                            new DataColumn("total",typeof(decimal)),
                            new DataColumn("observacion",typeof(string))});
            */
            //dtDetPedido.Columns.RemoveAt();


            foreach (DataGridViewColumn dc in dtgDetalleProd.Columns)
            {
                if (dc.Index.Equals(0))
                {
                    dc.ReadOnly = false;
                }
                else if ( dc.Index.Equals(0) || dc.Index.Equals(1)  || dc.Index.Equals(2) )
                {
                    dc.ReadOnly = true;
                }
                    
                }

            

        }

        private void Button15_Click(object sender, EventArgs e)
        {
           new  frmConsultarCliente(this).ShowDialog();
        }

        public string Nombress
        {
            get { return txtNombreCliente.Text; }
            set { txtNombreCliente.Text = value; }
        }     

        private void Button6_Click(object sender, EventArgs e)
        {
            new frmConsultarVendedor(this).ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
           new frmConsultarAlmacen (this).ShowDialog();
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button11_Click(object sender, EventArgs e)
        {
     //        new frmConsultarDireccione(this).ShowDialog();
                       
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (emp.Id ==0)
            {
                MessageBox.Show("Debe seleccionar un cliente para reconocer la direcciones correcta");
            }
            else
            {
                new frmConsultarDireccion(this, emp.Id).ShowDialog();
            }

          
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == false)
                txtmodalidad.Enabled = false;

            if (CheckBox3.Checked == true)
                txtmodalidad.Enabled = true ;
        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button25_Click(object sender, EventArgs e)
        {
            if (dtdetalle.Rows.Count >= 1)
                {
             
                string codigo = this.dtgDetalleProd.SelectedCells[0].Value.ToString();

                for (int i = dtdetalle.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtdetalle.Rows[i];
                    if (dr["codigo"].ToString() == codigo)
                        dr.Delete();
                }

                foreach (DataGridViewRow item in this.dtgDetalleProd.SelectedRows)
                {
                    dtgDetalleProd.Rows.RemoveAt(item.Index);
                }
           


                }

            actualizarmontos();
        }


        

        private void dtgDetalleProd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

       
        }

        private bool skipSelectionChanged = false;

        private void SelectTabWithoutFocus(TabPage tabPage)
        {
            this.skipSelectionChanged = true;

            // "this" is the form in my case, so you get the current focused control
            // (ActiveControl), backup it, and re-set it after Tab activation

            var prevFocusedControl = this.ActiveControl;
            if (this.ActiveControl != null)
            {
                this.TabControl1.SelectedTab = tabPage;
                prevFocusedControl.Focus();
            }
            this.skipSelectionChanged = false;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if  (txtCodCliente.Text == "") { MessageBox.Show("Debe seleccionar un Cliente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCodCliente.Focus(); this.Cursor = Cursors.Default; return; }
            if (txtCodVendedor.Text == "") { MessageBox.Show("Debe seleccionar un Vendedor", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error); txtCodVendedor.Focus(); this.Cursor = Cursors.Default; return; }
            if (txtDireccionEntrega.Text == "") { MessageBox.Show("Debe Ingresar una Dirección de entrega", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error); this.SelectTabWithoutFocus(this.TabControl1.TabPages[1]); ; this.Cursor = Cursors.Default; return; }
            if (dtgDetalleProd.Rows.Count == 0)
            {
                MessageBox.Show("Debe Ingresar Items", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnNuevo2.Focus();
                this.Cursor = Cursors.Default;
                return;
            }


            else
                ped = new FacturaBE();
            

                ped.ClienteId = cli.IdCliente;
            ped.FechaEmision = DateTimePicker1.Value;
            ped.FechaEntrega = DateTimePicker2.Value;
            ped.FechaPago = DateTimePicker1.Value;


            if (cli.IDTipoDocIdentidad ==1)
            {
                ped.TipoCP = 1;
            }else if (cli.IDTipoDocIdentidad == 2)
            {
                ped.TipoCP = 2;
            }

            ped.ModalidadCreditoId = cli.IDModalidadCredito;
            ped.Credito = true;
            ped.Direccion = txtDireccion.Text;
            ped.IdVendedor = int.Parse(txtCodVendedor.Text);
            ped.Observacion = "kjhjkh";
            ped.PrecioIncluyeImpuesto = true;
            ped.EstadoFacturacion = "FACTURADO";
            ped.DireccionEntrega = txtDireccionEntrega.Text;

            ped.FacturaId = 0;

            ped.ValorVenta = double.Parse(txtTotal.Text);

            ped.Subtotal = decimal.Parse(txtSubTotal.Text);
            ped.Total = decimal.Parse(txtTotal.Text);
            ped.MontoIGV = decimal.Parse(txtimpuesto.Text);
            ped.Observaciones = "GENERADO DESDE COMPROBANTE DE VENTA";
            ped.CodigoReq = 1;
            ped.CodigoSolAtencion = 1;
            ped.CodigoFormato = 0;




            //string codigo = cbMonedas.SelectedValue.ToString();
           // ped. = int.Parse(codigo);


            FacturaBL pbl = new FacturaBL();

            Boolean valor =  pbl.insertFactura(ped, dtdetalle);


            if (valor == true)
            {
                
           MessageBox.Show("Comprobante de Venta guardado satisfactoriamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.Close();

            }
            else
            {
                MessageBox.Show("Problema con el registro de Comprobante de Venta , Comunciarse con SoporteEtna@etna.com.pe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                 }

                
        }

        private void cbMonedas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCodCliente.Text == "")
            {
                MessageBox.Show("Debe Seleccionar un Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else{
                new frmConsultarProducto(this).Show();

            }}

     
        public void actualizarmontos()
        {
           
            if (dtgDetalleProd.Rows.Count>=1 )
                {

                
            double totalsinigv = 0;
            foreach(DataGridViewRow row in dtgDetalleProd.Rows)

            {
                
                Double precio = double.Parse(row.Cells[2].Value.ToString());
                int cantidad = int.Parse(row.Cells[3].Value.ToString());
                totalsinigv = (precio * cantidad)+totalsinigv;

                


            }

            txtSubTotal.Text = totalsinigv.ToString();
                    txtafecto.Text = totalsinigv.ToString();
                   double impuesto =  totalsinigv*0.18;
                    txtimpuesto.Text = impuesto.ToString();
                        txtTotal.Text = (totalsinigv + impuesto).ToString();

                }
            else{
                txtSubTotal.Text = "0.00";
                txtTotal.Text = "0.00";
                txtafecto.Text = "0.00";
                txtimpuesto .Text = "0.00";
                txtRecargo .Text = "0.00";
                txtRecargo.Text = "0.00";
            }


        }
        public void actualizarDetalle(){
           dtgDetalleProd.Rows.Clear();
            if (dtdetalle.Rows.Count >= 1){
                 foreach (DataRow row in dtdetalle.Rows)
                 {

                     dtgDetalleProd.Rows.Add(row[0].ToString(),
                         row[1].ToString(), double.Parse( row[2].ToString()),
                         int.Parse(row[3].ToString()) );

                   
                 }
            }

          
           

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Button27_Click(object sender, EventArgs e)
        {

        }

        private void dtgDetalleProd_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgDetalleProd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new frmConsultaTipoCP(this).ShowDialog();
        }


    }
}
