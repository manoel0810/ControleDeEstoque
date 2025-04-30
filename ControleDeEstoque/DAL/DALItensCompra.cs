using Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALItensCompra
    {
        private readonly DALConexao conexao;

        public DALItensCompra(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloItensCompra modelo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                Transaction = conexao.ObjetoTransacao,
                CommandText = "insert into itenscompra(itc_cod,itc_qtde,itc_valor,com_cod,pro_cod) values (@itc_cod,@itc_qtde,@itc_valor,@com_cod,@pro_cod);"
            };

            cmd.Parameters.AddWithValue("@itc_cod", modelo.ItcCod);
            cmd.Parameters.AddWithValue("@itc_qtde", modelo.ItcQtde);
            cmd.Parameters.AddWithValue("@itc_valor", modelo.ItcValor);
            cmd.Parameters.AddWithValue("@com_cod", modelo.ComCod);
            cmd.Parameters.AddWithValue("@pro_cod", modelo.ProCod);

            //conexao.Conectar();
            cmd.ExecuteNonQuery();
            //conexao.Desconectar();
        }
        public void Alterar(ModeloItensCompra modelo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                Transaction = conexao.ObjetoTransacao,
                CommandText = "update itenscompra set itc_qtde = @itc_qtde, itc_valor= @itc_valor " +
                    "where itc_cod = @itc_cod and com_cod = @com_cod and pro_cod = @pro_cod;"
            };

            cmd.Parameters.AddWithValue("@itc_cod", modelo.ItcCod);
            cmd.Parameters.AddWithValue("@itc_qtde", modelo.ItcQtde);
            cmd.Parameters.AddWithValue("@itc_valor", modelo.ItcValor);
            cmd.Parameters.AddWithValue("@com_cod", modelo.ComCod);
            cmd.Parameters.AddWithValue("@pro_cod", modelo.ProCod);

            //conexao.Conectar();
            cmd.ExecuteNonQuery();
            //conexao.Desconectar();
        }
        public void Excluir(ModeloItensCompra modelo)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                Transaction = conexao.ObjetoTransacao,
                CommandText = "delete from itenscompra " +
                    "where itc_cod = @itc_cod and com_cod = @com_cod and pro_cod = @pro_cod;"
            };

            cmd.Parameters.AddWithValue("@itc_cod", modelo.ItcCod);
            cmd.Parameters.AddWithValue("@com_cod", modelo.ComCod);
            cmd.Parameters.AddWithValue("@pro_cod", modelo.ProCod);

            //conexao.Conectar();
            cmd.ExecuteNonQuery();
            //conexao.Desconectar();
        }

        public void ExcluirTodosOsItens(int ComCod)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                Transaction = conexao.ObjetoTransacao,
                CommandText = "delete from itenscompra " +
                    "where com_cod = @com_cod;"
            };

            cmd.Parameters.AddWithValue("@com_cod", ComCod);

            //conexao.Conectar();
            cmd.ExecuteNonQuery();
            //conexao.Desconectar();
        }

        public DataTable Localizar(int comcod)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select i.com_cod, i.itc_cod, i.pro_cod, p.pro_nome, i.itc_qtde, i.itc_valor  from itenscompra i inner join produto p on p.pro_cod = i.pro_cod where i.com_cod =" +
                comcod.ToString(), conexao.StringConexao);

            da.Fill(tabela);
            return tabela;
        }

        public ModeloItensCompra CarregaModeloItensCompra(int ItcCod, int ComCod, int ProCod)
        {
            ModeloItensCompra modelo = new ModeloItensCompra();
            SqlCommand cmd = new SqlCommand
            {
                Connection = conexao.ObjetoConexao,
                CommandText = "select * from itenscompra where itc_cod = @itc_cod and com_cod = @com_cod and pro_cod = @pro_cod;"
            };

            cmd.Parameters.AddWithValue("@itc_cod", ItcCod);
            cmd.Parameters.AddWithValue("@com_cod", ComCod);
            cmd.Parameters.AddWithValue("@pro_cod", ProCod);
            conexao.Conectar();

            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.ItcCod = ItcCod;
                modelo.ProCod = ProCod;
                modelo.ComCod = ComCod;
                modelo.ItcQtde = Convert.ToDouble(registro["itc_qtde"]);
                modelo.ItcValor = Convert.ToDouble(registro["itc_valor"]);
            }

            registro.Close();
            conexao.Desconectar();
            return modelo;
        }
    }
}
