using BLL;
using DAL;
using Ferramentas;
using Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroFornecedor : GUI.ModeloDeFormularioDeCadastro
    {
        public CadastroFornecedor()
        {
            InitializeComponent();
        }

        private enum Campo
        {
            CPF = 1,
            CNPJ = 2,
            CEP = 3
        }

        private enum CampoT
        {
            Telefone = 1,
        }

        private void FormatarTelefone(CampoT valor, TextBox txtTexto)
        {
            if (valor == CampoT.Telefone)
            {
                AplicarFormato(txtTexto, 15, new Dictionary<int, string>
                {
                    { 0, "(" },
                    { 3, ")" },
                    { 5, " " },
                    { 10, "-" }
                });
            }
        }

        private void Formatar(Campo valor, TextBox txtTexto)
        {
            switch (valor)
            {
                case Campo.CPF:
                    AplicarFormato(txtTexto, 14, new Dictionary<int, string>
                    {
                        { 3, "." },
                        { 7, "." },
                        { 11, "-" }
                    });
                    break;

                case Campo.CNPJ:
                    AplicarFormato(txtTexto, 18, new Dictionary<int, string>
                    {
                        { 2, "." },
                        { 6, "." },
                        { 10, "/" },
                        { 15, "-" }
                    });
                    break;

                case Campo.CEP:
                    AplicarFormato(txtTexto, 9, new Dictionary<int, string>
                    {
                        { 5, "-" }
                    });
                    break;
            }
        }

        private void AplicarFormato(TextBox txtTexto, int maxLength, Dictionary<int, string> insercoes)
        {
            txtTexto.MaxLength = maxLength;

            if (insercoes.TryGetValue(txtTexto.Text.Length, out var caractere))
            {
                txtTexto.Text += caractere;
                txtTexto.SelectionStart = txtTexto.Text.Length;
            }
        }

        private void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtRsocial.Clear();
            txtBairro.Clear();
            txtCelular.Clear();
            txtCep.Clear();
            txtCidade.Clear();
            txtCNPJ.Clear();
            txtEmail.Clear();
            txtEstado.Clear();
            txtFone.Clear();
            txtNumero.Clear();
            txtIE.Clear();
            txtRua.Clear();
            lbValorIncorreto.Visible = false;
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = "inserir";
            AlteraBotoes(2);
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaFornecedor f = new ConsultaFornecedor())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLFornecedor bll = new BLLFornecedor(cx);
                    ModeloFornecedor modelo = bll.CarregaModeloFornecedor(f.codigo);
                    txtCodigo.Text = modelo.ForCod.ToString();
                    txtNome.Text = modelo.ForNome;
                    txtRsocial.Text = modelo.ForRSocial;
                    txtCNPJ.Text = modelo.ForCnpj;
                    txtIE.Text = modelo.ForIe;
                    txtCep.Text = modelo.ForCep;
                    txtEstado.Text = modelo.ForEstado;
                    txtCidade.Text = modelo.ForCidade;
                    txtRua.Text = modelo.ForEndereco;
                    txtNumero.Text = modelo.ForEndNumero;
                    txtBairro.Text = modelo.ForBairro;
                    txtEmail.Text = modelo.ForEmail;
                    txtFone.Text = modelo.ForFone;
                    txtCelular.Text = modelo.ForCelular;
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

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLFornecedor bll = new BLLFornecedor(cx);
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
                ModeloFornecedor modelo = new ModeloFornecedor
                {
                    ForNome = txtNome.Text,
                    ForRSocial = txtRsocial.Text,
                    ForCnpj = txtCNPJ.Text,
                    ForIe = txtIE.Text,
                    ForCep = txtCep.Text,
                    ForCidade = txtCidade.Text,
                    ForEstado = txtEstado.Text,
                    ForEndereco = txtRua.Text,
                    ForEndNumero = txtNumero.Text,
                    ForBairro = txtBairro.Text,
                    ForEmail = txtEmail.Text,
                    ForFone = txtFone.Text,
                    ForCelular = txtCelular.Text
                };

                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLFornecedor bll = new BLLFornecedor(cx);
                if (operacao.Equals(_inserir, StringComparison.OrdinalIgnoreCase))
                {
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.ForCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    modelo.ForCod = Convert.ToInt32(txtCodigo.Text);
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
            AlteraBotoes(1);
            LimpaTela();
        }

        private void CNPJ_Leave(object sender, EventArgs e)
        {
            lbValorIncorreto.Visible = false;
            if (!Validacao.IsCnpj(txtCNPJ.Text))
                lbValorIncorreto.Visible = true;
        }

        private void CNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                Campo edit = Campo.CNPJ;
                Formatar(edit, txtCNPJ);
            }
        }

        private void Cep_Leave(object sender, EventArgs e)
        {
            if (BuscaEndereco.VerificaCEP(txtCep.Text))
            {
                txtBairro.Text = BuscaEndereco.bairro;
                txtEstado.Text = BuscaEndereco.estado;
                txtCidade.Text = BuscaEndereco.cidade;
                txtRua.Text = BuscaEndereco.endereco;
            }
        }

        private void Fone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;

            if (e.KeyChar != (char)8)
            {
                FormatarTelefone(CampoT.Telefone, txtFone);
            }
        }
    }

}
