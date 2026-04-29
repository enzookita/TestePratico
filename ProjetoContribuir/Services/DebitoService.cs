public class DebitoService
{
    private readonly DataBaseContext _context;

    public DebitoService(DataBaseContext context)
    {
        _context = context;
    }

    public object CriarDebito(CreateDebito createDebito)
    {
        var debito = new Debito
        {
            ContribuinteId = createDebito.ContribuinteId,
            Valor = createDebito.Valor,
            DataPagamento = createDebito.DataPagamento ?? null,
            DataVencimento = createDebito.DataVencimento ?? DateTime.MinValue,
            DataCriacao = DateTime.Now
        };

        try
        {
            _context.Debitos.Add(debito);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            return new ErrorResponse { Message = "Erro ao criar débito", Errors = [ex.Message] };
        }
        return new ResponseDebito
        {
            Id = debito.Id,
            ContribuinteId = debito.ContribuinteId,
            Valor = debito.Valor,
            DataPagamento = debito.DataPagamento,
            DataVencimento = debito.DataVencimento
        };
    }

    public object GetDebito(Guid id)
    {
        var debito = _context.Debitos.Find(id);
        if (debito == null)
        {
            return new ErrorResponse { Message = "Débito não encontrado", Errors = ["ID inválido"] };
        }
        return new ResponseDebito
        {
            Id = Guid.NewGuid(),
            ContribuinteId = debito.ContribuinteId,
            Valor = debito.Valor,
            DataPagamento = debito.DataPagamento ?? DateTime.MinValue,
            DataVencimento = debito.DataVencimento
        };
    }

    public DebitoResumo GetResumoPorContribuinte(Guid contribuinteId, DateTime hoje)
    {
        var debitos = _context.Debitos.Where(d => d.ContribuinteId == contribuinteId);

        var totalEmAberto = debitos
            .Where(d => d.DataPagamento == null && d.DataVencimento >= hoje)
            .Sum(d => d.Valor);

        var vencidos = debitos
            .Where(d => d.DataPagamento == null && d.DataVencimento < hoje);

        return new DebitoResumo
        {
            QuantidadeTotal = debitos.Count(),
            TotalEmAberto = totalEmAberto,
            QuantidadeVencidos = vencidos.Count(),
            TotalVencido = vencidos.Sum(d => d.Valor)
        };
    }
}