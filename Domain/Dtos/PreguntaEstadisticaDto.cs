

namespace Domain.Dtos
{
    public class PreguntaEstadisticaDto
    {
        public string Pregunta { get; set; }
        public string TipoPregunta { get; set; }

        public int TotalRespuestas { get; set; }
        public object Datos { get; set; } // Puede ser un objeto dinámico o una lista de objetos, dependiendo de la estructura de los datos
    }
}
