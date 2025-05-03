using DAL;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class ConfiguracaoBancoDados : Form
    {
        public ConfiguracaoBancoDados()
        {
            InitializeComponent();
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter arquivo = new StreamWriter("ConfiguracaoBanco.txt", false);
                arquivo.WriteLine(AutenticacaoWindowsCheckBox.Checked.ToString());
                arquivo.WriteLine(txtServidor.Text);
                arquivo.WriteLine(txtBanco.Text);
                arquivo.WriteLine(txtUsuario.Text);
                arquivo.WriteLine(txtSenha.Text);
                arquivo.Close();
                MessageBox.Show("Arquivo Atualizado com sucesso!", UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfiguracaoBancoDados_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader arquivo = new StreamReader("ConfiguracaoBanco.txt");
                AutenticacaoWindowsCheckBox.Checked = Convert.ToBoolean(arquivo.ReadLine());
                txtServidor.Text = arquivo.ReadLine();
                txtBanco.Text = arquivo.ReadLine();
                txtUsuario.Text = arquivo.ReadLine();
                txtSenha.Text = arquivo.ReadLine();
                arquivo.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Testar_Click(object sender, EventArgs e)
        {
            try
            {
                DadosDaConexao.servidor = txtServidor.Text;
                DadosDaConexao.banco = txtBanco.Text;
                DadosDaConexao.usuario = txtUsuario.Text;
                DadosDaConexao.senha = txtSenha.Text;

                SqlConnection conexao = new SqlConnection
                {
                    ConnectionString = DadosDaConexao.StringDeConexao
                };

                conexao.Open();
                conexao.Close();
                MessageBox.Show("Conexão efetuada com sucesso", UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void AutenticacaoWindowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutenticacaoWindowsCheckBox.Checked)
            {
                txtUsuario.Enabled = false;
                txtSenha.Enabled = false;
                txtUsuario.Text = "";
                txtSenha.Text = "";
            }
            else
            {
                txtUsuario.Enabled = true;
                txtSenha.Enabled = true;
            }
        }
    }
}
