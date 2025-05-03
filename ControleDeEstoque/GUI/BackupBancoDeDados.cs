using DAL;
using Ferramentas;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class BackupBancoDeDados : Form
    {
        public BackupBancoDeDados()
        {
            InitializeComponent();
        }

        private void Backup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog d = new SaveFileDialog
                {
                    Filter = "Backup Files|*.bak"
                };

                d.ShowDialog();
                if (d.FileName != "")
                {
                    String nomeBanco = DadosDaConexao.banco;
                    String localBackup = d.FileName;
                    String conexao = @"Data Source=" + DadosDaConexao.servidor + ";Initial Catalog=master;User=" +
                        DadosDaConexao.usuario + ";Password=" + DadosDaConexao.senha;

                    SQLServerBackup.BackupDataBase(conexao, nomeBanco, d.FileName);
                    MessageBox.Show("Backup realizado com sucesso!!!!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog d = new OpenFileDialog
                {
                    Filter = "Backup Files|*.bak"
                };

                d.ShowDialog();
                if (d.FileName != "")
                {
                    String nomeBanco = DadosDaConexao.banco;
                    String localBackup = d.FileName;
                    String conexao = @"Data Source=" + DadosDaConexao.servidor + ";Initial Catalog=master;User=" +
                        DadosDaConexao.usuario + ";Password=" + DadosDaConexao.senha;

                    SQLServerBackup.RestauraDatabase(conexao, nomeBanco, d.FileName);
                    MessageBox.Show("Backup restaurado com sucesso!!!!");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
