using BLL;
using DAL;
using Modelo;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI
{
    public partial class MovimentacoesCompra : Form
    {
        public double totalCompra = 0;//Variavel Global
        public String operacao;

        private const string _inserir = "inserir";
        private const string _alterar = "alterar";

        public MovimentacoesCompra()
        {
            InitializeComponent();
        }

        public void AlteraBotoes(int op)
        {
            //op = operacoes que serao feitas com os botoes
            //1 = Preparar os botoes para inserir e localizar
            //2 = Preparar os para inserir /alterar um registro
            //3 = Preparr a tela para excluir ou alterar 
            pnDados.Enabled = false;
            btnInserir.Enabled = false;
            btnAlterar.Enabled = false;
            btnLocalizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;

            if (op == 1)
            {
                btnInserir.Enabled = true;
                btnLocalizar.Enabled = true;
            }
            if (op == 2)
            {
                pnDados.Enabled = true;
                btnSalvar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            if (op == 3)
            {
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        public void LimpaTela()
        {
            txtComCodigo.Clear();
            txtNFiscal.Clear();
            txtForCodigo.Clear();
            txtProCod.Clear();
            lProduto.Text = "Informe o código do produto ou clique em localizar";
            txtQtde.Clear();
            txtValor.Clear();
            txtTotalCompra.Clear();
            dgvItens.Rows.Clear();
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            totalCompra = 0;
            AlteraBotoes(2);

        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaCompra f = new ConsultaCompra())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCompra bll = new BLLCompra(cx);
                    ModeloCompra modelo = bll.CarregaModeloCompra(f.codigo);
                    txtComCodigo.Text = modelo.ComCod.ToString();
                    txtNFiscal.Text = modelo.ComNFiscal.ToString();
                    dtDataCompra.Value = modelo.ComData;
                    txtForCodigo.Text = modelo.ForCod.ToString();

                    ForCodigo_Leave(sender, e); //para escrever o nome do fornecedor na tela
                    cbTpagto.SelectedValue = modelo.TpaCod;
                    cbNParcela.Text = modelo.ComNParcelas.ToString();
                    txtTotalCompra.Text = modelo.ComTotal.ToString();
                    totalCompra = modelo.ComTotal; //armazenar o valor total da compra
                                                   //itens da compra

                    BLLItensCompra bllItens = new BLLItensCompra(cx);
                    DataTable tabela = bllItens.Localizar(modelo.ComCod);
                    for (int i = 0; i < tabela.Rows.Count; i++)
                    {
                        string icod = tabela.Rows[i]["pro_cod"].ToString();
                        string inome = tabela.Rows[i]["pro_nome"].ToString();
                        string iqtd = tabela.Rows[i]["itc_qtde"].ToString();
                        string ivu = tabela.Rows[i]["itc_valor"].ToString();
                        Double TotalLocal = Convert.ToDouble(tabela.Rows[i]["itc_qtde"]) * Convert.ToDouble(tabela.Rows[i]["itc_valor"]);

                        String[] it = new String[] { icod, inome, iqtd, ivu, TotalLocal.ToString() };
                        dgvItens.Rows.Add(it);
                    }

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
            int codigo = Convert.ToInt32(txtComCodigo.Text);
            int qtde = Convert.ToInt32(cbNParcela.Text);

            //conexao e bll da compra
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLCompra bllc = new BLLCompra(cx);
            qtde -= bllc.QuantidadeParcelasNaoPagas(codigo);

            if (qtde == 0) //paguei alguma parcela
            {
                operacao = _alterar;
                AlteraBotoes(2);
            }
            else
                MessageBox.Show("Impossivel Alterar o registro.\nO registro possui parcelas pagas.", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(UIConstants.ExcluirConfirmacao, UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int codigo = Convert.ToInt32(txtComCodigo.Text);
                    int qtde = Convert.ToInt32(cbNParcela.Text);

                    //conexao e bll da compra
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLCompra bllc = new BLLCompra(cx);
                    qtde -= bllc.QuantidadeParcelasNaoPagas(codigo);

                    if (qtde == 0) //paguei alguma parcela
                    {
                        cx.Conectar();
                        cx.IniciarTransacao();
                        try
                        {
                            //excluir as parcelas da compra
                            BLLParcelasCompra bllp = new BLLParcelasCompra(cx);
                            bllp.ExcluirTodasAsParcelas(codigo);

                            //itens da compra
                            BLLItensCompra blli = new BLLItensCompra(cx);
                            blli.ExcluirTodosOsItens(codigo);

                            //compra
                            bllc.Excluir(codigo);
                            cx.TerminarTransacao();
                            cx.Desconectar();

                            LimpaTela();
                            AlteraBotoes(1);
                        }
                        catch
                        {
                            MessageBox.Show("Impossível excluir o registro.\nO registro esta sendo utilizado em outro local.", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cx.CancelarTransacao();
                            cx.Desconectar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Impossivel Alterar o registro.\nO registro possui parcelas pagas.", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Impossível excluir o registro.\nO registro esta sendo utilizado em outro local.", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNFiscal.Text))
            {
                MessageBox.Show("Informe número da nota fiscal de compra", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtForCodigo.Text))
            {
                MessageBox.Show("Informe um fornecedor para a compra", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (totalCompra <= 0) //validação dos itens
            {
                MessageBox.Show("Informe os produtos dessa compra", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            dgvParcelas.Rows.Clear();
            int parcelas = Convert.ToInt32(cbNParcela.Text);
            double valor = totalCompra / parcelas;

            DateTime dt = dtDataini.Value;
            lbTotal.Text = totalCompra.ToString();
            for (int i = 1; i <= parcelas; i++)
            {
                String[] k = new String[] { i.ToString(), valor.ToString(), dt.Date.ToString() };
                dgvParcelas.Rows.Add(k);

                if (dt.Month != 12)
                    dt = new DateTime(dt.Year, dt.Month + 1, dt.Day);
                else
                    dt = new DateTime(dt.Year + 1, 1, dt.Day);
            }


            pnFinalizaCompra.Visible = true;
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            LimpaTela();
            totalCompra = 0;
        }

        private void LocFor_Click(object sender, EventArgs e)
        {
            using (ConsultaFornecedor f = new ConsultaFornecedor())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
#pragma warning disable CS1690 // Possível referência nula de argumento.
                    txtForCodigo.Text = f.codigo.ToString();
#pragma warning restore CS1690 // Possível referência nula de argumento.
                    ForCodigo_Leave(sender, e);
                    //chamada do método do txtForCod
                }
            }
        }

        private void MovimentacaoCompra_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLTipoPagamento bll = new BLLTipoPagamento(cx);

            cbTpagto.DataSource = bll.Localizar("");
            cbTpagto.DisplayMember = "tpa_nome";
            cbTpagto.ValueMember = "tpa_cod";
            cbNParcela.SelectedIndex = 0;
        }

        private void ForCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLFornecedor bll = new BLLFornecedor(cx);
                ModeloFornecedor modelo = bll.CarregaModeloFornecedor(Convert.ToInt32(txtForCodigo.Text));

                if (modelo.ForCod <= 0)
                {
                    txtForCodigo.Clear();
                    lFornecedor.Text = "Informe o código do produto ou clique em localizar";
                }
                else lFornecedor.Text = modelo.ForNome;
            }
            catch
            {
                txtForCodigo.Clear();
                lFornecedor.Text = "Informe o código do produto ou clique em localizar";
            }
        }

        private void LocProd_Click(object sender, EventArgs e)
        {
            using (ConsultaProduto f = new ConsultaProduto())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
#pragma warning disable CS1690 // Possível referência nula de argumento.
                    txtProCod.Text = f.codigo.ToString();
#pragma warning restore CS1690 // Possível referência nula de argumento.
                    ProCod_Leave(sender, e);
                    //chamada do método do txtForCod
                }
            }
        }

        private void ProCod_Leave(object sender, EventArgs e)
        {
            try
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);
                ModeloProduto modelo = bll.CarregaModeloProduto(Convert.ToInt32(txtProCod.Text));
                lProduto.Text = modelo.ProNome;

                txtQtde.Text = " 1";
                txtValor.Text = Convert.ToString((Decimal)modelo.ProValorVenda);

            }
            catch
            {
                txtProCod.Clear();
                lProduto.Text = "Informe o código do produto ou clique em localizar";
            }
        }

        private void AddProd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtProCod.Text != "") && (txtQtde.Text != "") && (txtValor.Text != ""))
                {
                    Double TotalLocal = Convert.ToDouble(txtQtde.Text) * Convert.ToDouble(txtValor.Text);
                    totalCompra += TotalLocal;

                    String[] i = new String[] { txtProCod.Text, lProduto.Text, txtQtde.Text, txtValor.Text, TotalLocal.ToString() };
                    dgvItens.Rows.Add(i);

                    txtProCod.Clear();
                    lProduto.Text = "Informe o código do produto ou clique em localizar";
                    txtQtde.Clear();
                    txtValor.Clear();

                    txtTotalCompra.Text = totalCompra.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Informe apenas números nos campos referentes ao valor e quantidade do produto.", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvItens_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtProCod.Text = dgvItens.Rows[e.RowIndex].Cells[0].Value.ToString();
                lProduto.Text = dgvItens.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtQtde.Text = dgvItens.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtValor.Text = dgvItens.Rows[e.RowIndex].Cells[3].Value.ToString();
                Double valor = Convert.ToDouble(dgvItens.Rows[e.RowIndex].Cells[4].Value);
                totalCompra = totalCompra - valor;
                dgvItens.Rows.RemoveAt(e.RowIndex);
                txtTotalCompra.Text = totalCompra.ToString();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pnFinalizaCompra.Visible = false;
        }

        private void SalvarFinal_Click(object sender, EventArgs e)
        {
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            cx.Conectar();
            cx.IniciarTransacao();

            try
            {
                //leitura dos dados
                ModeloCompra modeloCompra = new ModeloCompra
                {
                    ComData = dtDataCompra.Value,
                    ComNFiscal = Convert.ToInt32(txtNFiscal.Text),
                    ComNParcelas = Convert.ToInt32(cbNParcela.Text),
                    ComStatus = "ativa",
                    ComTotal = totalCompra,
                    ForCod = Convert.ToInt32(txtForCodigo.Text),
                    TpaCod = Convert.ToInt32(cbTpagto.SelectedValue)
                };

                //obj para gravar os dados no banco
                BLLCompra bll = new BLLCompra(cx);

                ModeloItensCompra mitens = new ModeloItensCompra();
                BLLItensCompra bitens = new BLLItensCompra(cx);

                ModeloParcelasCompra mparcelas = new ModeloParcelasCompra();
                BLLParcelasCompra bparcelas = new BLLParcelasCompra(cx);

                if (operacao == _inserir)
                {
                    //cadastrar uma compra ok
                    bll.Incluir(modeloCompra);

                    //cadastrar os itens da compra
                    for (int i = 0; i < dgvItens.RowCount; i++)
                    {
                        mitens.ItcCod = i + 1;
                        mitens.ComCod = modeloCompra.ComCod;
                        mitens.ProCod = Convert.ToInt32(dgvItens.Rows[i].Cells[0].Value);
                        mitens.ItcQtde = Convert.ToDouble(dgvItens.Rows[i].Cells[2].Value);
                        mitens.ItcValor = Convert.ToDouble(dgvItens.Rows[i].Cells[3].Value);
                        bitens.Incluir(mitens);
                        //alterar a quantidade de produtos comprados na tabela de produtos
                        //Trigger
                    }

                    // inserir os itens na tabela parcelas compra
                    for (int i = 0; i < dgvParcelas.RowCount; i++)
                    {
                        mparcelas.ComCod = modeloCompra.ComCod;
                        mparcelas.PcoCod = Convert.ToInt32(dgvParcelas.Rows[i].Cells[0].Value);
                        mparcelas.PcoValor = Convert.ToDouble(dgvParcelas.Rows[i].Cells[1].Value);
                        mparcelas.PcoDataVecto = Convert.ToDateTime(dgvParcelas.Rows[i].Cells[2].Value);
                        bparcelas.Incluir(mparcelas);
                    }

                    //cadastrar as parcelas da compra
                    MessageBox.Show($"Compra efetuada: Código {modeloCompra.ComCod}", UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //alterar uma compra
                    modeloCompra.ComCod = Convert.ToInt32(txtComCodigo.Text);
                    bll.Alterar(modeloCompra);

                    bitens.ExcluirTodosOsItens(modeloCompra.ComCod);
                    //cadastrar os itens da compra
                    for (int i = 0; i < dgvItens.RowCount; i++)
                    {
                        mitens.ItcCod = i + 1;
                        mitens.ComCod = modeloCompra.ComCod;
                        mitens.ProCod = Convert.ToInt32(dgvItens.Rows[i].Cells[0].Value);
                        mitens.ItcQtde = Convert.ToDouble(dgvItens.Rows[i].Cells[2].Value);
                        mitens.ItcValor = Convert.ToDouble(dgvItens.Rows[i].Cells[3].Value);
                        bitens.Incluir(mitens);
                        //alterar a quantidade de produtos comprados na tabela de produtos
                        //Trigger
                    }

                    bparcelas.ExcluirTodasAsParcelas(modeloCompra.ComCod);
                    // inserir os itens na tabela parcelas compra
                    for (int i = 0; i < dgvParcelas.RowCount; i++)
                    {
                        mparcelas.ComCod = modeloCompra.ComCod;
                        mparcelas.PcoCod = Convert.ToInt32(dgvParcelas.Rows[i].Cells[0].Value);
                        mparcelas.PcoValor = Convert.ToDouble(dgvParcelas.Rows[i].Cells[1].Value);
                        mparcelas.PcoDataVecto = Convert.ToDateTime(dgvParcelas.Rows[i].Cells[2].Value);
                        bparcelas.Incluir(mparcelas);
                    }

                    MessageBox.Show(UIConstants.CadastroAlterado, UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpaTela();
                pnFinalizaCompra.Visible = false;
                AlteraBotoes(1);

                cx.TerminarTransacao();
                cx.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show($"Ocorreu um erro ao salvar as alterações.\nErro: {erro.Message}", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cx.CancelarTransacao();
                cx.Desconectar();
            }
        }

        private void NFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}

