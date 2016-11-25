using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
namespace CapaDatos
{
   public  class DCategoria
    {
        private int _Idcategoria;
        private string _Nombre;
        private string _Descripcion;

        private string _TextoBuscar;

        public int Idcategoria
        {
            get
            {
                return _Idcategoria;
            }

            set
            {
                _Idcategoria = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
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

        //Contructor Vacio
        public DCategoria()
        {

        }
        //Constructor con parametros
        public DCategoria(int idcategoria, string nombre, string descripcion, string textobuscar)
        {
            this.Idcategoria = idcategoria;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.TextoBuscar = textobuscar;
        }

        //Metodo Insertar
        public string Insertar(DCategoria Categoria)
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
                SqlCmd.CommandText = "spinsertar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdecategoria = new SqlParameter();
                ParIdecategoria.ParameterName = "@idcategoria";
                ParIdecategoria.SqlDbType = SqlDbType.Int;
                ParIdecategoria.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdecategoria);

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Categoria.Nombre;
                SqlCmd.Parameters.Add(ParNombre);

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 256;
                ParDescripcion.Value = Categoria.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);
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
        public string Editar(DCategoria Categoria)
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
                SqlCmd.CommandText = "speditar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdecategoria = new SqlParameter();
                ParIdecategoria.ParameterName = "@idcategoria";
                ParIdecategoria.SqlDbType = SqlDbType.Int;
                ParIdecategoria.Value = Categoria.Idcategoria;
                SqlCmd.Parameters.Add(ParIdecategoria);

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Categoria.Nombre;
                SqlCmd.Parameters.Add(ParNombre);

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 256;
                ParDescripcion.Value = Categoria.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);
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

        public string Eliminar(DCategoria Categoria)
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
                SqlCmd.CommandText = "speliminar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdecategoria = new SqlParameter();
                ParIdecategoria.ParameterName = "@idcategoria";
                ParIdecategoria.SqlDbType = SqlDbType.Int;
                ParIdecategoria.Value = Categoria.Idcategoria;
                SqlCmd.Parameters.Add(ParIdecategoria);

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
            DataTable DtResultado = new DataTable("categoria");
            SqlConnection SqlCOn = new SqlConnection();
            try
            {
                SqlCOn.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCOn;
                SqlCmd.CommandText = "spmostrar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex) { DtResultado = null; }
            return DtResultado;

        }
        //Metodo BuscarNombre
        public DataTable BuscarNombre(DCategoria Categoria)
        {
            DataTable DtResultado = new DataTable("categoria");
            SqlConnection SqlCOn = new SqlConnection();
            try
            {
                SqlCOn.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCOn;
                SqlCmd.CommandText = "spbuscar_categoria";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Categoria.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex) { DtResultado = null; }
            return DtResultado;
        }
    }
}
