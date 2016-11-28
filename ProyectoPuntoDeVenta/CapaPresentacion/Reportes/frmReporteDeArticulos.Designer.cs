namespace CapaPresentacion
{
    partial class frmReporteDeArticulos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DsPrincipal = new CapaPresentacion.DsPrincipal();
            this.spmostrar_articuloBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spmostrar_articuloTableAdapter = new CapaPresentacion.DsPrincipalTableAdapters.spmostrar_articuloTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DsPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spmostrar_articuloBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spmostrar_articuloBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Reportes.rptArticulos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(762, 511);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // DsPrincipal
            // 
            this.DsPrincipal.DataSetName = "DsPrincipal";
            this.DsPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // spmostrar_articuloBindingSource
            // 
            this.spmostrar_articuloBindingSource.DataMember = "spmostrar_articulo";
            this.spmostrar_articuloBindingSource.DataSource = this.DsPrincipal;
            // 
            // spmostrar_articuloTableAdapter
            // 
            this.spmostrar_articuloTableAdapter.ClearBeforeFill = true;
            // 
            // frmReporteDeArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 511);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmReporteDeArticulos";
            this.Text = "Reporte de articulos";
            this.Load += new System.EventHandler(this.frmReporteDeArticulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DsPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spmostrar_articuloBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DsPrincipal DsPrincipal;
        private System.Windows.Forms.BindingSource spmostrar_articuloBindingSource;
        private DsPrincipalTableAdapters.spmostrar_articuloTableAdapter spmostrar_articuloTableAdapter;
    }
}