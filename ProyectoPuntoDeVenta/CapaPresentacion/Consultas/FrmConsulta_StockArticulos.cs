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

namespace CapaPresentacion.Consultas
{
    public partial class FrmConsulta_StockArticulos : Form
    {
        public FrmConsulta_StockArticulos()
        {
            InitializeComponent();
        }
        //Ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
           
        }
        //Metodo mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.StockArticulos();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void FrmConsulta_StockArticulos_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
    }
}
