using System;

namespace Modelo
{
    public class ModeloItensCompra
    {
        public ModeloItensCompra()
        {
            ItcCod = 0;
            ItcQtde = 0;
            ItcValor = 0;
            ProCod = 0;
            ComCod = 0;
        }

        public ModeloItensCompra(int itcCod, Double itcQtde, Double itcValor, int proCod, int comCod)
        {
            ItcCod = itcCod;
            ItcQtde = itcQtde;
            ItcValor = itcValor;
            ProCod = proCod;
            ComCod = comCod;
        }

        private int itc_cod;
        public int ItcCod
        {
            get { return itc_cod; }
            set { itc_cod = value; }
        }

        private double itc_qtde;
        public double ItcQtde
        {
            get { return itc_qtde; }
            set { itc_qtde = value; }
        }

        private double itc_valor;
        public double ItcValor
        {
            get { return itc_valor; }
            set { itc_valor = value; }
        }

        private int com_cod;
        public int ComCod
        {
            get { return com_cod; }
            set { com_cod = value; }
        }

        private int pro_cod;
        public int ProCod
        {
            get { return pro_cod; }
            set { pro_cod = value; }
        }
    }
}
