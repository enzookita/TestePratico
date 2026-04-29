public class ContruibuinteService
{
    private readonly DataBaseContext _context;
    private readonly ILogger<ContruibuinteService> _logger;
    private readonly DebitoService _debitoService;
    public ContruibuinteService(DataBaseContext context, ILogger<ContruibuinteService> logger, DebitoService debitoService)
    {
        _context = context;
        _logger = logger;
        _debitoService = debitoService;
    }

    public object CreateContribuinte(CreateContribuinteDto dto)
    {
        _logger.LogInformation("Criando contribuinte. Nome={Nome}, CpfCnpj={CpfCnpj}", dto.Nome, dto.CpfCnpj);
        var contribuinte = new Contribuinte
        {
            Nome = dto.Nome,
            CpfCnpj = dto.CpfCnpj,
            DataCriacao = DateTime.Now
        };
        if (CpfCnpjAlreadyExists(dto.CpfCnpj) == true)
        {
            _logger.LogWarning("Tentativa de criar contribuinte com CPF/CNPJ duplicado: {CpfCnpj}", dto.CpfCnpj);
            return new ErrorResponse { Message = "Contribuinte com este CPF/CNPJ já existe", Errors = ["CPF/CNPJ duplicado"] };
        }

        try
        {
            _context.Contribuintes.Add(contribuinte);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar contribuinte");
            return new ErrorResponse { Message = "Erro ao criar contribuinte", Errors = [ex.Message] };
        }

        return new ResponseContribuinte
        {
            Id = contribuinte.Id,
            Nome = contribuinte.Nome,
            CpfCnpj = contribuinte.CpfCnpj,
            DataCriacao = contribuinte.DataCriacao
        };
    }

    public bool CpfCnpjAlreadyExists(string cpfCnpj)
    {
        if (_context.Contribuintes.Any(c => c.CpfCnpj == cpfCnpj))
        {
            return true;
        }
        return false;
    }

    public object GetContribuinte(Guid id)
    {
        var contribuinte = _context.Contribuintes.Find(id);
        if (contribuinte == null)
        {
            _logger.LogWarning("Contribuinte não encontrado. ID={Id}", id);
            return new ErrorResponse { Message = "Contribuinte não encontrado", Errors = ["ID inválido"] };
        }

        var resumo = _debitoService.GetResumoPorContribuinte(id, DateTime.Today);

        return new ResponseContribuinteResumo
        {
            Id = contribuinte.Id,
            Nome = contribuinte.Nome,
            QuantidadeTotalDebitos = resumo.QuantidadeTotal,
            TotalEmAberto = resumo.TotalEmAberto,
            QuantidadeDebitosVencidos = resumo.QuantidadeVencidos,
            TotalVencido = resumo.TotalVencido
        };
    }

    public bool ContribuinteExists(Guid id)
    {
        if (_context.Contribuintes.Any(c => c.Id == id))
        {
            return true;
        }
        return false;
    }
}