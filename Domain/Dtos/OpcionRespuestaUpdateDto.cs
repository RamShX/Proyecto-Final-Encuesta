namespace Domain.Dtos
{
    public class OpcionRespuestaUpdateDto
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int? Valor { get; set; }
        public int Orden { get; set; }
    }
}
