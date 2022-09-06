using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Leo la cadena de conexión del archivo app.Config
            string miCadenaConexion = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            //Creo la conexión a base de datos
            SqlConnection sqlConnection = new SqlConnection(miCadenaConexion);
            sqlConnection.Open();

            //Creo la sentencia SQL
            string query = "SELECT * FROM Producto";

            //Creo un Command => SqlCommand (Donde se guarda la sentencia) => 
            //Consulta es la variable que guarda la sentencia SQL a ejecutar
            //cnn es el objeto conection que realizamos para poder conectarnos a la base de datos

            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            //Acá podemos trabajar de dos maneras diferentes
            this.UsandoSqlDataAdapter(cmd);
            this.UsandoSqlDataReader(cmd);
            
            sqlConnection.Close();

        }

        private void UsandoSqlDataAdapter(SqlCommand cmd)
        {
            //Crear el DataAdapter
            //y le pasamos el Command para mantener el resultado de nuestra sentencia
            SqlDataAdapter ad = new SqlDataAdapter(cmd);

            //Nueva instancia de un dataset. Un DataSet representa un caché de memoria interna de datos
            DataSet ds = new DataSet();

            //Luego le pasamos entre parentesis al metodo fill del adapter el objeto dataset que queremos llenar.
            // El metodo fill equivale a un SelectCommand
            ad.Fill(ds);

            //ya tenemos nuestro dataset cargado.  

            //Si lo queremos mostrar en un datagrid hacemos lo siguiente:
            this.dataGridView1.DataSource = ds.Tables[0];

            //Si lo queremos mostrar en un ComboBox:
            //this.comboBox1.DataSource = ds.Tables[0];
            //this.comboBox1.DisplayMember = "Nombre";
            //this.comboBox1.ValueMember = "idPcia";

            //Si lo queremos mostrar en un ListBox:
            //this.listBox1.DataSource = ds.Tables[0];
            //this.listBox1.DisplayMember = "Nombre";
            //this.listBox1.ValueMember = "idPcia";
        }

        private void UsandoSqlDataReader(SqlCommand cmd)
        {
            SqlDataReader dr = cmd.ExecuteReader();

            this.dataGridView2.Columns.Add("id", "id");
            this.dataGridView2.Columns.Add("descripcion", "descripcion");

            while (dr.Read())
            {
                //DataGridView
                string numero = dr["id"].ToString();
                string nombre = dr["descripcion"].ToString();
                this.dataGridView2.Rows.Add(dr["id"], dr["descripcion"]);

                //ComboBox
                //this.comboBox2.Items.Add(dr["Nombre"]);

                //ListBox
                //this.listBox2.Items.Add(dr["Nombre"]);
            }
        }
    }
}