using CapaNegocio;
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
    public partial class FrmVistaArticulo_Venta : Form
    {
        public FrmVistaArticulo_Venta()
        {
            InitializeComponent();
        }

        private void FrmVistaArticulo_Venta_Load(object sender, EventArgs e)
        {

        }
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        }
       
        private void MostrarArticuloPorNombre()
        {
            this.dataListado.DataSource = NVentas.MostrarArticulo_Venta_Nombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void MostrarArticuloVentaCodigo()
        {
            this.dataListado.DataSource = NCliente.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbForma_Busqueda.Text.Equals("Codigo"))
            {
                MostrarArticuloVentaCodigo();
            }
            else if (cbForma_Busqueda.Text.Equals("Nombre"))
            {
                MostrarArticuloPorNombre();
            }
        }

        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmVenta form = FrmVenta.GetInstancia();

            string par, par2;
            decimal par3, par4;
            int par5;
            DateTime par6;
            par = Convert.ToString(dataListado.CurrentRow.Cells["iddetalle_ingreso"].Value);
            par2 = Convert.ToString(dataListado.CurrentRow.Cells["nombre"].Value);
            par3 = Convert.ToDecimal(dataListado.CurrentRow.Cells["precio_compra"].Value);
            par4 = Convert.ToDecimal(dataListado.CurrentRow.Cells["precio_venta"].Value);
            par5 = Convert.ToInt32(dataListado.CurrentRow.Cells["stock_actual"].Value);
            par6 = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha_vencimiento"].Value);
            form.setArticulo(par, par2, par3, par4, par5,par6);
            Hide();
        }
    }
}
