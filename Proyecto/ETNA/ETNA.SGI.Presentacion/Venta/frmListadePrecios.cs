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

namespace ETNA.SGI.Presentacion.Venta
{
    public partial class frmListadePrecios : DevExpress.XtraEditors.XtraForm
    {
        public frmListadePrecios()
        {
            InitializeComponent();
        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void frmListadePrecios_Load(object sender, EventArgs e)
        {
            cboCriterios.Items.Add("Descripcion");
            cboCriterios.Items.Add("Linea");
            cboCriterios.Items.Add("Codigo");

            cboCriterios.SelectedIndex = 0;

        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }
    }
}