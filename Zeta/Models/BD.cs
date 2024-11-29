using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;


public static class BD
{
    // private static string _connectionString = @"Server=192.168.0.243;Database=bdZeta;User Id=sa;Password=Gjdmsp3275"; juan
    private static string _connectionString = @"Server=localhost;Database=bdZeta;Integrated Security=True"; //localhost

    // private static string _connectionString = @"Server=zetabd.c5g00cey2pne.us-east-1.rds.amazonaws.com,1433;Database=bdZeta;User Id=admin;Password=somoszeta;"; //aws
    //agregar regla en aws para ejecutar en local y en la nube
    //https://us-east-1.console.aws.amazon.com/ec2/home?region=us-east-1#ModifyInboundSecurityGroupRules:securityGroupId=sg-00fb263b5ddd14fd6
    public static List<Posts> ObtenerPostsOrdenadosPorFecha()
    {
       using (SqlConnection db = new SqlConnection(_connectionString))
        {
            // Modificación de la consulta para hacer el JOIN con la tabla Usuarios y obtener los likes y dislikes
            string sql = @"SELECT p.id_post, p.titulo_post, p.contenido_post, p.fecha_creacion, p.imagen_url, u.nombre AS autor,
                                  (SELECT COUNT(*) FROM Interacciones i WHERE i.id_post = p.id_post AND i.tipo_interaccion = 1) AS likes,
                                  (SELECT COUNT(*) FROM Interacciones i WHERE i.id_post = p.id_post AND i.tipo_interaccion = 2) AS dislikes
                           FROM Posts p
                           INNER JOIN Usuarios u ON p.id_usuario = u.id_usuario
                           ORDER BY p.fecha_creacion DESC";

            return db.Query<Posts>(sql).ToList();
        }
    }

     public static void InsertarLike(int idPost, int idUsuario)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = @"INSERT INTO Interacciones (id_post,id_usuario,tipo_interaccion,fecha_interaccion)
               VALUES (@IdPost, @IdUsuario, 1, GETDATE())";
            db.Execute(sql, new { IdPost = idPost, IdUsuario = idUsuario });
        }
    }

      public static void InsertarDislike(int idPost, int idUsuario)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = @"INSERT INTO Interacciones (id_post,id_usuario,tipo_interaccion,fecha_interaccion)
               VALUES (@IdPost, @IdUsuario, 2, GETDATE())";
            db.Execute(sql, new { IdPost = idPost, IdUsuario = idUsuario });
        }
    }

  public static void AgregarPatologiaUsuario(int userId, int patologiaId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO UsuariosPatologias (id_usuario, id_patologia)
                VALUES (@UserId, @PatologiaId)";

            connection.Execute(query, new { UserId = userId, PatologiaId = patologiaId });
        }
    }    
    // Método para agregar una actividad a un usuario en la tabla ActividadesUsuario
    public static void AgregarActividadUsuario(int userId, int actividadId, int horas)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO ActividadesUsuario (id_usuario, id_actividad, frecuencia_semanal)
                VALUES (@UserId, @ActividadId, @Horas)";

            connection.Execute(query, new { UserId = userId, ActividadId = actividadId, Horas = horas });
        }
    }

    public static Usuario ValidarUsuario(string email, string password)
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

public static int CrearUsuario(Usuario usuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Usuarios (nombre, email, password, fecha_registro, edad, sexo, peso, altura)
                OUTPUT INSERTED.id_usuario
                VALUES (@nombre, @email, @password, @fecha_registro, @edad, @sexo, @peso, @altura);
        ";


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

    public static List<Posts> ObtenerUltimosTresPosts()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            // Devuelve solo los 3 posts más recientes
            return db.Query<Posts>("SELECT TOP 3 * FROM Posts ORDER BY fecha_creacion DESC").ToList();
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

   public static int ObtenerIdUsuarioPorEmail(string email)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Open();

            // Consulta SQL para obtener el id_usuario usando el email
            var query = "SELECT id_usuario FROM Usuarios WHERE email = @Email";

            // Ejecutar la consulta y obtener el resultado
            var idUsuario = db.QuerySingleOrDefault<int>(query, new { Email = email });

            return idUsuario;
        }
    }

    public static List<Posts> ObtenerPostsPorUsuario(int userId)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
           // Modificación de la consulta para hacer el JOIN con la tabla Usuarios y obtener los likes y dislikes
            string sql = @"SELECT p.id_post, p.titulo_post, p.contenido_post, p.fecha_creacion, p.imagen_url, u.nombre AS autor,
                                  (SELECT COUNT(*) FROM Interacciones i WHERE i.id_post = p.id_post AND i.tipo_interaccion = 1) AS likes,
                                  (SELECT COUNT(*) FROM Interacciones i WHERE i.id_post = p.id_post AND i.tipo_interaccion = 2) AS dislikes
                           FROM Posts p
                           INNER JOIN Usuarios u ON p.id_usuario = u.id_usuario
                           ORDER BY p.fecha_creacion DESC";

            return db.Query<Posts>(sql).ToList();
        }
    }

    public static int InsertarPost(int idUsuario, string contenidoPost, DateTime fechaCreacion)
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Posts (id_usuario, contenido_post, fecha_creacion)
                OUTPUT INSERTED.id_post
                VALUES (@IdUsuario, @ContenidoPost, @FechaCreacion)";

            return db.QuerySingle<int>(query, new
            {
                IdUsuario = idUsuario,
                ContenidoPost = contenidoPost,
                FechaCreacion = fechaCreacion
            });
        }
    }

}