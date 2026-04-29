using System.ComponentModel.DataAnnotations;

public class CreateDebito
{
    [Required(ErrorMessage = "O ID do contribuinte é obrigatório.")]
    public Guid ContribuinteId { get; set; }
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }
    public DateTime? DataPagamento { get; set; }

    [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
    public DateTime? DataVencimento { get; set; }
}