/*
string connectionString = "your_connection_string_here"; // Substitua isso com sua string de conexão
string connectionString = "Data Source=LAPTOP-BHR7AFH7\\SQLEXPRESS; Initial Catalog=ControleDeEstoque; Integrated Security=True"; 
string connectionString = @"Data Source=DESKTOP-D2KELDD\DEV2019; Initial Catalog=lab; Integrated Security=True"; 

CREATE TABLE [Produtos]
( 
	Id								int  NOT NULL  IDENTITY ( 1,1 ) ,
	[Nome]            varchar(30) ,
	[Preco]           decimal(10,2)  NULL 
)

SET IDENTITY_INSERT Produtos OFF

 */
using System;
using System.Data.SqlClient;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = @"Data Source=DESKTOP-D2KELDD\DEV2019; Initial Catalog=lab; Integrated Security=True"; // Substitua isso com sua string de conexão

        // Exemplo de operações CRUD
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // READ
            Console.WriteLine("Limpando tabela...");
            TruncateProducts(connection);

            // CREATE
            //Console.WriteLine("Adicionando novo produto...");
            //string opcao = Console.ReadLine();

            InsertProduct(connection, "Novo Produto", 10.99m);
            //InsertProductId(connection, 1, "Novo Produto", 10.99m);

            Console.WriteLine("novo produto adicionado...");
            string opcao1 = Console.ReadLine();

            // READ
            Console.WriteLine("Listando todos os produtos...");
            ListProducts(connection);

            // UPDATE
            Console.WriteLine("Atualizando o preço do produto...");
            UpdateProduct(connection, 1, 19.99m);

            // READ (novamente após a atualização)
            Console.WriteLine("Listando todos os produtos após a atualização...");
            ListProducts(connection);

            Console.WriteLine("Listado todos os produtos após a atualização");
            string opcao2 = Console.ReadLine();


            // DELETE
            Console.WriteLine("Excluindo o produto...");
            DeleteProduct(connection, 1);

            // READ (novamente após a exclusão)
            Console.WriteLine("Listando todos os produtos após a exclusão...");
            ListProducts(connection);

            Console.WriteLine("Apos exclusao");
            string opcao3 = Console.ReadLine();

            connection.Close();
        }
    }

    static void TruncateProducts(SqlConnection connection)
    {
        string query = "TRUNCATE TABLE Produtos";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        reader.Close();
    }

    static void InsertProduct(SqlConnection connection, string name, decimal price)
    {
        string query = "INSERT INTO Produtos (Nome, Preco) VALUES (@Nome, @Preco)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nome", name);
        command.Parameters.AddWithValue("@Preco", price);
        command.ExecuteNonQuery();
    }

    /*
    static void InsertProductId(SqlConnection connection, int id, string name, decimal price)
    {
        string query = "INSERT INTO Produtos (Id, Nome, Preco) VALUES (@Id, @Nome, @Preco)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Nome", name);
        command.Parameters.AddWithValue("@Preco", price);
        command.ExecuteNonQuery();
    }
    */

    static void ListProducts(SqlConnection connection)
    {
        string query = "SELECT * FROM Produtos";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["ID"]}, Nome: {reader["Nome"]}, Preço: {reader["Preco"]}");
        }

        reader.Close();
    }

    static void UpdateProduct(SqlConnection connection, int id, decimal newPrice)
    {
        string query = "UPDATE Produtos SET Preco = @Preco WHERE ID = @ID";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", id);
        command.Parameters.AddWithValue("@Preco", newPrice);
        command.ExecuteNonQuery();
    }

    static void DeleteProduct(SqlConnection connection, int id)
    {
        string query = "DELETE FROM Produtos WHERE ID = @ID";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", id);
        command.ExecuteNonQuery();
    }
}
