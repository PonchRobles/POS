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
    public partial class FrmReporteFactura : Form
    {
        private int _Idventa;
        public int Idventa {
            get { return _Idventa; }
            set { _Idventa = value; }
        }
        public FrmReporteFactura()
        {
            InitializeComponent();
        }

        private void FrmReporteFactura_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DsPrincipal.spreporte_factura' table. You can move, or remove it, as needed.
            try
            {

                this.spreporte_facturaTableAdapter.Fill(this.DsPrincipal.spreporte_factura, Idventa);
                //  spreporte_facturaTableAdapter.Fill(DsPrincipal.spreporte_factura,Idventa);
                this.reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }
            catch(Exception ex) {
                this.reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();

            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
