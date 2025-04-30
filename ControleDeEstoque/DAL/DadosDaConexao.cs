using System;

namespace DAL
{
    public class DadosDaConexao
    {
        public static String servidor = "(localdb)\\mssqllocaldb";
        public static String banco = "EstoqueApp";
        public static String usuario = "sa";
        public static String senha = "123456";

        public static String StringDeConexao
        {
            get
            {
                return $"Server={servidor};Database={banco};Trusted_Connection=True;MultipleActiveResultSets=true";
            }
        }
    }
}
