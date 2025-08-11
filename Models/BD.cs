using Microsoft.Data.SqlClient;
using Dapper;
namespace TP06_Hevia_Ku.Models;
public static class BD {
    private static string _connectionString = @"Server=localhost; DataBase=TP06_Hevia_Ku;Integrated Security=True; TrustServerCertificate=True;";
    public static void agregarUsuario(Usuario nuevo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "exec InsertarUsuario @nombreUsuario, @email, @password";
            int IdUsuario = connection.Execute(query, new {
                nombreUsuario = nuevo.NombreUsuario,    
                email = nuevo.Email,
                password = nuevo.Password
            });
        }
    }
    public static Usuario encontrarUsuario(string NombreUsuario, string Password)
    {
        Usuario integrante;
        using(SqlConnection connection = new SqlConnection(_connectionString)) 
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Password = @Password";
            integrante = connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = NombreUsuario, Password = Password });
        }
        return integrante;
    }
    public static Usuario encontrarUsuarioPorEmail(string Email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Email = @Email";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Email = Email });
       }
    }
    public static Usuario encontrarUsuarioPorNombreDeUsuario(string NombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = NombreUsuario });
       }
    }

    public static Usuario encontrarTareaPorNombre(string NombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Usuario>(query, new { NombreUsuario = NombreUsuario });
       }
    }
public static List<Usuarios_Tareas> MostrarTareas() 
{
    List<Usuarios_Tareas> tareas = new List<Usuarios_Tareas>();
    using (SqlConnection connection = new SqlConnection(_connectionString)) 
    {
        string query = @"
            SELECT 
                t.Nombre, 
                t.Estado, 
                t.Eliminado, 
                u.NombreUsuario AS Creador
            FROM Tareas t
            INNER JOIN Usuarios_Tareas ut ON t.Id = ut.TareaId
            INNER JOIN Usuarios u ON ut.UsuarioId = u.Id
            WHERE ut.Creador = 1";
        tareas = connection.Query<Usuarios_Tareas>(query).ToList();
    }
    return tareas;
}

public static void agregarTarea(Tareas nuevo)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "exec InsertarTarea @nombre, @estado, @eliminado";
        int IdTarea = connection.Execute(query, new {
            nombre = nuevo.Nombre,    
            estado = nuevo.Estado,
            eliminado = nuevo.Eliminado
        });

        string query = "exec InsertarConeccionUsuarioTarea @usuarioID, @tareaID, @creador";
        connection.Execute(query, new {
            nombre = nuevo.Nombre,    
            estado = nuevo.Estado,
            eliminado = nuevo.Eliminado
        });
    }
}
public static void EliminarTarea(int idTarea)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "UPDATE Tareas SET Eliminado = 0 WHERE Id = @Id";
        connection.Execute(query, new { Id = idTarea });
    }
}

public static void MarcarTareaFinalizada(int idTarea)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "UPDATE Tareas SET Estado = 1 WHERE Id = @Id";
        connection.Execute(query, new { Id = idTarea });
    }
}

}
