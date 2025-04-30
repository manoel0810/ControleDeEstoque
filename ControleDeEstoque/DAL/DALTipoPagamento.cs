using Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALTipoPagamento
    {
        private readonly DALConexao conexao;

        public DALTipoPagamento(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloTipoPagamento modelo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                CommandText = "insert into tipopagamento(tpa_nome) values (@nome); select @@IDENTITY;"
            };

            cmd.Parameters.AddWithValue("@nome", modelo.TpaNome);
            conexao.Conectar();

            modelo.TpaCod = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconectar();
        }

        public void Alterar(ModeloTipoPagamento modelo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                CommandText = "update tipopagamento set tpa_nome = @nome where tpa_cod = @codigo;"
            };

            cmd.Parameters.AddWithValue("@nome", modelo.TpaNome);
            cmd.Parameters.AddWithValue("@codigo", modelo.TpaCod);

            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                CommandText = "delete from tipopagamento where tpa_cod = @codigo;"
            };

            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();

            cmd.ExecuteNonQuery();
            conexao.Desconectar();
        }

        public DataTable Localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tipopagamento where tpa_nome like '%" +
                valor + "%'", conexao.StringConexao);

            da.Fill(tabela);
            return tabela;
        }

        public ModeloTipoPagamento CarregaModeloTipoPagamento(int codigo)
        {
            ModeloTipoPagamento modelo = new ModeloTipoPagamento();
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                CommandText = "select * from tipopagamento where tpa_cod = @codigo"
            };

            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();

            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.TpaCod = Convert.ToInt32(registro["tpa_cod"]);
                modelo.TpaNome = Convert.ToString(registro["tpa_nome"]);
            }

            registro.Close();
            conexao.Desconectar();
            return modelo;
        }
    }
}
