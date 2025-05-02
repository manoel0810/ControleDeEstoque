using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroCategoria : ModeloDeFormularioDeCadastro
    {
        private const string _alerar = "alterar";
        private const string _inserir = "inserir";
        public CadastroCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
        }

        private void CadastroCategoria_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            AlteraBotoes(2);
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            LimpaTela();
            AlteraBotoes(1);
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloCategoria modelo = new ModeloCategoria
                {
                    CatNome = txtNome.Text
                };

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCategoria bll = new BLLCategoria(cx);

                if (operacao.Equals(_inserir, StringComparison.OrdinalIgnoreCase))
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.CatCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //alterar uma categoria
                    modelo.CatCod = Convert.ToInt32(txtCodigo.Text);
                    bll.Alterar(modelo);
                    MessageBox.Show(UIConstants.CadastroAlterado, UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpaTela();
                AlteraBotoes(1);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message, UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            operacao = _alerar;
            AlteraBotoes(2);
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCategoria bll = new BLLCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtCodigo.Text));

                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro.\nO registro esta sendo utilizado em outro local.", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Information);
                AlteraBotoes(3);
            }
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (frmConsultaCategoria f = new frmConsultaCategoria())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCategoria bll = new BLLCategoria(cx);
                    ModeloCategoria modelo = bll.CarregaModeloCategoria(f.codigo);
                    txtCodigo.Text = modelo.CatCod.ToString();
                    txtNome.Text = modelo.CatNome;
                    AlteraBotoes(3);
                }
                else
                {
                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
        }
    }
}
