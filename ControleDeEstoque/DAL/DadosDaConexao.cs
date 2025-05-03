using System;

namespace DAL
{
    public class DadosDaConexao
    {
        public static String servidor = "(localdb)\\mssqllocaldb";
        public static String banco = "EstoqueApp";
        public static String usuario = "sa";
        public static String senha = "123456";
        public static bool autenticacaoWindows = true;

        public static String StringDeConexao
        {
            get
            {
                return autenticacaoWindows
                    ? $"Data Source={servidor};Initial Catalog={banco};Integrated Security=True;Pooling=False"
                    : $"Data Source={servidor};Initial Catalog={banco};User Id={usuario};Password={senha};Pooling=False";
            }
        }
    }
}
