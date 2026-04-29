using System.ComponentModel.DataAnnotations;

public class ResponseContribuinte
{
    public Guid Id { get; set; }
    [Required]
    public required string Nome { get; set; }
    [Required]
    public required string CpfCnpj { get; set; }
    public DateTime DataCriacao { get; set; }
}