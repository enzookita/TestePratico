public class ResponseDebito
{
    public Guid Id { get; set; }
    public Guid ContribuinteId { get; set; }
    public decimal Valor { get; set; }
    public DateTime? DataPagamento { get; set; }
    public DateTime DataVencimento { get; set; }
}