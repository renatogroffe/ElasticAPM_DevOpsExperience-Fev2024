using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using APIContagem.Models;
using APIContagem.Logging;
using APIContagem.Tracing;

namespace APIContagem.Controllers;

[ApiController]
[Route("[controller]")]
public class ContadorController : ControllerBase
{
    private readonly ILogger<ContadorController> _logger;
    private readonly ConnectionMultiplexer _connectionRedis;

    public ContadorController(ILogger<ContadorController> logger,
        ConnectionMultiplexer connectionRedis)
    {
        _logger = logger;
        _connectionRedis = connectionRedis;
    }

    [HttpGet]
    public ResultadoContador Get()
    {
        var valorAtualContador =
            (int)_connectionRedis.GetDatabase().StringIncrement("APIContagem");;

        _logger.LogValorAtual(valorAtualContador);

        return new ()
        {
            ValorAtual = valorAtualContador,
            Producer = ContagemTracingExtensions.Local,
            Kernel = ContagemTracingExtensions.Kernel,
            Framework = ContagemTracingExtensions.Framework,
            Mensagem = "Testes com Redis"
        };
    }
}