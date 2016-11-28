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
    public partial class Frmarticulo : Form
    {
        private bool isNuevo = false;
        private bool isEditar = false;

        private static Frmarticulo _Instancia;
        public static Frmarticulo _GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new Frmarticulo();
            }
            return _Instancia;
        }

        public void setCategoria(string idcategoria, string nombre)
        {
            this.txtIdCategoria.Text = idcategoria;
            this.txtCategoria.Text = nombre;
        }

        public Frmarticulo()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre del articulo prro");
            this.ttMensaje.SetToolTip(this.pxImagen, "Selecciona imagen prro");
            this.ttMensaje.SetToolTip(this.cbIdPresentacion, "Selecciona la presentacion del articulo prro");
            this.ttMensaje.SetToolTip(this.txtCategoria, "Selecciona la categoria del articulo prro");

            this.txtIdarticulo.Visible = false;
            this.txtCategoria.ReadOnly = true;
            this.LlenarComboPresentacion();

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
            this.txtNombre.Text = string.Empty;
            this.txtCodigo.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtIdarticulo.Text = string.Empty;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.ezh2hh;
        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtIdarticulo.ReadOnly = !valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.cbIdPresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnLimpiar.Enabled = valor;

            this.txtIdarticulo.ReadOnly = !valor;
            //Mequede QUI!!!

        }

        //Habilitar los botones
        private void Botones()
        {
            if (this.isNuevo || this.isEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        //Ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;
        }
        //Metodo mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo buscar
        private void BuscarNombre()
        {
            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void LlenarComboPresentacion()
        {
            cbIdPresentacion.DataSource = NPresentacion.Mostrar();
            cbIdPresentacion.ValueMember = "idpresentacion";
            cbIdPresentacion.DisplayMember = "nombre";


        }





        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.ezh2hh;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void Frmarticulo_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.isNuevo = true;
            this.isEditar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || txtIdCategoria.Text == string.Empty || txtCodigo.Text == string.Empty)
                {
                    MensajeError("Falta ingresar datos, seran remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese nombre");
                    errorIcono.SetError(txtCodigo, "Ingrese codigo");
                    errorIcono.SetError(txtIdCategoria, "Ingrese categoria");

                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagen = ms.GetBuffer();

                    if (this.isNuevo)
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim(), imagen, Convert.ToInt32(this.txtIdCategoria.Text),
                            Convert.ToInt32(this.cbIdPresentacion.SelectedValue)
                            );

                    }
                    else
                    {
                        rpta = NArticulo.Editar(Convert.ToInt32(this.txtIdarticulo.Text), this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim(), imagen, Convert.ToInt32(this.txtIdCategoria.Text),
                            Convert.ToInt32(this.cbIdPresentacion.SelectedValue)
                            );
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.isNuevo)
                        {
                            this.MensajeOk("Se inserto de forma chida el registro");
                        }
                        else
                        {
                            this.MensajeOk("Se actualizo de forma chida el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);

                    }
                    this.isNuevo = false;
                    this.isEditar = false;
                    this.Botones();
                    this.limpiar();
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdarticulo.Text.Equals(""))
            {
                this.isEditar = true;
                this.Botones();
                this.Habilitar(true);

            }
            else
            {
                this.MensajeError("Debes seleccionar primero el registro para modificar prro ;V");

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.isNuevo = false;
            this.isEditar = false;
            this.Botones();
            this.limpiar();
            this.Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            //txtIdarticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            //this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            //this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            //this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            //byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);
            //this.pxImagen.Image = Image.FromStream(ms);
            //this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            //this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            //this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            //this.cbIdPresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);

            //this.tabControl1.SelectedIndex = 1;
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
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
                            Rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));

                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOk("Ya lo eliminaste prro ;v");
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }

                        }
                    }
                    this.Mostrar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            FrmVistaCategoria_Articulo form = new FrmVistaCategoria_Articulo();
            form.ShowDialog();
        }

        private void cbIdPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbIdPresentacion.DataSource = NPresentacion.Mostrar();
            cbIdPresentacion.ValueMember = "idpresentacion";
            cbIdPresentacion.DisplayMember = "nombre";
        }

        private void dataListado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdarticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["articulo"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);
            this.pxImagen.Image = Image.FromStream(ms);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

           
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.cbIdPresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void dataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.txtIdarticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            //this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            //this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            //this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            //byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);
            //this.pxImagen.Image = Image.FromStream(ms);
            //this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            //this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            //this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            //this.cbIdPresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);

            //this.tabControl1.SelectedIndex = 1;
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReporteDeArticulos frm = new frmReporteDeArticulos();
            frm.ShowDialog();
        }
    }
}
