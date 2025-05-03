namespace Modelo
{
    public class ModeloCliente  //clitipo 0->fisica 1->juridica
    {
        //construtores
        public ModeloCliente()
        {
            CliCod = 0;
            CliNome = "";
            CliCpfCnpj = "";
            CliRgIe = "";
            CliRSocial = "";
            CliTipo = "Física";
            CliCep = "";
            CliEndereco = "";
            CliBairro = "";
            CliFone = "";
            CliCelular = "";
            CliEmail = "";
            CliEndNumero = "";
            CliCidade = "";
            CliEstado = "";
        }

        public ModeloCliente(int cod, string nome, string cpfcnpj, string rgie,
            string rsocial, string tipo, string cep, string endereco, string bairro,
            string fone, string celular, string email, string endnumero,
            string cidade, string estado)
        {
            CliCod = cod;
            CliNome = nome;
            CliCpfCnpj = cpfcnpj;
            CliRgIe = rgie;
            CliRSocial = rsocial;
            CliTipo = tipo;
            CliCep = cep;
            CliEndereco = endereco;
            CliBairro = bairro;
            CliFone = fone;
            CliCelular = celular;
            CliEmail = email;
            CliEndNumero = endnumero;
            CliCidade = cidade;
            CliEstado = estado;
        }

        //propriedades da  classe
        private int cli_cod;
        public int CliCod
        {
            get { return cli_cod; }
            set { cli_cod = value; }
        }

        private string cli_nome;
        public string CliNome
        {
            get { return cli_nome; }
            set { cli_nome = value; }
        }

        private string cli_cpfcnpj;
        public string CliCpfCnpj
        {
            get { return cli_cpfcnpj; }
            set { cli_cpfcnpj = value; }
        }

        private string cli_RgIe;
        public string CliRgIe
        {
            get { return cli_RgIe; }
            set { cli_RgIe = value; }
        }

        private string cli_RSocial;
        public string CliRSocial
        {
            get { return cli_RSocial; }
            set { cli_RSocial = value; }
        }

        private string cli_tipo;
        public string CliTipo
        {
            get { return cli_tipo; }
            set { cli_tipo = value; }
        }

        private string cli_cep;
        public string CliCep
        {
            get { return cli_cep; }
            set { cli_cep = value; }
        }

        private string cli_endereco;
        public string CliEndereco
        {
            get { return cli_endereco; }
            set { cli_endereco = value; }
        }

        private string cli_bairro;
        public string CliBairro
        {
            get { return cli_bairro; }
            set { cli_bairro = value; }
        }

        private string cli_fone;
        public string CliFone
        {
            get { return cli_fone; }
            set { cli_fone = value; }
        }

        private string cli_cel;
        public string CliCelular
        {
            get { return cli_cel; }
            set { cli_cel = value; }
        }

        private string cli_email;
        public string CliEmail
        {
            get { return cli_email; }
            set { cli_email = value; }
        }

        private string cli_endnumero;
        public string CliEndNumero
        {
            get { return cli_endnumero; }
            set { cli_endnumero = value; }
        }

        private string cli_cidade;
        public string CliCidade
        {
            get { return cli_cidade; }
            set { cli_cidade = value; }
        }

        private string cli_estado;
        public string CliEstado
        {
            get { return cli_estado; }
            set { cli_estado = value; }
        }
    }
}
