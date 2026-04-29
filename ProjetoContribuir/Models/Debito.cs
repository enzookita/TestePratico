using System.ComponentModel.DataAnnotations;

public class Debito
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Preco precisa ser um valor valido maior que zero.")]
    public decimal Valor { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DataVencimento { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DataPagamento { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DataCriacao { get; set; }

    [Required]
    public Guid ContribuinteId { get; set; }

    public Contribuinte? Contribuinte { get; set; }
}