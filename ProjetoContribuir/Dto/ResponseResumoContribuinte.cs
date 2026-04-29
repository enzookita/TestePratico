public class ResponseContribuinteResumo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public int QuantidadeTotalDebitos { get; set; }
    public decimal TotalEmAberto { get; set; }
    public int QuantidadeDebitosVencidos { get; set; }
    public decimal TotalVencido { get; set; }
}