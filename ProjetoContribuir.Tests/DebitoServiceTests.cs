using System;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DebitoServiceTests
{
    [Fact]
    public void GetResumoPorContribuinte_DeveCalcularCorretamente()
    {
        var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var contribuinteId = Guid.NewGuid();
        var hoje = DateTime.Today;

        using (var context = new DataBaseContext(options))
        {
            context.Debitos.Add(new Debito
            {
                Id = Guid.NewGuid(),
                ContribuinteId = contribuinteId,
                Valor = 100,
                DataPagamento = null,
                DataVencimento = hoje.AddDays(1)
            });

            context.Debitos.Add(new Debito
            {
                Id = Guid.NewGuid(),
                ContribuinteId = contribuinteId,
                Valor = 50,
                DataPagamento = null,
                DataVencimento = hoje.AddDays(-1)
            });

            context.SaveChanges();
        }

        using (var context = new DataBaseContext(options))
        {
            var service = new DebitoService(context);

            var resumo = service.GetResumoPorContribuinte(contribuinteId, hoje);

            Assert.Equal(2, resumo.QuantidadeTotal);
            Assert.Equal(100, resumo.TotalEmAberto);
            Assert.Equal(1, resumo.QuantidadeVencidos);
            Assert.Equal(50, resumo.TotalVencido);
        }
    }
}