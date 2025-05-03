using BLL;
using DAL;
using Modelo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class CadastroProduto : ModeloDeFormularioDeCadastro
    {
        public string foto = "";
        public CadastroProduto()
        {
            InitializeComponent();
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            AlteraBotoes(2);
        }

        private void LimpaTela()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtDescricao.Clear();
            txtValorPago.Clear();
            txtValorVenda.Clear();
            txtQtde.Clear();
            foto = "";
            pbFoto.Image = null;
        }

        private void CadastroProduto_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            //combo da categoria
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            cbCategoria.DataSource = bll.Localizar("");
            cbCategoria.DisplayMember = "cat_nome";
            cbCategoria.ValueMember = "cat_cod";

            try
            {
                //combo da subcategoria
                BLLSubCategoria sbll = new BLLSubCategoria(cx);
                cbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cbCategoria.SelectedValue);
                cbSubCategoria.DisplayMember = "scat_nome";
                cbSubCategoria.ValueMember = "scat_cod";
            }
            catch { }

            //combo und medida
            BLLUnidadeDeMedida ubll = new BLLUnidadeDeMedida(cx);
            cbUnd.DataSource = ubll.Localizar("");
            cbUnd.DisplayMember = "umed_nome";
            cbUnd.ValueMember = "umed_cod";

        }

        private void ValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isBackspace = e.KeyChar == (char)Keys.Back;
            bool isDecimalSeparator = e.KeyChar == ',' || e.KeyChar == '.';

            if (!isDigit && !isBackspace && !isDecimalSeparator)
            {
                e.Handled = true;
                return;
            }

            if (isDecimalSeparator)
            {
                if (txtValorPago.Text.Contains(","))
                    e.Handled = true;
                else
                    e.KeyChar = ',';
            }
        }


        private void ValorPago_Leave(object sender, EventArgs e)
        {
            string texto = txtValorPago.Text;

            if (!texto.Contains(","))
                texto += ",00";
            else if (texto.EndsWith(","))
                texto += "00";

            if (double.TryParse(texto, out double valor))
                txtValorPago.Text = valor.ToString("N2");
            else
                txtValorPago.Text = "0,00";
        }


        private void ValorVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isBackspace = e.KeyChar == (char)Keys.Back;
            bool isDecimalSeparator = e.KeyChar == ',' || e.KeyChar == '.';

            if (!isDigit && !isBackspace && !isDecimalSeparator)
            {
                e.Handled = true;
                return;
            }

            if (isDecimalSeparator)
            {
                if (txtValorVenda.Text.Contains(","))
                    e.Handled = true;

                else
                    e.KeyChar = ',';
            }
        }


        private void ValorVenda_Leave(object sender, EventArgs e)
        {
            string texto = txtValorVenda.Text;

            if (!texto.Contains(","))
                texto += ",00";
            else if (texto.EndsWith(","))
                texto += "00";

            if (double.TryParse(texto, out double valor))
                txtValorVenda.Text = valor.ToString("N2");
            else
                txtValorVenda.Text = "0,00";
        }


        private void Qtde_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isDigit = char.IsDigit(e.KeyChar);
            bool isBackspace = e.KeyChar == (char)Keys.Back;
            bool isDecimalSeparator = e.KeyChar == ',' || e.KeyChar == '.';

            if (!isDigit && !isBackspace && !isDecimalSeparator)
            {
                e.Handled = true;
                return;
            }

            if (isDecimalSeparator)
            {
                if (txtQtde.Text.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.KeyChar = ',';
                }
            }
        }

        private void Qtde_Leave(object sender, EventArgs e)
        {
            string texto = txtQtde.Text;

            if (!texto.Contains(","))
            {
                texto += ",00";
            }
            else if (texto.EndsWith(","))
            {
                texto += "00";
            }

            if (double.TryParse(texto, out double valor))
            {
                txtQtde.Text = valor.ToString("N2");
            }
            else
            {
                txtQtde.Text = "0,00";
            }
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            operacao = _alterar;
            AlteraBotoes(2);
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                //leitura dos dados
                ModeloProduto modelo = new ModeloProduto
                {
                    ProNome = txtNome.Text,
                    ProDescricao = txtDescricao.Text,
                    ProValorPago = Convert.ToDouble(txtValorPago.Text),
                    ProValorVenda = Convert.ToDouble(txtValorVenda.Text),
                    ProQtde = Convert.ToDouble(txtQtde.Text),
                    UmedCod = Convert.ToInt32(cbUnd.SelectedValue),
                    CatCod = Convert.ToInt32(cbCategoria.SelectedValue),
                    ScatCod = Convert.ToInt32(cbSubCategoria.SelectedValue)
                };

                //obj para gravar os dados no banco
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);
                if (operacao.Equals(_inserir, StringComparison.OrdinalIgnoreCase))
                {
                    //cadastrar uma Produto
                    modelo.CarregaImagem(foto);
                    bll.Incluir(modelo);
                    MessageBox.Show(string.Format(UIConstants.CadastroEfetuado, modelo.CatCod), UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    modelo.ProCod = Convert.ToInt32(txtCodigo.Text);
                    //alterar um produto
                    if (foto == "Foto Original")
                    {
                        ModeloProduto mt = bll.CarregaModeloProduto(modelo.ProCod);
                        modelo.ProFoto = mt.ProFoto;
                    }
                    else
                    {
                        modelo.CarregaImagem(foto);
                    }

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

        private void Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combo da categoria
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            try
            {
                cbSubCategoria.Text = "";
                //combo da subcategoria
                BLLSubCategoria sbll = new BLLSubCategoria(cx);
                cbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cbCategoria.SelectedValue);
                cbSubCategoria.DisplayMember = "scat_nome";
                cbSubCategoria.ValueMember = "scat_cod";
            }
            catch { }
        }

        private void LoFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.ShowDialog();

            if (!string.IsNullOrEmpty(od.FileName))
            {
                foto = od.FileName;
                pbFoto.Load(foto);
            }
        }

        private void RmFoto_Click(object sender, EventArgs e)
        {
            foto = "";
            pbFoto.Image = null;
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaProduto f = new ConsultaProduto())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLProduto bll = new BLLProduto(cx);
                    ModeloProduto modelo = bll.CarregaModeloProduto(f.codigo);
                    txtCodigo.Text = modelo.ProCod.ToString();
                    //colocar os dados na tela
                    txtDescricao.Text = modelo.ProDescricao;
                    txtNome.Text = modelo.ProNome;
                    txtQtde.Text = modelo.ProQtde.ToString();
                    txtValorPago.Text = modelo.ProValorPago.ToString();
                    txtValorVenda.Text = modelo.ProValorVenda.ToString();
                    cbCategoria.SelectedValue = modelo.CatCod;
                    cbSubCategoria.SelectedValue = modelo.ScatCod;
                    cbUnd.SelectedValue = modelo.UmedCod;
                    try
                    {
                        MemoryStream ms = new MemoryStream(modelo.ProFoto);
                        pbFoto.Image = Image.FromStream(ms);
                        foto = "Foto Original";
                    }
                    catch { }
                    Qtde_Leave(sender, e);
                    ValorPago_Leave(sender, e);
                    ValorVenda_Leave(sender, e);
                    AlteraBotoes(3);
                }
                else
                {
                    LimpaTela();
                    AlteraBotoes(1);
                }
            }
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLProduto bll = new BLLProduto(cx);
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

        private void Cancelar_Click(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            LimpaTela();
        }

        private void AddCategoria_Click(object sender, EventArgs e)
        {
            CadastroCategoria f = new CadastroCategoria();
            f.ShowDialog();
            f.Dispose();

            //combo da categoria
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCategoria bll = new BLLCategoria(cx);
            cbCategoria.DataSource = bll.Localizar("");
            cbCategoria.DisplayMember = "cat_nome";
            cbCategoria.ValueMember = "cat_cod";
            try
            {
                //combo da subcategoria
                BLLSubCategoria sbll = new BLLSubCategoria(cx);
                cbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cbCategoria.SelectedValue);
                cbSubCategoria.DisplayMember = "scat_nome";
                cbSubCategoria.ValueMember = "scat_cod";
            }
            catch { }

        }

        private void AddSubCategoria_Click(object sender, EventArgs e)
        {
            CadastroSubCategoria f = new CadastroSubCategoria();
            f.ShowDialog();
            f.Dispose();

            //combo da categoria
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);

            try
            {
                //combo da subcategoria
                BLLSubCategoria sbll = new BLLSubCategoria(cx);
                cbSubCategoria.DataSource = sbll.LocalizarPorCategoria((int)cbCategoria.SelectedValue);
                cbSubCategoria.DisplayMember = "scat_nome";
                cbSubCategoria.ValueMember = "scat_cod";
            }
            catch { }
        }

        private void AddUnidadeMedida_Click(object sender, EventArgs e)
        {
            CadastrounidadeDeMedida f = new CadastrounidadeDeMedida();
            f.ShowDialog();
            f.Dispose();

            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            //combo und medida

            BLLUnidadeDeMedida ubll = new BLLUnidadeDeMedida(cx);
            cbUnd.DataSource = ubll.Localizar("");
            cbUnd.DisplayMember = "umed_nome";
            cbUnd.ValueMember = "umed_cod";

        }
    }
}
