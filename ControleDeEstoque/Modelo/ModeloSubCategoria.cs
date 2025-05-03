using System;

namespace Modelo
{
    public class ModeloSubCategoria
    {
        public ModeloSubCategoria()
        {
            CatCod = 0;
            ScatCod = 0;
            ScatNome = "";
        }

        public ModeloSubCategoria(int scatcod, int catcod, String snome)
        {
            CatCod = catcod;
            ScatCod = scatcod;
            ScatNome = snome;
        }

        private int scat_cod;
        public int ScatCod
        {
            get { return scat_cod; }
            set { scat_cod = value; }
        }

        private int cat_cod;
        public int CatCod
        {
            get { return cat_cod; }
            set { cat_cod = value; }
        }

        private String scat_nome;
        public String ScatNome
        {
            get { return scat_nome; }
            set { scat_nome = value; }
        }

    }
}
