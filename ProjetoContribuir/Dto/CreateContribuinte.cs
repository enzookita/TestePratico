using System.ComponentModel.DataAnnotations;

public class CreateContribuinteDto
{
    [Required(ErrorMessage = "O nome é um campo obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public required string Nome { get; set; }
    [Required(ErrorMessage = "O CPF/CNPJ é um campo obrigatório.")]
    [MaxLength(14, ErrorMessage = "O CPF/CNPJ deve ter no máximo 14 caracteres.")]
    public required string CpfCnpj { get; set; }
}