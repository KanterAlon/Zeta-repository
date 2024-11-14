using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

public class BD
{
    // private static string _connectionString = @"Server=192.168.0.243;Database=bdZeta;User Id=sa;Password=Gjdmsp3275";
    private static string _connectionString = @"Server=localhost;Database=bdZeta;Integrated Security=True";

    public static List<Post> ObtenerPostsOrdenadosPorFecha()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            return db.Query<Post>("SELECT * FROM Posts ORDER BY fecha_creacion DESC").ToList();
        }
    }

    public void AgregarActividadUsuario(int userId, int actividadId, int horas)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        string query = @"
            INSERT INTO UsuarioActividades (id_usuario, id_actividad, horas)
            VALUES (@UserId, @ActividadId, @Horas)";
        connection.Execute(query, new { UserId = userId, ActividadId = actividadId, Horas = horas });
    }
}

    public Usuario? ValidarUsuario(string email, string password)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            // Consulta SQL para buscar al usuario con email y password
            string query = @"
                SELECT id_usuario, nombre, email, fecha_registro, ultima_sesion, 
                       edad, sexo, peso, altura, password
                FROM Usuarios
                WHERE email = @Email AND password = @Password";

            // Ejecuta la consulta y devuelve una instancia de Usuario si coincide
            return connection.QuerySingleOrDefault<Usuario>(query, new { Email = email, Password = password });
        }
    }


    public int CrearUsuario(Usuario usuario)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        string query = @"
            INSERT INTO Usuarios (nombre, email, password, fecha_registro, edad, sexo, peso, altura)
            OUTPUT INSERTED.id_usuario
            VALUES (@Nombre, @Email, @Password, @FechaRegistro, @Edad, @Sexo, @Peso, @Altura)";

        return connection.QuerySingle<int>(query, usuario);
    }
}

    public static List<Patologias> ObtenerPatologias()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string query = "SELECT id_patologia AS IdPatologia, nombre_patologia AS NombrePatologia FROM Patologias";
            return db.Query<Patologias>(query).ToList();
        }
    }

    public static List<Post> ObtenerUltimosTresPosts()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            // Devuelve solo los 3 posts más recientes
            return db.Query<Post>("SELECT TOP 3 * FROM Posts ORDER BY fecha_creacion DESC").ToList();
        }
    }

    // Nuevo método para obtener todas las actividades
    public static List<Actividades> ObtenerActividades()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string query = "SELECT id_actividad AS IdActividad, nombre_actividad AS NombreActividad FROM Actividades";
            return db.Query<Actividades>(query).ToList();
        }
    }
}