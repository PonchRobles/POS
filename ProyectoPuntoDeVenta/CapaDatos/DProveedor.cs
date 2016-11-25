using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace CapaDatos
{
    public class DProveedor
    {

        //Variables
        private int  _IdProveedor;
        private string _Razon_Social;
        private string _Sector_Comercia;
        private string _Tipo_de_Documento;
        private string _Num_Documento;
        private string _Direccion;
        private string _Telefono;
        private string _Email;
        private string _Url;
        private string _TextoBuscar;

        //propiedades
        public int IdProveedor
        {
            get
            {
                return _IdProveedor;
            }

            set
            {
                _IdProveedor = value;
            }
        }

        public string Razon_Social
        {
            get
            {
                return _Razon_Social;
            }

            set
            {
                _Razon_Social = value;
            }
        }

        public string Sector_Comercia
        {
            get
            {
                return _Sector_Comercia;
            }

            set
            {
                _Sector_Comercia = value;
            }
        }

        public string Tipo_de_Documento
        {
            get
            {
                return _Tipo_de_Documento;
            }

            set
            {
                _Tipo_de_Documento = value;
            }
        }

        public string Num_Documento
        {
            get
            {
                return _Num_Documento;
            }

            set
            {
                _Num_Documento = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }

            set
            {
                _Email = value;
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }

            set
            {
                _Url = value;
            }
        }

        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }

        public DProveedor() { }
        public DProveedor( int idproveedor, string razon_social,string sector_comercial,string tipo_documento,string num_documento, string direccion, string telefono, string email,string url, string textobuscar)
        {
            this.IdProveedor = idproveedor;
            _Razon_Social = razon_social;
            //Variables

            _Sector_Comercia = sector_comercial;
         _Tipo_de_Documento = tipo_documento;
         _Num_Documento = num_documento;
         _Direccion = direccion;
         _Telefono = telefono;
         _Email = email;
         _Url=url;
         _TextoBuscar = textobuscar;
    }
        //Metodos
        //Metodo Insertar
        public string Insertar(DProveedor Proveedor)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsertar_proveedor";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdeProveedor = new SqlParameter();
                ParIdeProveedor.ParameterName = "@idproveedor";
                ParIdeProveedor.SqlDbType = SqlDbType.Int;
                ParIdeProveedor.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdeProveedor);

                SqlParameter ParRazon_Social = new SqlParameter();
                ParRazon_Social.ParameterName = "@razon_social";
                ParRazon_Social.SqlDbType = SqlDbType.VarChar;
                ParRazon_Social.Size = 50;
                ParRazon_Social.Value = Proveedor._Razon_Social;
                SqlCmd.Parameters.Add(ParRazon_Social);

                SqlParameter ParSector_Comercial = new SqlParameter();
                ParSector_Comercial.ParameterName = "@sector_comercial";
                ParSector_Comercial.SqlDbType = SqlDbType.VarChar;
                ParSector_Comercial.Size = 50;
                ParSector_Comercial.Value = Proveedor._Sector_Comercia;
                SqlCmd.Parameters.Add(ParSector_Comercial);

                SqlParameter ParTipo_Documento = new SqlParameter();
                ParTipo_Documento.ParameterName = "@tipo_documento";
                ParTipo_Documento.SqlDbType = SqlDbType.VarChar;
                ParTipo_Documento.Size = 20;
                ParTipo_Documento.Value = Proveedor._Tipo_de_Documento;
                SqlCmd.Parameters.Add(ParSector_Comercial);

                SqlParameter ParNum_Documento = new SqlParameter();
                ParNum_Documento.ParameterName = "@num_documento";
                ParNum_Documento.SqlDbType = SqlDbType.VarChar;
                ParNum_Documento.Size = 20;
                ParNum_Documento.Value = Proveedor._Num_Documento;
                SqlCmd.Parameters.Add(ParSector_Comercial);


                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = 20;
                ParDireccion.Value = Proveedor._Direccion;
                SqlCmd.Parameters.Add(ParDireccion);


                SqlParameter ParTelefono = new SqlParameter();
                ParTelefono.ParameterName = "@telefono";
                ParTelefono.SqlDbType = SqlDbType.VarChar;
                ParTelefono.Size = 20;
                ParTelefono.Value = Proveedor._Telefono;
                SqlCmd.Parameters.Add(ParTelefono);

                SqlParameter ParEmail = new SqlParameter();
                ParEmail.ParameterName = "@email";
                ParEmail.SqlDbType = SqlDbType.VarChar;
                ParEmail.Size = 50;
                ParEmail.Value = Proveedor._Email;
                SqlCmd.Parameters.Add(ParEmail);

                SqlParameter ParUrl = new SqlParameter();
                ParUrl.ParameterName = "@url";
                ParUrl.SqlDbType = SqlDbType.VarChar;
                ParUrl.Size = 50;
                ParUrl.Value = Proveedor._Url;  
                SqlCmd.Parameters.Add(ParUrl);


                //Ejecutar comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";

            }
            catch (Exception ex) { rpta = ex.Message; }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }

        //Metodo Editar
        public string Editar(DProveedor Proveedor)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_proveedor";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdprovedor = new SqlParameter();
                ParIdprovedor.ParameterName = "@idproveedor";
                ParIdprovedor.SqlDbType = SqlDbType.Int;
                ParIdprovedor.Value = Proveedor.IdProveedor;
                SqlCmd.Parameters.Add(ParIdprovedor);

                SqlParameter ParRazon_Social = new SqlParameter();
                ParRazon_Social.ParameterName = "@razon_social";
                ParRazon_Social.SqlDbType = SqlDbType.VarChar;
                ParRazon_Social.Size = 50;
                ParRazon_Social.Value = Proveedor._Razon_Social;
                SqlCmd.Parameters.Add(ParRazon_Social);

                SqlParameter ParSector_Comercial = new SqlParameter();
                ParSector_Comercial.ParameterName = "@sector_comercial";
                ParSector_Comercial.SqlDbType = SqlDbType.VarChar;
                ParSector_Comercial.Size = 50;
                ParSector_Comercial.Value = Proveedor._Sector_Comercia;
                SqlCmd.Parameters.Add(ParSector_Comercial);

                SqlParameter ParTipo_Documento = new SqlParameter();
                ParTipo_Documento.ParameterName = "@tipo_documento";
                ParTipo_Documento.SqlDbType = SqlDbType.VarChar;
                ParTipo_Documento.Size = 20;
                ParTipo_Documento.Value = Proveedor._Tipo_de_Documento;
                SqlCmd.Parameters.Add(ParSector_Comercial);

                SqlParameter ParNum_Documento = new SqlParameter();
                ParNum_Documento.ParameterName = "@num_documento";
                ParNum_Documento.SqlDbType = SqlDbType.VarChar;
                ParNum_Documento.Size = 20;
                ParNum_Documento.Value = Proveedor._Num_Documento;
                SqlCmd.Parameters.Add(ParSector_Comercial);


                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = 20;
                ParDireccion.Value = Proveedor._Direccion;
                SqlCmd.Parameters.Add(ParDireccion);


                SqlParameter ParTelefono = new SqlParameter();
                ParTelefono.ParameterName = "@telefono";
                ParTelefono.SqlDbType = SqlDbType.VarChar;
                ParTelefono.Size = 20;
                ParTelefono.Value = Proveedor._Telefono;
                SqlCmd.Parameters.Add(ParTelefono);

                SqlParameter ParEmail = new SqlParameter();
                ParEmail.ParameterName = "@email";
                ParEmail.SqlDbType = SqlDbType.VarChar;
                ParEmail.Size = 50;
                ParEmail.Value = Proveedor._Email;
                SqlCmd.Parameters.Add(ParEmail);

                SqlParameter ParUrl = new SqlParameter();
                ParUrl.ParameterName = "@url";
                ParUrl.SqlDbType = SqlDbType.VarChar;
                ParUrl.Size = 50;
                ParUrl.Value = Proveedor._Url;
                SqlCmd.Parameters.Add(ParUrl);
                //Ejecutar comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";

            }
            catch (Exception ex) { rpta = ex.Message; }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }

        //Metodo Elimicar

        public string Eliminar(DProveedor Proveedor)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                //Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_proveedor";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdproveedor = new SqlParameter();
                ParIdproveedor.ParameterName = "@idcategoria";
                ParIdproveedor.SqlDbType = SqlDbType.Int;
                ParIdproveedor.Value = Proveedor.IdProveedor;
                SqlCmd.Parameters.Add(ParIdproveedor);

                //SqlParameter ParNombre = new SqlParameter();
                //ParNombre.ParameterName = "@nombre";
                //ParNombre.SqlDbType = SqlDbType.VarChar;
                //ParNombre.Size = 50;
                //ParNombre.Value = Categoria.Nombre;
                //SqlCmd.Parameters.Add(ParNombre);

                //SqlParameter ParDescripcion = new SqlParameter();
                //ParDescripcion.ParameterName = "@descripcion";
                //ParDescripcion.SqlDbType = SqlDbType.VarChar;
                //ParDescripcion.Size = 256;
                //ParDescripcion.Value = Categoria.Descripcion;
                //SqlCmd.Parameters.Add(ParDescripcion);
                ////Ejecutar comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se elimino el registro";

            }
            catch (Exception ex) { rpta = ex.Message; }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }

        //Metodo Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("proveedor");
            SqlConnection SqlCOn = new SqlConnection();
            try
            {
                SqlCOn.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCOn;
                SqlCmd.CommandText = "spmostrar_proveedor";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex) { DtResultado = null; }
            return DtResultado;

        }
        //Metodo BuscarNombre
        public DataTable BuscarRazon_Social(DProveedor Proveedor)
        {
            DataTable DtResultado = new DataTable("proveedor");
            SqlConnection SqlCOn = new SqlConnection();
            try
            {
                SqlCOn.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCOn;
                SqlCmd.CommandText = "spbuscar_proveedor_razon_social";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Proveedor.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex) { DtResultado = null; }
            return DtResultado;
        }

        public DataTable BuscarNum_Documento(DProveedor Proveedor)
        {
            DataTable DtResultado = new DataTable("proveedor");
            SqlConnection SqlCOn = new SqlConnection();
            try
            {
                SqlCOn.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCOn;
                SqlCmd.CommandText = "spbuscar_proveedor_num_documento";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Proveedor.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex) { DtResultado = null; }
            return DtResultado;
        }
    }
}
