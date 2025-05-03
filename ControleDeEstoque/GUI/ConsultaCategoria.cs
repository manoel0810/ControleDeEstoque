using BLL;
using DAL;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class ConsultaCategoria : Form
    {
        public int codigo = 0;
        public ConsultaCategoria()
        {
            InitializeComponent();
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            dgvDados.DataSource = bll.Localizar(txtValor.Text);
        }

        private void ConsultaCategoria_Load(object sender, EventArgs e)
        {
            Localizar_Click(sender, e);
            dgvDados.Columns[0].HeaderText = "Código";
            dgvDados.Columns[0].Width = 50;
            dgvDados.Columns[1].HeaderText = "Categoria";
            dgvDados.Columns[1].Width = 700;
        }

        private void Dados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                codigo = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells[0].Value);
                Close();
            }
        }
    }
}
