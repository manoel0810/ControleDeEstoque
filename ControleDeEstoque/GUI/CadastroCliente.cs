using BLL;
using DAL;
using Ferramentas;
using Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroCliente : ModeloDeFormularioDeCadastro
    {
        public enum Campo
        {
            CPF = 1,
            CNPJ = 2,
            CEP = 3,
        }

        public enum CampoT
        {
            Telefone = 1,
        }

        public void FormatarTelefone(CampoT valor, TextBox txtTexto)
        {
            if (valor == CampoT.Telefone)
            {
                AplicarFormato(txtTexto, 13, new Dictionary<int, string>
                {
                    { 0, "(" },
                    { 3, ")" },
                    { 8, "-" }
                });
            }
        }

        public void Formatar(Campo valor, TextBox txtTexto)
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

        public CadastroCliente()
        {
            InitializeComponent();
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            AlteraBotoes(2);
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaCliente f = new ConsultaCliente())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCliente bll = new BLLCliente(cx);
                    ModeloCliente modelo = bll.CarregaModeloCliente(f.codigo);
                    txtCodigo.Text = modelo.CliCod.ToString();

                    if (modelo.CliTipo == "Física")
                        rbFisica.Checked = true;
                    else
                        rbJuridica.Checked = true;

                    txtNome.Text = modelo.CliNome;
                    txtRsocial.Text = modelo.CliRSocial;
                    txtCPFCNPJ.Text = modelo.CliCpfCnpj;
                    txtRGIE.Text = modelo.CliRgIe;
                    txtCep.Text = modelo.CliCep;
                    txtEstado.Text = modelo.CliEstado;
                    txtCidade.Text = modelo.CliCidade;
                    txtRua.Text = modelo.CliEndereco;
                    txtNumero.Text = modelo.CliEndNumero;
                    txtBairro.Text = modelo.CliBairro;
                    txtEmail.Text = modelo.CliEmail;
                    txtFone.Text = modelo.CliFone;
                    txtCelular.Text = modelo.CliCelular;
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
                    BLLCliente bll = new BLLCliente(cx);
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
                ModeloCliente modelo = new ModeloCliente
                {
                    CliNome = txtNome.Text,
                    CliRSocial = txtRsocial.Text,
                    CliCpfCnpj = txtCPFCNPJ.Text,
                    CliRgIe = txtRGIE.Text,
                    CliCep = txtCep.Text,
                    CliCidade = txtCidade.Text,
                    CliEstado = txtEstado.Text,
                    CliEndereco = txtRua.Text,
                    CliEndNumero = txtNumero.Text,
                    CliBairro = txtBairro.Text,
                    CliEmail = txtEmail.Text,
                    CliFone = txtFone.Text,
                    CliCelular = txtCelular.Text
                };

                if (rbFisica.Checked == true)
                {
                    modelo.CliTipo = "Física"; // fisica
                    modelo.CliRSocial = "";
                }
                else
                    modelo.CliTipo = "Jurídica"; // juridica


                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCliente bll = new BLLCliente(cx);
                if (operacao.Equals(_inserir, StringComparison.OrdinalIgnoreCase))
                {
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.CliCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    modelo.CliCod = Convert.ToInt32(txtCodigo.Text);
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

        public void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtRsocial.Clear();
            txtBairro.Clear();
            txtCelular.Clear();
            txtCep.Clear();
            txtCidade.Clear();
            txtCPFCNPJ.Clear();
            txtEmail.Clear();
            txtEstado.Clear();
            txtFone.Clear();
            txtNumero.Clear();
            txtRGIE.Clear();
            txtRua.Clear();
            rbFisica.Checked = true;
            lbValorIncorreto.Visible = false;
        }

        private void CadastroCliente_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
        }

        private void Fisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFisica.Checked == true)
            {
                lbRSocial.Visible = false;
                txtRsocial.Visible = false;
                lbCPFCNPJ.Text = "CPF";
                lbRGIE.Text = "RG";
            }
            else
            {
                lbRSocial.Visible = true;
                txtRsocial.Visible = true;
                lbCPFCNPJ.Text = "CNPJ";
                lbRGIE.Text = "IE";
            }
        }

        private void Cep_Leave(object sender, EventArgs e)
        {
            if (Validacao.ValidaCep(txtCep.Text) == false)
            {
                MessageBox.Show("O CEP é inválido", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBairro.Clear();
                txtEstado.Clear();
                txtCidade.Clear();
                txtRua.Clear();
            }
            else
            {
                if (BuscaEndereco.VerificaCEP(txtCep.Text) == true)
                {
                    txtBairro.Text = BuscaEndereco.bairro;
                    txtEstado.Text = BuscaEndereco.estado;
                    txtCidade.Text = BuscaEndereco.cidade;
                    txtRua.Text = BuscaEndereco.endereco;
                }
            }
        }

        private void CPFCNPJ_Leave(object sender, EventArgs e)
        {
            lbValorIncorreto.Visible = false;
            if (rbFisica.Checked && !Validacao.IsCpf(txtCPFCNPJ.Text))
            {
                lbValorIncorreto.Visible = true;
            }
            else if (rbJuridica.Checked)
            {
                if (Validacao.IsCnpj(txtCPFCNPJ.Text) == false)
                    lbValorIncorreto.Visible = true;
            }
        }

        private void CPFCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                Campo edit = Campo.CPF;
                if (rbFisica.Checked == false) edit = Campo.CNPJ;
                Formatar(edit, txtCPFCNPJ);
            }
        }

        private void Cep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                Campo edit = Campo.CEP;
                Formatar(edit, txtCep);
            }
        }

        private void Fone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;

            if (e.KeyChar != (char)8)
                FormatarTelefone(CampoT.Telefone, txtFone);
        }
    }
}
