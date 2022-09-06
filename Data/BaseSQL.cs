//DEBO AGREGAR ESTOS "USING" PARA PODER CONECTARME A LA BASE DE DATOS.
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Data
{
    public class BaseSQL
    {
        //"Data Source= Servidor ; Initial Catalog= Base ; Integrated Security=true; ";
        private string stConexion = "Data Source=ENZO\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=BDTiendaDigital";
        //Data source = .
        //(local)
        //andreapc\sqlexpress
        //  .\sqlexpress


        public SqlCommand MiSqlCommand;

        private SqlConnection mConnection;
        public SqlConnection Connection
        {
            get { return mConnection; }
            set { mConnection = value; }
        }


    public BaseSQL()
    {
    }

    private void Open()
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-AR");
            Connection = new SqlConnection(stConexion);
            Connection.Open();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void Close()
    {
        try
        {
            Connection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ExecuteScalar(string sSQL)
    {
        try
        {
            Open();

            SqlCommand oCommand = new SqlCommand(sSQL, Connection);
            int Id = Convert.ToInt32(oCommand.ExecuteScalar());

            return Id;
        }
        catch (Exception)
        {

            return -1;
        }
        finally
        {
            Close();
        }
    }

    public int ExecuteNoQuery(string sSQL)
    {
        try
        {
            Open();
            SqlCommand oCommand = new SqlCommand(sSQL, Connection);
            int Id = oCommand.ExecuteNonQuery();

            return Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Connection.Close();
        }
    }

    public DataTable ExecuteDataTable(string strSQL)
    {
        DataTable functionReturnValue = null;
        Open();

        DataSet oData = new DataSet();
        try
        {
            SqlDataAdapter oAdap = new SqlDataAdapter(strSQL, Connection);
            oAdap.Fill(oData, "Registros");

            functionReturnValue = oData.Tables[0];
        }
        catch (Exception ex)
        {
            Connection.Close();
            throw ex;
        }
        finally
        {
            Connection.Close();
        }
        return functionReturnValue;
    }

    public object ExecuteComando(string strSQL)
    {
        object functionReturnValue = null;
        try
        {
            Open();
            SqlCommand oCommand = new SqlCommand(strSQL, Connection);
            functionReturnValue = oCommand.ExecuteScalar();

        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            Connection.Close();
        }
        return functionReturnValue;

    }

    public SqlDataReader Executereader(string sSQL)
    {
        try
        {
            Open();
            SqlCommand oCommand = new SqlCommand(sSQL, Connection);
            SqlDataReader reader = oCommand.ExecuteReader();
            reader.Close();
            return reader;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            Connection.Close();
        }
    }
    }
}