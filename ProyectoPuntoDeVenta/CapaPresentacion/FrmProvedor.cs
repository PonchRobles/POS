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
    public partial class FrmProvedor : Form
    {

        private bool IsNuevo = false;
        private bool IsEditar = false;

        public FrmProvedor()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtRazonsocial, "Ingresa la razon social prro");
            ttMensaje.SetToolTip(txtNum_Documento, "Ingresa el numero de documento prro");
            ttMensaje.SetToolTip(txtDireccion, "Ingresa la direccion prro");
        }

        //Mensaje de confirmacion

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        //Mostrar mensaje de error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        //Limpiar todos los controles del formulario
        private void limpiar()
        {
            txtRazonsocial.Text = string.Empty;
            txtNum_Documento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtUrl.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtIdproveedor.Text = string.Empty;
        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            txtRazonsocial.ReadOnly = !valor;
            txtDireccion.ReadOnly = !valor;
            cbSectorComercial.Enabled = valor;
            cbTipo_Documento.Enabled = valor;
            txtNum_Documento.ReadOnly = !valor;
            txtTelefono.ReadOnly = !valor;
            txtUrl.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            txtIdproveedor.ReadOnly = !valor;

        }

        //Habilitar los botones
        private void Botones()
        {
            if (IsNuevo || IsEditar)
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;

            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }

        //Ocultar columnas
        private void OcultarColumnas()
        {
            dataListado.Columns[0].Visible = false;
            dataListado.Columns[1].Visible = false;

        }
        //Metodo mostrar
        private void Mostrar()
        {
            dataListado.DataSource = NProveedor.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo buscarrazonsocial
        private void BuscarRazon_Social()
        {
            dataListado.DataSource = NProveedor.BuscarRazon_Social(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo buscar por numero de documento
        private void Buscarnumero_documento()
        {
            dataListado.DataSource = NProveedor.BuscarNum_Documento(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void FrmProvedor_Load(object sender, EventArgs e)
        {
            Top = 0;
            Left = 0;
            Mostrar();
            Habilitar(false);
            Botones();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Si los quieres eliminar prro ;V?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NProveedor.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                MensajeOk("Ya lo eliminaste prro ;v");
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            IsNuevo = true;
            IsEditar = false;
            Botones();
            limpiar();
            Habilitar(true);
            txtRazonsocial.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (txtRazonsocial.Text == string.Empty || txtNum_Documento.Text == string.Empty || txtDireccion.Text == string.Empty)
                {
                    MensajeError("Falta ingresar datos, seran remarcados");
                    errorIcono.SetError(txtRazonsocial, "Ingrese valor");
                    errorIcono.SetError(txtNum_Documento, "Ingrese valor");
                    errorIcono.SetError(txtDireccion, "Ingrese valor");
                }
                else
                {
                    if (IsNuevo)
                    {
                        rpta = NProveedor.Insertar(txtRazonsocial.Text.Trim().ToUpper(),
                            cbSectorComercial.Text, cbTipo_Documento.Text, txtNum_Documento.Text, txtDireccion.Text, txtDireccion.Text, txtEmail.Text, txtUrl.Text);
                    }
                    else
                    {
                        rpta = NProveedor.Editar(Convert.ToInt32(txtIdproveedor.Text), txtRazonsocial.Text.Trim().ToUpper(),
                            cbSectorComercial.Text, cbTipo_Documento.Text, txtNum_Documento.Text, txtDireccion.Text, txtDireccion.Text, txtEmail.Text, txtUrl.Text);
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (IsNuevo)
                        {
                            MensajeOk("Se inserto de forma chida el registro");
                        }
                        else
                        {
                            MensajeOk("Se actualizo de forma chida el registro");
                        }
                    }
                    else
                    {
                        MensajeError(rpta);

                    }
                    IsNuevo = false;
                    IsEditar = false;
                    Botones();
                    limpiar();
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdproveedor.Text.Equals(""))
            {
                IsEditar = true;
                Botones();
                Habilitar(true);

            }
            else
            {
                MensajeError("Debe seleccionar primero el registro para modificar");


            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            IsNuevo = false;
            IsEditar = false;
            Botones();
            limpiar();
            txtIdproveedor.Text = string.Empty;
            //Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdproveedor.Text = Convert.ToString(dataListado.CurrentRow.Cells["idproveedor"].Value);
            txtRazonsocial.Text = Convert.ToString(dataListado.CurrentRow.Cells["razon_social"].Value);
            cbSectorComercial.Text = Convert.ToString(dataListado.CurrentRow.Cells["sector_comerciall"].Value);
            cbTipo_Documento.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_documento"].Value);
            txtNum_Documento.Text = Convert.ToString(dataListado.CurrentRow.Cells["num_documento"].Value);
            txtDireccion.Text = Convert.ToString(dataListado.CurrentRow.Cells["direccion"].Value);
            txtTelefono.Text = Convert.ToString(dataListado.CurrentRow.Cells["telefono"].Value);
            txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
            txtUrl.Text = Convert.ToString(dataListado.CurrentRow.Cells["url"].Value);
            tabControl1.SelectedIndex = 1;
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            //txtIdproveedor.Text = Convert.ToString(dataListado.CurrentRow.Cells["idproveedor"].Value);
            //txtRazonsocial.Text = Convert.ToString(dataListado.CurrentRow.Cells["razon_social"].Value);
            //cbSectorComercial.Text = Convert.ToString(dataListado.CurrentRow.Cells["sector_comerciall"].Value);
            //cbTipo_Documento.Text = Convert.ToString(dataListado.CurrentRow.Cells["tipo_documento"].Value);
            //txtNum_Documento.Text = Convert.ToString(dataListado.CurrentRow.Cells["num_documento"].Value);
            //txtDireccion.Text = Convert.ToString(dataListado.CurrentRow.Cells["direccion"].Value);
            //txtTelefono.Text = Convert.ToString(dataListado.CurrentRow.Cells["telefono"].Value);
            //txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
            //txtUrl.Text = Convert.ToString(dataListado.CurrentRow.Cells["url"].Value);
        }

        private void txtRazonsocial_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
