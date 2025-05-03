using BLL;
using DAL;
using Modelo;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroSubCategoria : ModeloDeFormularioDeCadastro
    {
        public CadastroSubCategoria()
        {
            InitializeComponent();
        }

        public void LimpaTela()
        {
            txtNome.Clear();
            txtScatCod.Clear();
        }

        private void CadastroSubCategoria_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);

            cbCatCod.DataSource = bll.Localizar("");
            cbCatCod.DisplayMember = "cat_nome";
            cbCatCod.ValueMember = "cat_cod";
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            AlteraBotoes(2);
            operacao = _inserir;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            LimpaTela();
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloSubCategoria modelo = new ModeloSubCategoria
                {
                    ScatNome = txtNome.Text,
                    CatCod = Convert.ToInt32(cbCatCod.SelectedValue)
                };

                //obj para gravar os dados no banco
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLSubCategoria bll = new BLLSubCategoria(cx);

                if (operacao.Equals(_inserir, StringComparison.InvariantCultureIgnoreCase))
                {
                    //cadastrar uma categoria
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.ScatCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //alterar uma categoria
                    modelo.ScatCod = Convert.ToInt32(txtScatCod.Text);
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

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLSubCategoria bll = new BLLSubCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtScatCod.Text));

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

        private void Alterar_Click(object sender, EventArgs e)
        {
            AlteraBotoes(2);
            operacao = _alterar;
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaSubCategoria f = new ConsultaSubCategoria())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLSubCategoria bll = new BLLSubCategoria(cx);
                    ModeloSubCategoria modelo = bll.CarregaModeloSubCategoria(f.codigo);
                    txtScatCod.Text = modelo.ScatCod.ToString();
                    txtNome.Text = modelo.ScatNome;
                    cbCatCod.SelectedValue = modelo.CatCod;
                    AlteraBotoes(3);
                }
                else
                {
                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            CadastroCategoria f = new CadastroCategoria();
            f.ShowDialog();
            f.Dispose();

            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            cbCatCod.DataSource = bll.Localizar("");
            cbCatCod.DisplayMember = "cat_nome";
            cbCatCod.ValueMember = "cat_cod";

        }
    }
}
