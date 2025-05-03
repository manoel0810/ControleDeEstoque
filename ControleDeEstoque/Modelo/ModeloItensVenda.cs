using System;

namespace Modelo
{
    public class ModeloItensVenda
    {
        public ModeloItensVenda()
        {
            ItvCod = 0;
            ItvQtde = 0;
            ItvValor = 0;
            ProCod = 0;
            VenCod = 0;
        }

        public ModeloItensVenda(int itvCod, Double itvQtde, Double itvValor, int proCod, int venCod)
        {
            ItvCod = itvCod;
            ItvQtde = itvQtde;
            ItvValor = itvValor;
            ProCod = proCod;
            VenCod = venCod;
        }

        private int itv_cod;
        public int ItvCod
        {
            get { return itv_cod; }
            set { itv_cod = value; }
        }

        private double itv_qtde;
        public double ItvQtde
        {
            get { return itv_qtde; }
            set { itv_qtde = value; }
        }

        private double itv_valor;
        public double ItvValor
        {
            get { return itv_valor; }
            set { itv_valor = value; }
        }

        private int ven_cod;
        public int VenCod
        {
            get { return ven_cod; }
            set { ven_cod = value; }
        }

        private int pro_cod;
        public int ProCod
        {
            get { return pro_cod; }
            set { pro_cod = value; }
        }
    }
}
