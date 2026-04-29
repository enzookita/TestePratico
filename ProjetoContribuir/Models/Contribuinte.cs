using System.ComponentModel.DataAnnotations;

public class Contribuinte
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Nome { get; set; }

    [Required]
    [MaxLength(14)]
    public required string CpfCnpj { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime DataCriacao { get; set; }

    //public List<Debito> Debitos { get; set; } = new List<Debito>();


}