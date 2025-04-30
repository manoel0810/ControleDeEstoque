using DAL;
using Modelo;
using System;
using System.Data;

namespace BBL
{
    public class BLLCliente2
    {
        private readonly DALConexao conexao;

        public BLLCliente2(DALConexao cx)
        {
            conexao = cx;
        }

        public void Incluir(ModeloCliente modelo)
        {
            if (modelo.CliNome.Trim().Length == 0)
                throw new Exception("O nome do cliente é obrigatório");


            if (modelo.CliCpfCnpj.Trim().Length == 0)
                throw new Exception("O CPF/CNPJ do cliente é obrigatório");


            //verificar CPF/CNPJ
            if (modelo.CliRgIe.Trim().Length == 0)
                throw new Exception("O RG/IE do cliente é obrigatório");


            if (modelo.CliFone.Trim().Length == 0)
                throw new Exception("O telefone do cliente é obrigatório");


            /*
            if(modelo.CliTipo < 0 || modelo.CliTipo > 1) // 0 = fisica ; 1 = juridica
            {  
                throw new Exception("Desenvolvedor defina o tipo do cliente corretamente");
            }
            */

            DALCliente DALobj = new DALCliente(conexao);
            DALobj.Incluir(modelo);
        }

        public void Alterar(ModeloCliente modelo)
        {
            if (modelo.CliNome.Trim().Length == 0)
                throw new Exception("O nome do cliente é obrigatório");


            if (modelo.CliCpfCnpj.Trim().Length == 0)
                throw new Exception("O CPF/CNPJ do cliente é obrigatório");


            //verificar CPF/CNPJ

            if (modelo.CliRgIe.Trim().Length == 0)
                throw new Exception("O RG/IE do cliente é obrigatório");


            if (modelo.CliFone.Trim().Length == 0)
                throw new Exception("O telefone do cliente é obrigatório");

            /*
            if(modelo.CliTipo < 0 || modelo.CliTipo > 1) // 0 = fisica ; 1 = juridica
            {  
                throw new Exception("Desenvolvedor defina o tipo do cliente corretamente");
            }
            */

            DALCliente DALobj = new DALCliente(conexao);
            DALobj.Alterar(modelo);
        }

        public void Excluir(int codigo)
        {
            DALCliente DALobj = new DALCliente(conexao);
            DALobj.Excluir(codigo);
        }

        public DataTable Localizar(String valor)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.Localizar(valor);
        }

        public ModeloCliente CarregaModeloCliente(int codigo)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.CarregaModeloCliente(codigo);
        }

        public ModeloCliente CarregaModeloCliente(string cpfcnpj)
        {
            DALCliente DALobj = new DALCliente(conexao);
            return DALobj.CarregaModeloCliente(cpfcnpj);
        }
    }
}
