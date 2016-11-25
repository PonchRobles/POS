using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
  public   class NPresentacion
    {
        public static string Insertar(string nombre, string descripcion)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.Nombre = nombre;
            Obj.Descripcion1 = descripcion;

            return Obj.Insertar(Obj);
        }
        //Metodo Editar que llama al metodo insertar de la clase DPresentacion
        //De la capa datos
        public static string Editar(int idpresentacion, string nombre, string descripcion)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.Idpresentacion = idpresentacion;
            Obj.Nombre = nombre;
            Obj.Descripcion1 = descripcion;

            return Obj.Editar(Obj);
        }
        //Metodo Eliminar que llama al metodo insertar de la clase DPresentacion
        //De la capa datos
        public static string Eliminar(int idpresentacion)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.Idpresentacion = idpresentacion;

            return Obj.Eliminar(Obj);
        }
        //Metodo Mostrar que llama al metodo insertar de la clase DPresentacion
        //De la capa datos
        public static DataTable Mostrar()
        {

            return new DPresentacion().Mostrar();
        }
        //Metodo Buscar Nombre que llama al metodo insertar de la clase DPresentacion
        //De la capa datos

        public static DataTable BuscarNombre(string textobuscar)
        {
            DPresentacion Obj = new DPresentacion();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);

        }
    }
}
