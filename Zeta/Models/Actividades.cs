using System.Collections.Generic;

public class Actividades
{
    public int IdActividad { get; set; }
    public string NombreActividad { get; set; }

    // Método para obtener todas las actividades desde la base de datos
    public static List<Actividades> ObtenerTodas()
    {
        return BD.ObtenerActividades(); // Llama al método de la clase BD
    }
}