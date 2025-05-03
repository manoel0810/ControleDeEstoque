using System;

namespace Modelo
{
    public class ModeloTipoPagamento
    {
        public ModeloTipoPagamento()
        {
            TpaCod = 0;
            TpaNome = "";
        }

        public ModeloTipoPagamento(int catcod, String nome)
        {
            TpaCod = catcod;
            TpaNome = nome;
        }

        private int tpa_cod;
        public int TpaCod
        {
            get { return tpa_cod; }
            set { tpa_cod = value; }
        }

        private String tpa_nome;
        public String TpaNome
        {
            get { return tpa_nome; }
            set { tpa_nome = value; }
        }
    }
}
