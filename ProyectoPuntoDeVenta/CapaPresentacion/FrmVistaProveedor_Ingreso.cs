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
    public partial class FrmVistaProveedor_Ingreso : Form
    {
        public FrmVistaProveedor_Ingreso()
        {
            InitializeComponent();
        }

        private void FrmVistaProveedor_Ingreso_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
        //Ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;

        }
        //Metodo mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NProveedor.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo buscarrazonsocial
        private void BuscarRazon_Social()
        {
            this.dataListado.DataSource = NProveedor.BuscarRazon_Social(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo buscar por numero de documento
        private void Buscarnumero_documento()
        {
            this.dataListado.DataSource = NProveedor.BuscarNum_Documento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbForma_Busqueda.Text.Equals("Razon Social"))
            {
                BuscarRazon_Social();


            }
            else if (cbForma_Busqueda.Text.Equals("Documento"))
            {

                Buscarnumero_documento();

            }
        }

        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmIngreso form = FrmIngreso.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(dataListado.CurrentRow.Cells["idproveedor"].Value);
            par2 = Convert.ToString(dataListado.CurrentRow.Cells["razon_social"].Value);
            form.setProveedor(par1, par2);
            Hide();
        }
    }
}
