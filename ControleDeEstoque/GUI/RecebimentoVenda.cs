using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class RecebimentoVenda : Form
    {
        public int pveCod;
        public RecebimentoVenda()
        {
            InitializeComponent();
        }

        private void TBtLocalizar_Click(object sender, EventArgs e)
        {
            btReceber.Enabled = false;
            ConsultaVenda f = new ConsultaVenda();
            f.ShowDialog();

            if (f.codigo != 0)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLVenda bll = new BLLVenda(cx);
                ModeloVenda modelo = bll.CarregaModeloVenda(f.codigo);
                txtCodigo.Text = modelo.VenCod.ToString();
                dtData.Value = modelo.VenData;
                BLLCliente bllf = new BLLCliente(cx);
                ModeloCliente modeloc = bllf.CarregaModeloCliente(modelo.CliCod);
                txtCliente.Text = modeloc.CliNome;
                txtValor.Text = modelo.VenTotal.ToString();

                BLLParcelasVenda bllp = new BLLParcelasVenda(cx);
                dgvParcelas.DataSource = bllp.Localizar(modelo.VenCod);

                dgvParcelas.Columns[0].HeaderText = "Parcela";
                dgvParcelas.Columns[1].HeaderText = "Valor da parcela";
                dgvParcelas.Columns[2].HeaderText = "Recebido em";
                dgvParcelas.Columns[3].HeaderText = "Vencimento";
            }
        }

        private void DgvParcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btReceber.Enabled = false;
            pveCod = 0;

            if (e.RowIndex >= 0 && dgvParcelas.Rows[e.RowIndex].Cells[2].Value.ToString() == "")
            {
                btReceber.Enabled = true;
                pveCod = Convert.ToInt32(dgvParcelas.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void BtReceber_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLParcelasVenda bllp = new BLLParcelasVenda(cx);
            int venCod = Convert.ToInt32(txtCodigo.Text);
            DateTime data = dtpRecebimento.Value;
            bllp.EfetuaRecebimentoParcela(venCod, pveCod, data);

            dgvParcelas.DataSource = bllp.Localizar(venCod);
            btReceber.Enabled = false;

            dgvParcelas.Columns[0].HeaderText = "Parcela";
            dgvParcelas.Columns[1].HeaderText = "Valor da parcela";
            dgvParcelas.Columns[2].HeaderText = "Recebido em";
            dgvParcelas.Columns[3].HeaderText = "Vencimento";
        }
    }
}
