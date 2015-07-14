using System;
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

namespace ETNA.SGI.Presentacion.Venta
{


    public partial class frmConsultaComprobante : DevExpress.XtraEditors.XtraForm
    {

        FacturaBL pbl = new FacturaBL();

        public frmConsultaComprobante()
        {
            InitializeComponent();
        }

        public EmpleadosBE emp = new EmpleadosBE();

        public frmConsultaComprobante(EmpleadosBE be)
        {
            InitializeComponent();
            emp = be;
        }

        private void frmConsultaComprobante_Load(object sender, EventArgs e)
        {
            actualizar();
        dataGridView1.AllowUserToAddRows = false;}

        private void button1_Click(object sender, EventArgs e)
        {
            actualizar2();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmComprobanteVenta(emp).ShowDialog();
           actualizar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea Eliminar el pedido?", "Confirmación", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                if (dataGridView1.Rows.Count >= 1)
                {

                    int codigo = int.Parse(this.dataGridView1.SelectedCells[0].Value.ToString());


                    Boolean result = pbl.deletefactura(codigo);
                    if (result == true)
                    {
                        MessageBox.Show("Pedido Eliminado exitosamente");
                        actualizar();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un problema inesperado , comunicarse con el Administrador del sistemas"); 
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
               
            }
        }


        void actualizar()
        {
            int codigotcp=0;

            if (rbtTodos.Checked)
            {
                codigotcp = 0;

            }
            else if (rbtBole.Checked)
            {

                codigotcp = 1;
            }
            else if (rbtFactu.Checked)
            {
                codigotcp = 2;
            }

            dataGridView1.DataSource = dataGridView1.DataSource = pbl.getFacturas("",codigotcp);
        }

        void actualizar2()
        {
            int codigotcp = 0;

            if (rbtTodos.Checked)
            {
                codigotcp = 0;

            }
            else if (rbtBole.Checked)
            {

                codigotcp = 1;
            }
            else if (rbtFactu.Checked)
            {
                codigotcp = 2;
            }

            dataGridView1.DataSource = dataGridView1.DataSource = pbl.getFacturas(textBox1.Text, codigotcp);
        }
        private void button4_Click(object sender, EventArgs e)
        {

              if (dataGridView1.Rows.Count >= 1)
                {

                    int codigo = int.Parse(this.dataGridView1.SelectedCells[0].Value.ToString());


            new frmUpComprobante(codigo).ShowDialog();
               }}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                 if (rbtTodos.Checked)
                 {
                     rbtBole.Checked = false;
                     rbtFactu.Checked = false;

                 }else if (rbtBole.Checked){

                     rbtTodos.Checked = false;
                     rbtFactu.Checked = false;

                 }else if (rbtFactu.Checked){
                     rbtTodos.Checked = false;
                     rbtBole.Checked = false;
                 }

            actualizar();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTodos.Checked)
            {
                rbtBole.Checked = false;
                rbtFactu.Checked = false;

            }
            else if (rbtBole.Checked)
            {

                rbtTodos.Checked = false;
                rbtFactu.Checked = false;

            }
            else if (rbtFactu.Checked)
            {
                rbtTodos.Checked = false;
                rbtBole.Checked = false;
            }

            actualizar();
        }
    }
}