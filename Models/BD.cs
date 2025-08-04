using Microsoft.Data.SqlClient;
using Dapper;
namespace TP06_Hevia_Ku.Models;
public static class BD {
    private static string _connectionString = @"Server=localhost; DataBase=TP06_Hevia_Ku;Integrated Security=True; TrustServerCertificate=True;";
    public static void AgregarUsuario(Usuario nuevo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO Integrantes 
                (email, password)
                VALUES 
                (@email, @password)";
            connection.Execute(query, nuevo);
        }
    }
    public static Usuario encontrarUsuario(string email, string contrasenia)
    {
        Usuario integrante;
        using(SqlConnection connection = new SqlConnection(_connectionString)) 
        {
            string query = "SELECT email, password FROM Integrantes WHERE email = @Email AND password = @Contrasenia";
            integrante = connection.QueryFirstOrDefault<Usuario>(query, new { Email = email, Password = contrasenia });
        }
        return integrante;
    }
    public static Usuario encontrarUsuarioPorEmail(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Integrantes WHERE email = @Email";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Email = email });
       }
    }

    public static Usuario encontrarTareaPorNombre(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Integrantes WHERE email = @Email";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Email = email });
       }
    }
}