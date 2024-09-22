using DataFlow_TDSA.App_Code.BAS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataFlow_TDSA.App_Code.Dao
{
    public class DaoCliente
    {
        string connString = ConfigurationManager.ConnectionStrings["DBTDSA"].ConnectionString;

        public void TestarConexao()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexão bem-sucedida!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar: " + ex.Message);
                }
            }
        }

        public List<Cliente> CarregaClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open(); // Abre a conexão

                    string query = "SELECT CLI_ID, CLI_NOME, CLI_DATANASCIMENTO, CLI_ATIVO FROM CLIENTE";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaClientes.Add(new Cliente{
                                    CLI_ID = reader.GetInt32(reader.GetOrdinal("CLI_ID")),
                                    CLI_NOME = reader.GetString(reader.GetOrdinal("CLI_NOME")).ToString(),
                                    CLI_DATANASCIMENTO = reader.GetDateTime(reader.GetOrdinal("CLI_DATANASCIMENTO")),
                                    CLI_ATIVO = reader.GetBoolean(reader.GetOrdinal("CLI_ATIVO"))
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }

            return listaClientes;
        }

        public bool InsereCliente(string nome, DateTime data)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO CLIENTE (CLI_NOME, CLI_DATANASCIMENTO, CLI_ATIVO) VALUES (@Nome, @Data, @Ativo)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", nome);
                        command.Parameters.AddWithValue("@Data", data);
                        command.Parameters.AddWithValue("@Ativo", 1);

                        int rowsAffected = command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                    return false;
                }
            }
        }

        public void DeletaCliente(int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string query = $"DELETE FROM CLIENTE WHERE CLI_ID = {id}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }
        }

        public void EditaCliente(int id, string nome, DateTime data, bool ativo)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE CLIENTE SET CLI_NOME = @Nome, CLI_DATANASCIMENTO = @Data, CLI_ATIVO = @Ativo WHERE CLI_ID = @ID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Nome", nome);
                        command.Parameters.AddWithValue("@Data", data);
                        command.Parameters.AddWithValue("@Ativo", ativo);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} linha(s) atualizada(s).");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }
        }
    }
}