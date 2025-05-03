using System;
using System.IO;

namespace Modelo
{
    public class ModeloProduto
    {
        public ModeloProduto()
        {
            ProCod = 0;
            ProNome = "";
            ProDescricao = "";
            ProValorPago = 0;
            ProValorVenda = 0;
            ProQtde = 0;
            UmedCod = 0;
            CatCod = 0;
            ScatCod = 0;
        }

        public ModeloProduto(int pro_cod, String pro_nome, String pro_descricao,
            String pro_foto, Double pro_valorpago, Double pro_valorvenda, Double pro_qtde,
            int umed_cod, int cat_cod, int scat_cod)
        {
            ProCod = pro_cod;
            ProNome = pro_nome;
            ProDescricao = pro_descricao;
            CarregaImagem(pro_foto);
            ProValorPago = pro_valorpago;
            ProValorVenda = pro_valorvenda;
            ProQtde = pro_qtde;
            UmedCod = umed_cod;
            CatCod = cat_cod;
            ScatCod = scat_cod;
        }

        public ModeloProduto(int pro_cod, String pro_nome, String pro_descricao,
            Byte[] pro_foto, Double pro_valorpago, Double pro_valorvenda, float pro_qtde,
            int umed_cod, int cat_cod, int scat_cod)
        {
            ProCod = pro_cod;
            ProNome = pro_nome;
            ProDescricao = pro_descricao;
            ProFoto = pro_foto;
            ProValorPago = pro_valorpago;
            ProValorVenda = pro_valorvenda;
            ProQtde = pro_qtde;
            UmedCod = umed_cod;
            CatCod = cat_cod;
            ScatCod = scat_cod;
        }

        private int _pro_cod;
        public int ProCod
        {
            get
            {
                return _pro_cod;
            }
            set
            {
                _pro_cod = value;
            }
        }

        private String _pro_nome;
        public String ProNome
        {
            get
            {
                return _pro_nome;
            }
            set
            {
                _pro_nome = value;
            }
        }

        private String _pro_descricao;
        public String ProDescricao
        {
            get
            {
                return _pro_descricao;
            }
            set
            {
                _pro_descricao = value;
            }
        }

        private byte[] _pro_foto;
        public byte[] ProFoto
        {
            get { return _pro_foto; }
            set { _pro_foto = value; }
        }

        public void CarregaImagem(String imgCaminho)
        {
            try
            {
                if (string.IsNullOrEmpty(imgCaminho))
                    return;

                FileInfo arqImagem = new FileInfo(imgCaminho);
                FileStream fs = new FileStream(imgCaminho, FileMode.Open, FileAccess.Read, FileShare.Read);
                ProFoto = new byte[Convert.ToInt32(arqImagem.Length)];
                int iBytesRead = fs.Read(ProFoto, 0, Convert.ToInt32(arqImagem.Length));
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private Double _pro_valorpago;
        public Double ProValorPago
        {
            get
            {
                return _pro_valorpago;
            }
            set
            {
                _pro_valorpago = value;
            }
        }

        private Double _pro_valorvenda;
        public Double ProValorVenda
        {
            get
            {
                return _pro_valorvenda;
            }
            set
            {
                _pro_valorvenda = value;
            }
        }

        private Double _pro_qtde;
        public Double ProQtde
        {
            get
            {
                return _pro_qtde;
            }
            set
            {
                _pro_qtde = value;
            }
        }

        private int _umed_cod;
        public int UmedCod
        {
            get
            {
                return _umed_cod;
            }
            set
            {
                _umed_cod = value;
            }
        }

        private int _cat_cod;
        public int CatCod
        {
            get
            {
                return _cat_cod;
            }
            set
            {
                _cat_cod = value;
            }
        }

        private int _scat_cod;
        public int ScatCod
        {
            get
            {
                return _scat_cod;
            }
            set
            {
                _scat_cod = value;
            }
        }
    }
}
