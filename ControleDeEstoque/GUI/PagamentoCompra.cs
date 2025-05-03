using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class PagamentoCompra : Form
    {
        public int pcoCod = 0;

        public PagamentoCompra()
        {
            InitializeComponent();
        }

        private void BtLocalizar_Click(object sender, EventArgs e)
        {
            ConsultaCompra f = new ConsultaCompra();
            f.ShowDialog();
            btPagar.Enabled = false;

            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCompra bll = new BLLCompra(cx);
                ModeloCompra modelo = bll.CarregaModeloCompra(f.codigo);
                txtCodigo.Text = modelo.ComCod.ToString();
                dtData.Value = modelo.ComData;
                BLLFornecedor bllf = new BLLFornecedor(cx);
                ModeloFornecedor modelof = bllf.CarregaModeloFornecedor(modelo.ForCod);
                txtFornecedor.Text = modelof.ForNome;
                txtValor.Text = modelo.ComTotal.ToString();

                BLLParcelasCompra bllp = new BLLParcelasCompra(cx);
                dgvParcelas.DataSource = bllp.Localizar(modelo.ComCod);

                dgvParcelas.Columns[0].HeaderText = "Parcela";
                dgvParcelas.Columns[1].HeaderText = "Valor da parcela";
                dgvParcelas.Columns[2].HeaderText = "Pago em";
                dgvParcelas.Columns[3].HeaderText = "Vencimento";

                //dgvDados.Columns[0].Width = 50;
                dgvParcelas.Columns[4].Visible = false;
            }
        }

        private void BtPagar_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLParcelasCompra bllp = new BLLParcelasCompra(cx);
            int comCod = Convert.ToInt32(txtCodigo.Text);
            DateTime data = dtpPagto.Value;
            bllp.EfetuaPagamentoParcela(comCod, pcoCod, data);

            dgvParcelas.DataSource = bllp.Localizar(comCod);
            btPagar.Enabled = false;

            dgvParcelas.Columns[0].HeaderText = "Parcela";
            dgvParcelas.Columns[1].HeaderText = "Valor da parcela";
            dgvParcelas.Columns[2].HeaderText = "Pago em";
            dgvParcelas.Columns[3].HeaderText = "Vencimento";

            //dgvDados.Columns[0].Width = 50;
            dgvParcelas.Columns[4].Visible = false;
        }

        private void DgvParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btPagar.Enabled = false;
            pcoCod = 0;

            if (e.RowIndex >= 0 && dgvParcelas.Rows[e.RowIndex].Cells[2].Value.ToString() == "")
            {
                btPagar.Enabled = true;
                pcoCod = Convert.ToInt32(dgvParcelas.Rows[e.RowIndex].Cells[0].Value);
            }
        }
    }
}
