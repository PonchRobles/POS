using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmReporteDeArticulos : Form
    {
        public frmReporteDeArticulos()
        {
            InitializeComponent();
        }

        private void frmReporteDeArticulos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DsPrincipal.spmostrar_articulo' table. You can move, or remove it, as needed.
            this.spmostrar_articuloTableAdapter.Fill(this.DsPrincipal.spmostrar_articulo);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
