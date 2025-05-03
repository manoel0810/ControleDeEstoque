using System;

namespace Modelo
{
    public class ModeloVenda
    {
        public ModeloVenda()
        {
            VenCod = 0;
            VenData = DateTime.Now;
            VenNFiscal = 0;
            VenTotal = 0;
            VenNParcelas = 0;
            VenStatus = "Válida";
            CliCod = 0;
            TpaCod = 0;
            VenAvista = 1;
        }

        public ModeloVenda(int venCod, DateTime data, int nFiscal, double total,
            int nParcelas, String status, int cliCod, int tpaCod, int avista)
        {
            VenCod = venCod;
            VenData = data;
            VenNFiscal = nFiscal;
            VenTotal = total;
            VenNParcelas = nParcelas;
            VenStatus = status;
            CliCod = cliCod;
            TpaCod = tpaCod;
            VenAvista = VenAvista;
        }

        private int _ven_cod;
        public int VenCod
        {
            get
            {
                return _ven_cod;
            }
            set
            {
                _ven_cod = value;
            }
        }

        private DateTime _ven_data;
        public DateTime VenData
        {
            get { return _ven_data; }
            set { _ven_data = value; }
        }

        private int _ven_nfiscal;
        public int VenNFiscal
        {
            get { return _ven_nfiscal; }
            set { _ven_nfiscal = value; }
        }

        private Double _ven_total;
        public Double VenTotal
        {
            get { return _ven_total; }
            set { _ven_total = value; }
        }

        private int _ven_nparcelas;
        public int VenNParcelas
        {
            get { return _ven_nparcelas; }
            set { _ven_nparcelas = value; }
        }

        private String _ven_status;
        public String VenStatus
        {
            get { return _ven_status; }
            set { _ven_status = value; }

        }

        private int _clicod;
        public int CliCod
        {
            get { return _clicod; }
            set { _clicod = value; }
        }

        private int _tpa_cod;
        public int TpaCod
        {
            get { return _tpa_cod; }
            set { _tpa_cod = value; }
        }

        private int _ven_avista;
        public int VenAvista
        {
            get { return _ven_avista; }
            set { _ven_avista = value; }
        }
    }
}
