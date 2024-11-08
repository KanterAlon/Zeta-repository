using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
    public class BD
    {
        private static string _connectionString = @"Server=192.168.0.243;Database=bdZeta;User Id=sa;Password=Gjdmsp3275";

        public static List<Post> ObtenerPostsOrdenadosPorFecha()
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Post>("SELECT * FROM Posts ORDER BY FechaCreacion DESC").ToList();
            }
        }
    }

