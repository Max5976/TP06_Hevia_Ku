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
    public static Usuario encontrarUsuario(string nombreUsuario, string password)
    {
        Usuario integrante;
        using(SqlConnection connection = new SqlConnection(_connectionString)) 
        {
            string query = "SELECT * FROM Usuarios WHERE Usuario = @Usuario AND Password = @Password";
            integrante = connection.QueryFirstOrDefault<Usuario>(query, new { Usuario = nombreUsuario, Password = password });
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
    public static Usuario encontrarUsuarioPorNombreDeUsuario(string Nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Integrantes WHERE  = @Email";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Email = email });
       }
    }

    public static Usuario encontrarTareaPorNombre(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE nombre = @Nombre";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Nombre = nombre });
       }
    }
}