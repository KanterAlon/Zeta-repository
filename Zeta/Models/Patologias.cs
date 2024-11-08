using System.Collections.Generic;

public class Patologias
{
    public int IdPatologia { get; set; }
    public string NombrePatologia { get; set; }

    // Método para obtener todas las patologías desde la base de datos
    public static List<Patologias> ObtenerTodas()
    {
        return BD.ObtenerPatologias(); // Llama al método de la clase BD
    }
}
