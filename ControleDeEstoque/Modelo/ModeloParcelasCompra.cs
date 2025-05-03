using System;

namespace Modelo
{
    public class ModeloParcelasCompra
    {
        public ModeloParcelasCompra()
        {
            PcoCod = 0;
            PcoValor = 0;
            ComCod = 0;
            PcoDataVecto = DateTime.Now;
        }

        private int pco_cod;
        public int PcoCod
        {
            get { return pco_cod; }
            set { pco_cod = value; }
        }

        private int com_cod;
        public int ComCod
        {
            get { return com_cod; }
            set { com_cod = value; }
        }

        private Double pco_valor;
        public Double PcoValor
        {
            get { return pco_valor; }
            set { pco_valor = value; }
        }

        private DateTime pco_datapagto;
        public DateTime PcoDataPagto
        {
            get { return pco_datapagto; }
            set { pco_datapagto = value; }
        }

        private DateTime pco_datavecto;
        public DateTime PcoDataVecto
        {
            get { return pco_datavecto; }
            set { pco_datavecto = value; }
        }

    }
}
