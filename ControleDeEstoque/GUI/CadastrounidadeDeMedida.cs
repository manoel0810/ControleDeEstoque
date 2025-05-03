using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastrounidadeDeMedida : ModeloDeFormularioDeCadastro
    {
        public CadastrounidadeDeMedida()
        {
            InitializeComponent();
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            AlteraBotoes(2);
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            operacao = _alterar;
            AlteraBotoes(2);
        }

        public void LimpaTela()
        {
            txtCod.Clear();
            txtUnidadeMedida.Clear();
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                    bll.Excluir(Convert.ToInt32(txtCod.Text));

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
                ModeloUnidadeDeMedida modelo = new ModeloUnidadeDeMedida
                {
                    UmedNome = txtUnidadeMedida.Text
                };


                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                if (operacao.Equals(_inserir, StringComparison.InvariantCultureIgnoreCase))
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.UmedCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //alterar uma categoria
                    modelo.UmedCod = Convert.ToInt32(txtCod.Text);
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

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaUnidadeDeMedida f = new ConsultaUnidadeDeMedida())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                    ModeloUnidadeDeMedida modelo = bll.CarregaModeloUnidadeDeMedida(f.codigo);
                    txtCod.Text = modelo.UmedCod.ToString();
                    txtUnidadeMedida.Text = modelo.UmedNome;
                    AlteraBotoes(3);
                }
                else
                {
                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
        }

        private void UnidadeMedida_Leave(object sender, EventArgs e)
        {
            if (operacao == _inserir)
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLUnidadeDeMedida bll = new BLLUnidadeDeMedida(cx);
                int r = bll.VerificaUnidadeDeMedida(txtUnidadeMedida.Text);

                if (r > 0)
                {
                    if (DialogResult.Yes == MessageBox.Show("Já existe um registro com esse valor. Deseja alterar o registro?", UIConstants.Aviso, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        operacao = _alterar;
                        ModeloUnidadeDeMedida modelo = bll.CarregaModeloUnidadeDeMedida(r);
                        txtCod.Text = modelo.UmedCod.ToString();
                        txtUnidadeMedida.Text = modelo.UmedNome;
                        // alteraBotoes(3);
                    }
                }
            }
        }
    }
}
