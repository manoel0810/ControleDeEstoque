using System;
using System.Data.SqlClient;

namespace DAL
{
    public class DALConexao
    {
        private String _stringConexao;
        private SqlConnection _conexao;
        private SqlTransaction _transaction;

        public DALConexao(String dadosConexao)
        {
            _conexao = new SqlConnection();
            StringConexao = dadosConexao;
            _conexao.ConnectionString = dadosConexao;
        }

        public SqlTransaction ObjetoTransacao
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public void IniciarTransacao()
        {
            _transaction = _conexao.BeginTransaction();
        }

        public void TerminarTransacao()
        {
            _transaction.Commit();
        }

        //desfaz tudo o que fez
        public void CancelarTransacao()
        {
            _transaction.Rollback();
        }

        public String StringConexao
        {
            get { return _stringConexao; }
            set { _stringConexao = value; }
        }

        public SqlConnection ObjetoConexao
        {
            get { return _conexao; }
            set { _conexao = value; }

        }

        public void Conectar()
        {
            _conexao.Open();
        }

        public void Desconectar()
        {
            _conexao.Close();
        }
    }
}
