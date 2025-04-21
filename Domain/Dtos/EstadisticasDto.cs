
namespace Domain.Dtos
{
    public class EstadisticasDto
    {
        public int EncuestaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int TotalRespuestas { get; set; }
        public Dictionary<int, PreguntaEstadisticaDto> EstadisticasPorPregunta { get; set; }

    }
}
