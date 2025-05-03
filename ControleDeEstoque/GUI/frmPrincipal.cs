using DAL;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroCategoria f = new CadastroCategoria();
            f.ShowDialog();
            f.Dispose();
        }

        private void categoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaCategoria f = new ConsultaCategoria();
            f.ShowDialog();
            f.Dispose();
        }

        private void subCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroSubCategoria f = new CadastroSubCategoria();
            f.ShowDialog();
            f.Dispose();
        }

        private void subCategoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaSubCategoria f = new ConsultaSubCategoria();
            f.ShowDialog();
            f.Dispose();
        }

        private void unidadeDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastrounidadeDeMedida f = new CadastrounidadeDeMedida();
            f.ShowDialog();
            f.Dispose();
        }

        private void unidadeDeMedidaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaUnidadeDeMedida f = new ConsultaUnidadeDeMedida();
            f.ShowDialog();
            f.Dispose();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroProduto f = new CadastroProduto();
            f.ShowDialog();
            f.Dispose();
        }

        private void produtoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaProduto f = new ConsultaProduto();
            f.ShowDialog();
            f.Dispose();
        }

        private void configuraçãoDoBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracaoBancoDados f = new frmConfiguracaoBancoDados();
            f.ShowDialog();
            f.Dispose();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //verifica conexao com o banco
            try
            {
                StreamReader arquivo = new StreamReader("ConfiguracaoBanco.txt");
                DadosDaConexao.servidor = arquivo.ReadLine();
                DadosDaConexao.banco = arquivo.ReadLine();
                DadosDaConexao.usuario = arquivo.ReadLine();
                DadosDaConexao.senha = arquivo.ReadLine();
                arquivo.Close();
                //testar a conexao
                SqlConnection conexao = new SqlConnection();

                conexao.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=EstoqueApp;Trusted_Connection=True;MultipleActiveResultSets=true";
                conexao.Open();
                conexao.Close();
            }
            catch (SqlException errob)
            {
                MessageBox.Show("Erro ao se conectar no banco de dados \n" +
                                "Acesse as configurações do banco de dados e informe os parâmetros de conexao");
            }
            catch (Exception erros)
            {
                MessageBox.Show(erros.Message);
            }

        }

        private void backupDoBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupBancoDeDados f = new BackupBancoDeDados();
            f.ShowDialog();
            f.Dispose();
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer");
        }

        private void blocoDeNotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad");
        }

        private void tipoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroTipoPagamento f = new CadastroTipoPagamento();
            f.ShowDialog();
            f.Dispose();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroCliente f = new CadastroCliente();
            f.ShowDialog();
            f.Dispose();
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaCliente f = new ConsultaCliente();
            f.ShowDialog();
            f.Dispose();
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroFornecedor f = new CadastroFornecedor();
            f.ShowDialog();
            f.Dispose();
        }

        private void fornecedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaFornecedor f = new ConsultaFornecedor();
            f.ShowDialog();
            f.Dispose();
        }

        private void tipoDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultaTipoPagamento f = new ConsultaTipoPagamento();
            f.ShowDialog();
            f.Dispose();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSobre f = new frmSobre();
            f.ShowDialog();
            f.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMovimentacaoCompra f = new frmMovimentacaoCompra();
            f.ShowDialog();
            f.Dispose();
        }

        private void compraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCompra f = new frmConsultaCompra();
            f.ShowDialog();
            f.Dispose();
        }

        private void pagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PagamentoCompra f = new PagamentoCompra();
            f.ShowDialog();
            f.Dispose();
        }

        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMovimentacaoVenda f = new frmMovimentacaoVenda();
            f.ShowDialog();
            f.Dispose();
        }

        private void vendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaVenda f = new frmConsultaVenda();
            f.ShowDialog();
            f.Dispose();
        }

        private void recebimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecebimentoVenda f = new RecebimentoVenda();
            f.ShowDialog();
            f.Dispose();
        }
    }
}
