using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Negocio;

namespace Interfaz
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            LlenaLista();
        }

        private void LlenaLista()
        {
            clsProductos productos = new clsProductos();

            dataGridView1.DataSource = productos.SelectAll();
        }

        private void frmProductos_Load_1(object sender, EventArgs e)
        {
            LlenaLista();
        }
    }
}
