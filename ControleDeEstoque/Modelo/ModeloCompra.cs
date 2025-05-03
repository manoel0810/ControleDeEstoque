using System;

namespace Modelo
{
    public class ModeloCompra
    {
        public ModeloCompra()
        {
            ComCod = 0;
            ComData = DateTime.Now;
            ComNFiscal = 0;
            ComTotal = 0;
            ComNParcelas = 0;
            ComStatus = "Válida";
            ForCod = 0;
            TpaCod = 0;
        }

        public ModeloCompra(int comCod, DateTime data, int nFiscal, double total,
            int nParcelas, String status, int forCod, int tpaCod)
        {
            ComCod = comCod;
            ComData = data;
            ComNFiscal = nFiscal;
            ComTotal = total;
            ComNParcelas = nParcelas;
            ComStatus = status;
            ForCod = forCod;
            TpaCod = tpaCod;
        }

        private int _com_cod;
        public int ComCod
        {
            get
            {
                return _com_cod;
            }
            set
            {
                _com_cod = value;
            }
        }

        private DateTime _com_data;
        public DateTime ComData
        {
            get { return _com_data; }
            set { _com_data = value; }
        }

        private int _com_nfiscal;
        public int ComNFiscal
        {
            get { return _com_nfiscal; }
            set { _com_nfiscal = value; }
        }

        private Double _com_total;
        public Double ComTotal
        {
            get { return _com_total; }
            set { _com_total = value; }
        }

        private int _com_nparcelas;
        public int ComNParcelas
        {
            get { return _com_nparcelas; }
            set { _com_nparcelas = value; }
        }

        private String _com_status;
        public String ComStatus
        {
            get { return _com_status; }
            set { _com_status = value; }

        }

        private int _for_cod;
        public int ForCod
        {
            get { return _for_cod; }
            set { _for_cod = value; }
        }

        private int _tpa_cod;
        public int TpaCod
        {
            get { return _tpa_cod; }
            set { _tpa_cod = value; }
        }
    }
}
