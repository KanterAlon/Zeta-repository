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

         public static List<Patologias> ObtenerPatologias()
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_patologia AS IdPatologia, nombre_patologia AS NombrePatologia FROM Patologias";
                return db.Query<Patologias>(query).ToList();
            }
        }
    }

