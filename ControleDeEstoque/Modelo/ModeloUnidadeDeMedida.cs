using System;

namespace Modelo
{
    public class ModeloUnidadeDeMedida
    {
        public ModeloUnidadeDeMedida()
        {
            UmedCod = 0;
            UmedNome = "";
        }

        public ModeloUnidadeDeMedida(int cod, String nome)
        {
            UmedCod = cod;
            UmedNome = nome;
        }

        private int umed_cod;
        public int UmedCod
        {
            get
            {
                return umed_cod;
            }
            set
            {
                umed_cod = value;
            }
        }
        private String umed_nome;
        public String UmedNome
        {
            get { return umed_nome; }
            set { umed_nome = value; }
        }
    }
}
