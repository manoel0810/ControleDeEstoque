namespace Modelo
{
    public class ModeloFornecedor
    {
        //construtores
        public ModeloFornecedor()
        {
            ForCod = 0;
            ForNome = "";
            ForCnpj = "";
            ForIe = "";
            ForRSocial = "";
            ForCep = "";
            ForEndereco = "";
            ForBairro = "";
            ForFone = "";
            ForCelular = "";
            ForEmail = "";
            ForEndNumero = "";
            ForCidade = "";
            ForEstado = "";
        }

        public ModeloFornecedor(int cod, string nome, string cpfcnpj, string rgie,
            string rsocial, string cep, string endereco, string bairro,
            string fone, string celular, string email, string endnumero,
            string cidade, string estado)
        {
            ForCod = cod;
            ForNome = nome;
            ForCnpj = cpfcnpj;
            ForIe = rgie;
            ForRSocial = rsocial;
            ForCep = cep;
            ForEndereco = endereco;
            ForBairro = bairro;
            ForFone = fone;
            ForCelular = celular;
            ForEmail = email;
            ForEndNumero = endnumero;
            ForCidade = cidade;
            ForEstado = estado;
        }

        //propriedades da  classe
        private int For_cod;
        public int ForCod
        {
            get { return For_cod; }
            set { For_cod = value; }
        }

        private string For_nome;
        public string ForNome
        {
            get { return For_nome; }
            set { For_nome = value; }
        }

        private string For_cnpj;
        public string ForCnpj
        {
            get { return For_cnpj; }
            set { For_cnpj = value; }
        }

        private string For_Ie;
        public string ForIe
        {
            get { return For_Ie; }
            set { For_Ie = value; }
        }

        private string For_RSocial;
        public string ForRSocial
        {
            get { return For_RSocial; }
            set { For_RSocial = value; }
        }

        private string For_cep;
        public string ForCep
        {
            get { return For_cep; }
            set { For_cep = value; }
        }

        private string For_endereco;
        public string ForEndereco
        {
            get { return For_endereco; }
            set { For_endereco = value; }
        }

        private string For_bairro;
        public string ForBairro
        {
            get { return For_bairro; }
            set { For_bairro = value; }
        }

        private string For_fone;
        public string ForFone
        {
            get { return For_fone; }
            set { For_fone = value; }
        }

        private string For_cel;
        public string ForCelular
        {
            get { return For_cel; }
            set { For_cel = value; }
        }

        private string For_email;
        public string ForEmail
        {
            get { return For_email; }
            set { For_email = value; }
        }

        private string For_endnumero;
        public string ForEndNumero
        {
            get { return For_endnumero; }
            set { For_endnumero = value; }
        }

        private string For_cidade;
        public string ForCidade
        {
            get { return For_cidade; }
            set { For_cidade = value; }
        }

        private string For_estado;
        public string ForEstado
        {
            get { return For_estado; }
            set { For_estado = value; }
        }
    }

}
