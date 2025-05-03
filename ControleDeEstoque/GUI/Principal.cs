using DAL;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void ShowForm(Form form)
        {
            form.ShowDialog();
            form.Dispose();
        }

        private void CategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroCategoria());
        }

        private void CategoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaCategoria());
        }

        private void SubCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroSubCategoria());
        }

        private void SubCategoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaSubCategoria());
        }

        private void UnidadeDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastrounidadeDeMedida());
        }

        private void UnidadeDeMedidaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaUnidadeDeMedida());
        }

        private void ProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroProduto());
        }

        private void ProdutoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaProduto());
        }

        private void ConfiguraçãoDoBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new ConfiguracaoBancoDados());
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //verifica conexao com o banco
            try
            {
                StreamReader arquivo = new StreamReader("ConfiguracaoBanco.txt");
                DadosDaConexao.autenticacaoWindows = Convert.ToBoolean(arquivo.ReadLine());
                DadosDaConexao.servidor = arquivo.ReadLine();
                DadosDaConexao.banco = arquivo.ReadLine();
                DadosDaConexao.usuario = arquivo.ReadLine();
                DadosDaConexao.senha = arquivo.ReadLine();
                arquivo.Close();

                //testar a conexao
                SqlConnection conexao = new SqlConnection
                {
                    ConnectionString = DadosDaConexao.StringDeConexao
                };

                conexao.Open();
                conexao.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Ocorreu um erro ao se conectar com o banco de dados. Favor verificar os dados informados", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackupDoBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new BackupBancoDeDados());
        }

        private void CalculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer");
        }

        private void BlocoDeNotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad");
        }

        private void TipoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroTipoPagamento());
        }

        private void ClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroCliente());
        }

        private void ClienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaCliente());
        }

        private void FornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new CadastroFornecedor());
        }

        private void FornecedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaFornecedor());
        }

        private void TipoDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaTipoPagamento());
        }

        private void CompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new MovimentacoesCompra());
        }

        private void CompraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaCompra());
        }

        private void PagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new PagamentoCompra());
        }

        private void VendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new MovimentacaoVenda());
        }

        private void VendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new ConsultaVenda());
        }

        private void RecebimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new RecebimentoVenda());
        }
    }
}
