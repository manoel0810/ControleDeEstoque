using BLL;
using DAL;
using Modelo;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI
{
    public partial class MovimentacaoVenda : ModeloDeFormularioDeCadastro
    {
        public double totalVenda = 0;

        public MovimentacaoVenda()
        {
            InitializeComponent();
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            operacao = _inserir;
            totalVenda = 0;
            AlteraBotoes(2);

            cbNParcela.SelectedIndex = 0;
            cbxVendaAVista.Checked = false;
        }

        private void LocCli_Click(object sender, EventArgs e)
        {
            using (var f = new ConsultaCliente())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
#pragma warning disable CS1690
                    txtCliCodigo.Text = f.codigo.ToString();
#pragma warning restore CS1690
                    CliCodigo_Leave(sender, e);
                }
            }
        }

        private void CliCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLCliente bll = new BLLCliente(cx);
                ModeloCliente modelo = bll.CarregaModeloCliente(Convert.ToInt32(txtCliCodigo.Text));

                if (modelo.CliCod <= 0)
                {
                    txtCliCodigo.Clear();
                    lCliente.Text = "Informe o código do cliente ou clique em localizar";
                }
                else lCliente.Text = modelo.CliNome;
            }
            catch
            {
                txtCliCodigo.Clear();
                lCliente.Text = "Informe o código do cliente ou clique em localizar";
            }
        }

        private void LocProd_Click(object sender, EventArgs e)
        {
            using (ConsultaProduto f = new ConsultaProduto())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
#pragma warning disable CS1690
                    txtProCod.Text = f.codigo.ToString();
#pragma warning restore CS1690
                    ProCod_Leave(sender, e);
                }
            }
        }

        private Double VerificaQtdeProdutos(int ProCod)
        {
            Double QtdEmEstoque = 0;
            try
            {
                //O que tem no banco de dados
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);
                ModeloProduto modelo = bll.CarregaModeloProduto(ProCod);
                QtdEmEstoque = modelo.ProQtde;
                //verifica produtos na grid

                for (int i = 0; i < dgvItens.RowCount; i++)
                {
                    if (Convert.ToInt32(dgvItens.Rows[i].Cells[0].Value) == ProCod)
                        QtdEmEstoque -= Convert.ToDouble(dgvItens.Rows[i].Cells[2].Value);

                }
            }
            catch { }
            return QtdEmEstoque;
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
                txtValor.Text = modelo.ProValorVenda.ToString();
            }
            catch
            {
                txtProCod.Clear();
                lProduto.Text = "Informe o código do produto ou clique em localizar";
            }
        }

        private void AddProd_Click(object sender, EventArgs e)
        {
            Double Qtde;

            try
            {
                if ((txtProCod.Text != "") && (txtQtde.Text != "") && (txtValor.Text != ""))
                {
                    if (cbValidaQtde.Checked == true)
                    {
                        Qtde = VerificaQtdeProdutos(Convert.ToInt32(txtProCod.Text));
                        if (Convert.ToDouble(txtQtde.Text) > Qtde)
                        {
                            MessageBox.Show($"Quantidade de produtos indisponível.\nVocê possui {Qtde} unidades em estoque.", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    Double TotalLocal = Convert.ToDouble(txtQtde.Text) * Convert.ToDouble(txtValor.Text);
                    totalVenda += +TotalLocal;
                    String[] i = new String[] { txtProCod.Text, lProduto.Text, txtQtde.Text, txtValor.Text, TotalLocal.ToString() };
                    dgvItens.Rows.Add(i);

                    txtProCod.Clear();
                    lProduto.Text = "Informe o código do produto ou clique em localizar";
                    txtQtde.Clear();
                    txtValor.Clear();

                    txtTotalVenda.Text = totalVenda.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Informe apenas números nos campos referentes ao valor e quantidade do produto", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MovimentacaoVenda_Load(object sender, EventArgs e)
        {
            AlteraBotoes(1);
            DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
            BLLTipoPagamento bll = new BLLTipoPagamento(cx);

            cbTpagto.DataSource = bll.Localizar("");
            cbTpagto.DisplayMember = "tpa_nome";
            cbTpagto.ValueMember = "tpa_cod";
            cbNParcela.SelectedIndex = 0;
        }

        private void VendaAVista_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxVendaAVista.Checked)
            {
                cbNParcela.SelectedIndex = 0;
                cbNParcela.Enabled = false;
            }
            else
            {
                cbNParcela.Enabled = true;
            }
        }

        private void Itens_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtProCod.Text = dgvItens.Rows[e.RowIndex].Cells[0].Value.ToString();
                lProduto.Text = dgvItens.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtQtde.Text = dgvItens.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtValor.Text = dgvItens.Rows[e.RowIndex].Cells[3].Value.ToString();
                Double valor = Convert.ToDouble(dgvItens.Rows[e.RowIndex].Cells[4].Value);
                totalVenda -= valor;

                dgvItens.Rows.RemoveAt(e.RowIndex);
                txtTotalVenda.Text = totalVenda.ToString();
            }
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtCliCodigo.Text) <= 0)
                {
                    MessageBox.Show("Informe um código válido para o cliente", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToInt32(txtNFiscal.Text) < 0)
                {
                    MessageBox.Show("Informe um 'número válido para a nota fiscal", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (totalVenda <= 0)
                {
                    MessageBox.Show("Insira itens em sua venda para continuar...", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvParcelas.Rows.Clear();
                int parcelas = Convert.ToInt32(cbNParcela.Text);
                Double totallocal = totalVenda;
                double valor = totallocal / parcelas;

                DateTime dt = new DateTime();
                dt = dtDataini.Value;
                lbTotal.Text = totalVenda.ToString();
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
            catch
            {
                MessageBox.Show("Verifique os campos da tela de venda", UIConstants.Aviso, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarFinal_Click(object sender, EventArgs e)
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
                ModeloVenda modeloVenda = new ModeloVenda
                {
                    VenData = dtDataVenda.Value,
                    VenNFiscal = Convert.ToInt32(txtNFiscal.Text),
                    VenNParcelas = Convert.ToInt32(cbNParcela.Text),
                    VenStatus = "ativa",
                    VenTotal = totalVenda,
                    CliCod = Convert.ToInt32(txtCliCodigo.Text),
                    TpaCod = Convert.ToInt32(cbTpagto.SelectedValue)
                };

                if (cbxVendaAVista.Checked == true)
                    modeloVenda.VenAvista = 1;
                else modeloVenda.VenAvista = 0;

                //obj para gravar os dados no banco
                BLLVenda bllVenda = new BLLVenda(cx);

                ModeloItensVenda mitens = new ModeloItensVenda();
                BLLItensVenda bitens = new BLLItensVenda(cx);

                ModeloParcelasVenda mparcelas = new ModeloParcelasVenda();
                BLLParcelasVenda bparcelas = new BLLParcelasVenda(cx);

                if (operacao == _inserir)
                {
                    //cadastrar uma compra ok
                    bllVenda.Incluir(modeloVenda);

                    //cadastrar os itens da venda
                    for (int i = 0; i < dgvItens.RowCount; i++)
                    {
                        mitens.ItvCod = i + 1;
                        mitens.VenCod = modeloVenda.VenCod;
                        mitens.ProCod = Convert.ToInt32(dgvItens.Rows[i].Cells[0].Value);
                        mitens.ItvQtde = Convert.ToDouble(dgvItens.Rows[i].Cells[2].Value);
                        mitens.ItvValor = Convert.ToDouble(dgvItens.Rows[i].Cells[3].Value);
                        bitens.Incluir(mitens);
                        //alterar a quantidade de produtos vendidos na tabela de produtos
                        //Trigger
                    }

                    // inserir os itens na tabela parcelas venda
                    for (int i = 0; i < dgvParcelas.RowCount; i++)
                    {
                        mparcelas.VenCod = modeloVenda.VenCod;
                        mparcelas.PveCod = Convert.ToInt32(dgvParcelas.Rows[i].Cells[0].Value);
                        mparcelas.PveValor = Convert.ToDouble(dgvParcelas.Rows[i].Cells[1].Value);
                        mparcelas.PveDataVecto = Convert.ToDateTime(dgvParcelas.Rows[i].Cells[2].Value);
                        bparcelas.Incluir(mparcelas);
                    }

                    MessageBox.Show($"Venda efetuada: Código {modeloVenda.VenCod}", UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                pnFinalizaCompra.Visible = false;
                AlteraBotoes(1);
                cx.TerminarTransacao();
                cx.Desconectar();
                LimpaTela();

            }
            catch (Exception erro)
            {
                MessageBox.Show($"Ocorreu um erro ao salvar as alterações.\nErro: {erro.Message}", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cx.CancelarTransacao();
                cx.Desconectar();
            }
        }

        private void Localizar_Click(object sender, EventArgs e)
        {
            using (ConsultaVenda f = new ConsultaVenda())
            {
                f.ShowDialog();
                if (f.codigo != 0)
                {
                    DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                    BLLVenda bll = new BLLVenda(cx);
                    ModeloVenda modelo = bll.CarregaModeloVenda(f.codigo);

                    txtVenCodigo.Text = modelo.VenCod.ToString();
                    txtNFiscal.Text = modelo.VenNFiscal.ToString();
                    dtDataVenda.Value = modelo.VenData;
                    txtCliCodigo.Text = modelo.CliCod.ToString();
                    CliCodigo_Leave(sender, e); //para escrever o nome do fornecedor na tela
                    cbTpagto.SelectedValue = modelo.TpaCod;
                    cbNParcela.Text = modelo.VenNParcelas.ToString();
                    if (modelo.VenAvista == 1) cbxVendaAVista.Checked = true;
                    else cbxVendaAVista.Checked = false;
                    txtTotalVenda.Text = modelo.VenTotal.ToString();
                    totalVenda = modelo.VenTotal; //armazenar o valor total da compra
                                                  //itens da venda
                    BLLItensVenda bllItens = new BLLItensVenda(cx);
                    DataTable tabela = bllItens.Localizar(modelo.VenCod);
                    for (int i = 0; i < tabela.Rows.Count; i++)
                    {
                        string icod = tabela.Rows[i]["pro_cod"].ToString();
                        string inome = tabela.Rows[i]["pro_nome"].ToString();
                        string iqtd = tabela.Rows[i]["itv_qtde"].ToString();
                        string ivu = tabela.Rows[i]["itv_valor"].ToString();
                        Double TotalLocal = Convert.ToDouble(tabela.Rows[i]["itv_qtde"]) * Convert.ToDouble(tabela.Rows[i]["itv_valor"]);

                        String[] it = new String[] { icod, inome, iqtd, ivu, TotalLocal.ToString() };
                        dgvItens.Rows.Add(it);
                    }

                    AlteraBotoes(3);
                    lbMsg.Visible = false;
                    if (modelo.VenStatus != "ativa")
                    {
                        lbMsg.Visible = true;
                        btExcluir.Enabled = false;
                    }
                }
                else
                {
                    AlteraBotoes(1);
                }
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            LimpaTela();
            AlteraBotoes(1);
        }

        public void LimpaTela()
        {
            txtVenCodigo.Clear();
            txtNFiscal.Clear();
            txtCliCodigo.Clear();
            txtProCod.Clear();
            lProduto.Text = "Informe o código do produto ou clique em localizar";
            txtQtde.Clear();
            txtValor.Clear();
            txtTotalVenda.Clear();
            dgvItens.Rows.Clear();
            cbNParcela.SelectedIndex = 0;
            cbTpagto.SelectedIndex = 0;
            dgvItens.Rows.Clear();
            txtNFiscal.Clear();
            txtTotalVenda.Text = "0,00";
            lbMsg.Visible = false;

        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja cancelar o registro?", UIConstants.Prosseguir, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DALConexao cx = new DALConexao(DadosDaConexao.StringDeConexao);
                BLLVenda bll = new BLLVenda(cx);

                if (bll.CancelarVenda(Convert.ToInt32(txtVenCodigo.Text)) == true)
                    MessageBox.Show("Venda Cancelada", UIConstants.Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Não foi possível cancelar a venda.\nContate o seu desenvolvedor.", UIConstants.Erro, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
