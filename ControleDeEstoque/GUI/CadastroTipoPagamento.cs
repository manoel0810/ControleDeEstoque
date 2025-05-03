using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroTipoPagamento : ModeloDeFormularioDeCadastro
    {
        public CadastroTipoPagamento()
        {
            InitializeComponent();
        }

        private void CadastroTipoPagamento_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            AlteraBotoes(2);
            operacao = _inserir;
        }

        private void Localizar_Click(object sender, EventArgs e)
        {

            using (ConsultaTipoPagamento f = new ConsultaTipoPagamento())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLTipoPagamento bll = new BLLTipoPagamento(cx);
                    ModeloTipoPagamento modelo = bll.CarregaModeloTipoPagamento(f.codigo);
                    txtCodigo.Text = modelo.TpaCod.ToString();
                    txtNome.Text = modelo.TpaNome;
                    AlteraBotoes(3);
                }
                else
                {
                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            operacao = _alterar;
            AlteraBotoes(2);
        }

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLTipoPagamento bll = new BLLTipoPagamento(cx);
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

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloTipoPagamento modelo = new ModeloTipoPagamento
                {
                    TpaNome = txtNome.Text
                };

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLTipoPagamento bll = new BLLTipoPagamento(cx);
                if (operacao.Equals(_inserir, StringComparison.InvariantCultureIgnoreCase))
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.TpaCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //alterar uma categoria
                    modelo.TpaCod = Convert.ToInt32(txtCodigo.Text);
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

        private void Cancelar_Click(object sender, EventArgs e)
        {
            LimpaTela();
            AlteraBotoes(1);
        }
    }
}
