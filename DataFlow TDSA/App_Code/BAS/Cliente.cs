using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataFlow_TDSA.App_Code.Dao;

namespace DataFlow_TDSA.App_Code.BAS
{
    public class Cliente
    {
        public int CLI_ID { get; set; }
        public string CLI_NOME { get; set; }
        public DateTime CLI_DATANASCIMENTO { get; set; }
        public bool CLI_ATIVO { get; set; }

        public void TestaBanco()
        {
            DaoCliente daoCliente = new DaoCliente();
            daoCliente.TestarConexao();
        }

        public List<Cliente> CarregarClientes()
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.CarregaClientes();
        }

        public bool InsereCliente(string nome, DateTime data)
        {
            DaoCliente daoCliente = new DaoCliente();
            return daoCliente.InsereCliente(nome, data);
        }

        public void DeletarCliente(int id)
        {
            DaoCliente daoCliente = new DaoCliente();
            daoCliente.DeletaCliente(id);
        }

        public void EditarCliente(int id, string nome, DateTime data, bool ativo)
        {
            DaoCliente daoCliente = new DaoCliente();
            daoCliente.EditaCliente(id, nome, data, ativo);
        }
    }
}