using System;

namespace Modelo
{
    public class ModeloCategoria
    {
        public ModeloCategoria()
        {
            CatCod = 0;
            CatNome = "";
        }

        public ModeloCategoria(int catcod, String nome)
        {
            CatCod = catcod;
            CatNome = nome;
        }

        private int cat_cod;
        public int CatCod
        {
            get { return cat_cod; }
            set { cat_cod = value; }
        }

        private String cat_nome;
        public String CatNome
        {
            get { return cat_nome; }
            set { cat_nome = value; }
        }
    }
}
