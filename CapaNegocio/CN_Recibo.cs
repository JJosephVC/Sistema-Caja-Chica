using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Recibo
    {
        private CD_Recibos obj = new CD_Recibos();

        public DataTable MostrarRec()
        {
            DataTable tabla = new DataTable();
            tabla = obj.Mostrar();
            return tabla;
        }

        public void InsertarRec(string roc, string fecha, string solici, string monto, string concep, string canti)
        {
            obj.Insertar(Convert.ToInt32(roc), fecha, solici, Convert.ToDouble(monto), concep, canti);
        }

        public void EditarRec(string roc, string fecha, string solici, string monto, string concep, string canti)
        {
            obj.Editar(Convert.ToInt32(roc), fecha, solici, Convert.ToDouble(monto), concep, canti);
        }
        public void EliminarRec(string roc)
        {
            obj.Eliminar(Convert.ToInt32(roc));
        }
    }
}