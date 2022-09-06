using Data;

namespace Negocio
{
    public class clsProductos
    {
        public class Producto
        {
            private int _id;
            private string? _descripcion;

            public string Descripcion
            {
                get { return _descripcion; }
                set { _descripcion = value; }
            }

            public int Id;
        }

        public List<Producto> SelectAll(
            //string stFiltro = ""
        )
        {
            BaseSQL BD = new BaseSQL();

            string stSQL = "SELECT * FROM Producto";

            //if (stFiltro.Length > 0) 
            //    stSQL += " Where nombre like '%" + stFiltro + "%' or apellido like '%" + stFiltro + "%'";
            System.Data.DataTable dat = BD.ExecuteDataTable(stSQL);

            List<Producto> lstProducto = new List<Producto>();
            foreach (System.Data.DataRow item in dat.Rows)
            {
                Producto producto = new Producto();
                producto.Id = Convert.ToInt32(item.ItemArray[0]);
                producto.Descripcion = item.ItemArray[1].ToString();

                lstProducto.Add(producto);
            }
            return lstProducto;

        }

        public Producto Select(Int32 id)
        {
            return null;
        }
        public Int32 TraeMax_ID()
        {
            string stSQL = "SELECT MAX(id) FROM Producto";
            BaseSQL BD = new BaseSQL();
            return BD.ExecuteScalar(stSQL);
        }
        public int Guardar(Producto producto)
        {
            try
            {
                string stSQL = "";
                if (producto.Id == 0)
                {
                    stSQL = "INSERT INTO Producto(descripcion) VALUES " +
                        "('" + producto.Descripcion + "')";
                    Insert(stSQL);
                    return TraeMax_ID();
                }
                else
                {
                    stSQL = "UPDATE Producto SET descripcion ='" + producto.Descripcion + "' WHERE id = " + producto.Id;
                    Update(stSQL);
                    return 0;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private int Insert(string sql)
        {
            try
            {
                BaseSQL BD = new BaseSQL();
                return BD.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Insertar " + ex.Message.ToString());
            }
        }

        private int Update(string SQL)
        {
            try
            {
                BaseSQL BD = new BaseSQL();
                return BD.ExecuteNoQuery(SQL);

            }
            catch (Exception)
            {
                throw new Exception("Error al Actualizar");
            }
        }

        private int Borrar(Producto producto)
        {
            try
            {

                string stSQL = "";

                stSQL = "DELETE * FROM Producto WHERE id = " + producto.Id;
                BaseSQL BD = new BaseSQL();
                return BD.ExecuteNoQuery(stSQL);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al Eliminar " + ex.Message);
            }
        }
    }
}
