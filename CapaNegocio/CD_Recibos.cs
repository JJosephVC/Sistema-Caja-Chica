using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Recibos
    {
        private CD_Conexion conexion = new CD_Conexion();

        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand cmd = new SqlCommand();

        // Tabla para mostrar los datos insertados en la Base de Datos
        public DataTable Mostrar()
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "Select * from REGISTRO_RECIBOS";
            leer = cmd.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        // Método para insertar registros a la Base de Datos
        public void Insertar(int roc, string fecha, string solici, double monto, string concep, string canti)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "insert into REGISTRO_RECIBOS values ('"+ roc +"' , '"+ fecha +"' , '"+ solici +"' , '"+ monto +"' , '"+ concep +"' , '"+ canti +"')";
            //cmd.Parameters.AddWithValue("@roc", roc);
            //cmd.Parameters.AddWithValue("@fecha", fecha);
            //cmd.Parameters.AddWithValue("@solicitante", solici);
            //cmd.Parameters.AddWithValue("@monto", monto);
            //cmd.Parameters.AddWithValue("@concepto", concep);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();  

            cmd.Parameters.Clear();
        }

        // Método para editar registros en la Base de Datos
        public void Editar(int roc, string fecha, string solici, double monto, string concep, string canti)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = @"update REGISTRO_RECIBOS 
                                set ROC = @roc, FECHA_RECIBO = @fecha, SOLICITANTE = @solici, MONTO = @monto, CONCEPTO = @concep, CANTIDAD =@canti
                                where ROC = @roc";
            cmd.Parameters.AddWithValue("@roc", roc);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@solici", solici);
            cmd.Parameters.AddWithValue("@monto", monto);
            cmd.Parameters.AddWithValue("@concep", concep);
            cmd.Parameters.AddWithValue("@canti", canti);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
        }

        // Método para eliminar registros en la Base de Datos
        public void Eliminar(int roc)
        {
            cmd.Connection = conexion.AbrirConexion();
            cmd.CommandText = "delete from REGISTRO_RECIBOS where ROC=@roc";
            cmd.Parameters.AddWithValue("@roc", roc);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
    }
}
