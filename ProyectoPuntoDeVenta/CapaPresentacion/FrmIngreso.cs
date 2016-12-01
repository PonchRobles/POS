using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmIngreso : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        public int Idtrabajador;

        private DataTable dtDetalle;

        private decimal totalPagado = 0;

        private static FrmIngreso _instancia;

        //Creamos una instancia para poder utilizar los
        //Objetos del formulario
        public static FrmIngreso GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FrmIngreso();
            }
            return _instancia;
        }
        //Creamos un método para enviar los valores recibidos
        //del proveedor
        public void setProveedor(string idproveedor, string nombre)
        {
            txtIdProveedor.Text = idproveedor;
            txtProveedor.Text = nombre;
        }

        //Creamos un método para enviar los valores recibidos
        //del artículo
        public void setArticulo(string idarticulo, string nombre)
        {
            txtIdArticulo.Text = idarticulo;
            txtArticulo.Text = nombre;
        }
        public FrmIngreso()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtProveedor, "Seleccione un Proveedor");
            ttMensaje.SetToolTip(txtSerie, "Ingrese la serie del comprobante");
            ttMensaje.SetToolTip(txtCoRelativo, "Ingrese el número del comprobante");
            ttMensaje.SetToolTip(txtStock, "Ingrese la cantidad de compra");
            ttMensaje.SetToolTip(txtArticulo, "Seleccione el artículo");
            txtIdProveedor.Visible = false;
            txtProveedor.ReadOnly = true;
            txtIdArticulo.Visible = false;
            txtArticulo.ReadOnly = true;
        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            //Para ubicar al formulario en la parte superior del contenedor
            Top = 0;
            Left = 0;
            //Mostrar
            Mostrar();
            //Deshabilita los controles
            Habilitar(false);
            //Establece los botones
            Botones();
            crearTabla();
        }

        private void FrmIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            FrmVistaProveedor_Ingreso vista = new FrmVistaProveedor_Ingreso();
            vista.ShowDialog();
        }

        private void btnArticulo_Click(object sender, EventArgs e)
        {
            FrmVistaArticulo_Ingreso vista = new FrmVistaArticulo_Ingreso();
            vista.ShowDialog();
        }
        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpiar todos los controles del formulario
        private void Limpiar()
        {
            txtIdIngreso.Text = string.Empty;
            txtIdProveedor.Text = string.Empty;
            txtProveedor.Text = string.Empty;
            txtSerie.Text = string.Empty;
            txtCoRelativo.Text = string.Empty;
            txtIGV.Text = "0,18";
            lblTotalPagado.Text = "0,0";
            crearTabla();

        }

        private void limpiarDetalle()
        {
            txtIdArticulo.Text = string.Empty;
            txtArticulo.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtPreciodeCompra.Text = string.Empty;
            txtPrecioDeVenta.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            txtIdIngreso.ReadOnly = !valor;
            txtSerie.ReadOnly = !valor;
            txtCoRelativo.ReadOnly = !valor;
            txtIGV.Enabled = valor;
            dtFecha.Enabled = valor;
            cbComprobante.Enabled = valor;
            txtStock.ReadOnly = !valor;
            txtPreciodeCompra.ReadOnly = !valor;
            txtPrecioDeVenta.ReadOnly = !valor;
            dtFechaProduccion.Enabled = valor;
            dtFechaVencimiento.Enabled = valor;
            btnAgregar.Enabled = valor;
            btnQuitar.Enabled = valor;
            btnBuscarProveedor.Enabled = valor;
            btnBuscar.Enabled = valor;
        }

        //Habilitar los botones
        private void Botones()
        {
            if (IsNuevo || IsEditar) //Alt + 124
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
             
            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }

        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            dataListado.Columns[0].Visible = false;
            dataListado.Columns[1].Visible = false;
        }

        //Método Mostrar
        private void Mostrar()
        {
            dataListado.DataSource = NIngreso.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        //Método BuscarFecha
        private void BuscarFechas()
        {
            dataListado.DataSource = NIngreso.BuscarFechas(dtFecha_Inicio.Value.ToString("dd/MM/yyyy"), dtFecha_Fin.Value.ToString("dd/MM/yyyy"));
            OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);

        }

        //Método BuscarDetalles
        private void MostrarDetalles()
        {
            dataListadoDetalle.DataSource = NIngreso.MostrarDetalle(txtIdIngreso.Text);
            //OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
            //datalistadoDetalle.AutoGenerateColumns = false;
        }

        //Crea la tabla de Detalle 
        private void crearTabla()
        {
            //Crea la tabla con el nombre de Detalle
            dtDetalle = new DataTable("Detalle");
            //Agrega las columnas que tendra la tabla
            dtDetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            dtDetalle.Columns.Add("precio_compra", System.Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("precio_venta", System.Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("stock_inicial", System.Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("fecha_produccion", System.Type.GetType("System.DateTime"));
            dtDetalle.Columns.Add("fecha_vencimiento", System.Type.GetType("System.DateTime"));
            dtDetalle.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
            //Relacionamos nuestro datagridview con nuestro datatable
            dataListadoDetalle.DataSource = dtDetalle;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Si los quieres anular prro ;V?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NIngreso.Anular(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                MensajeOk("Ya lo anular prro ;v");
                            }
                            else
                            {
                                MensajeError(Rpta);
                            }

                        }
                    }
                    Mostrar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarFechas();
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                dataListado.Columns[0].Visible = true;
            }
            else
            {
                dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            IsNuevo = true;

            Botones();
            Limpiar();
            Habilitar(true);
            txtSerie.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IsNuevo = false;

            Botones();
            Limpiar();
            Habilitar(false);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                //La variable que almacena si se inserto 
                //o se modifico la tabla
                string Rpta = "";
                if (txtIdProveedor.Text == string.Empty || txtSerie.Text == string.Empty || txtCoRelativo.Text == string.Empty || txtIGV.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtProveedor, "Seleccione un Proveedor");
                    errorIcono.SetError(txtSerie, "Ingrese la serie del comprobante");
                    errorIcono.SetError(txtCoRelativo, "Ingrese el número del comprobante");
                    errorIcono.SetError(txtIGV, "Ingrese el porcentaje de IGV");
                }
                else
                {
                    if (IsNuevo)
                    {
                        //Vamos a insertar un Ingreso 
                        Rpta = NIngreso.Insertar(Idtrabajador, Convert.ToInt32(txtIdProveedor.Text),
                        dtFecha.Value, cbComprobante.Text,
                        txtSerie.Text, txtCoRelativo.Text,
                        Convert.ToDecimal(txtIGV.Text), "EMITIDO", dtDetalle);

                    }

                    //Si la respuesta fue OK, fue porque se modifico 
                    //o inserto el Ingreso
                    //de forma correcta
                    if (Rpta.Equals("OK"))
                    {

                        MensajeOk("Se insertó de forma correcta el registro");


                    }
                    else
                    {
                        //Mostramos el mensaje de error
                        MensajeError(Rpta);
                    }
                    IsNuevo = false;
                    Botones();
                    Limpiar();
                    limpiarDetalle();
                    Mostrar();

                }
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtIdArticulo.Text == string.Empty || txtStock.Text == string.Empty || txtPreciodeCompra.Text == string.Empty || txtPrecioDeVenta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtArticulo, "Seleccione un Artículo");
                    errorIcono.SetError(txtStock, "Ingrese el stock inicial");
                    errorIcono.SetError(txtPreciodeCompra, "Ingrese el precio de Compra");
                    errorIcono.SetError(txtPrecioDeVenta, "Ingrese el precio de Venta");
                }
                else
                {
                    //Variable que va a indicar si podemos registrar el detalle
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idarticulo"]) == Convert.ToInt32(txtIdArticulo.Text))
                        {
                            registrar = false;
                            MensajeError("Ya se encuentra el artículo en el detalle");
                        }
                    }
                    //Si podemos registrar el producto en el detalle
                    if (registrar)
                    {
                        //Calculamos el sub total del detalle sin descuento
                        decimal subTotal = Convert.ToDecimal(txtPreciodeCompra.Text) * Convert.ToDecimal(txtStock.Text);
                        totalPagado = totalPagado + subTotal;
                        lblTotalPagado.Text = totalPagado.ToString("#0.00#");
                        //Agregamos al fila a nuestro datatable
                        DataRow row = dtDetalle.NewRow();
                        row["idarticulo"] = Convert.ToInt32(txtIdArticulo.Text);
                        row["articulo"] = txtArticulo.Text;
                        row["precio_compra"] = Convert.ToDecimal(txtPreciodeCompra.Text);
                        row["precio_venta"] = Convert.ToDecimal(txtPrecioDeVenta.Text);
                        row["stock_inicial"] = Convert.ToInt32(txtStock.Text);
                        row["fecha_produccion"] = dtFechaProduccion.Value;
                        row["fecha_vencimiento"] = dtFechaVencimiento.Value;
                        row["subTotal"] = subTotal;
                        dtDetalle.Rows.Add(row);
                        limpiarDetalle();
                    }
                }


            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                //Indice dila actualmente seleccionado y que vamos a eliminar
                int indiceFila = dataListadoDetalle.CurrentCell.RowIndex;
                //Fila que vamos a eliminar
                DataRow row = dtDetalle.Rows[indiceFila];
                //Disminuimos el total a pagar
                totalPagado = totalPagado - Convert.ToDecimal(row["subTotal"].ToString());
                lblTotalPagado.Text = totalPagado.ToString("#0.00#");
                //Removemos la fila
                dtDetalle.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MensajeError("No hay fila para remover");
            }
        }

        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdIngreso.Text = Convert.ToString(dataListado.CurrentRow.Cells["idingreso"].Value);
            txtProveedor.Text = Convert.ToString(dataListado.CurrentRow.Cells["proveedor"].Value);
            dtFecha.Value = Convert.ToDateTime(dataListado.CurrentRow.Cells["fecha"].Value);
            cbComprobante.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            txtSerie.Text = Convert.ToString(dataListado.CurrentRow.Cells["serie"].Value);
            txtCoRelativo.Text = Convert.ToString(dataListado.CurrentRow.Cells["correlativo"].Value);
            lblTotalPagado.Text = Convert.ToString(dataListado.CurrentRow.Cells["Total"].Value);
            MostrarDetalles();
            tabControl1.SelectedIndex = 1;
        }
    }
}

