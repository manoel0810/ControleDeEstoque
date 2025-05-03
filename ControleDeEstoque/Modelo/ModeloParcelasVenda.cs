using System;

namespace Modelo
{
    public class ModeloParcelasVenda
    {
        public ModeloParcelasVenda()
        {
            PveCod = 0;
            PveValor = 0;
            VenCod = 0;
            PveDataPagto = DateTime.Now;
            PveDataVecto = DateTime.Now;
        }

        private int pve_cod;
        public int PveCod
        {
            get { return pve_cod; }
            set { pve_cod = value; }
        }

        private int ven_cod;
        public int VenCod
        {
            get { return ven_cod; }
            set { ven_cod = value; }
        }

        private Double pve_valor;
        public Double PveValor
        {
            get { return pve_valor; }
            set { pve_valor = value; }
        }

        private DateTime pve_datapagto;
        public DateTime PveDataPagto
        {
            get { return pve_datapagto; }
            set { pve_datapagto = value; }
        }

        private DateTime pve_datavecto;
        public DateTime PveDataVecto
        {
            get { return pve_datavecto; }
            set { pve_datavecto = value; }
        }
    }
}
