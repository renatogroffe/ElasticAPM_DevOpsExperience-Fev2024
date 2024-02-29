using APIOrquestracao.Clients;
using APIOrquestracao.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIOrquestracao.Controllers;

[ApiController]
[Route("[controller]")]
public class OrquestracaoController : ControllerBase
{
    private readonly ILogger<OrquestracaoController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ContagemClient _contagemClient;

    public OrquestracaoController(ILogger<OrquestracaoController> logger,
        IConfiguration configuration, ContagemClient contagemClient)
    {
        _logger = logger;
        _configuration = configuration;
        _contagemClient = contagemClient;
    }

    [HttpGet]
    public async Task<ResultadoOrquestracao> Get()
    {
        var resultado = new ResultadoOrquestracao();
        resultado.Horario = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        var urlApiRedis = _configuration["ApiContagemRedis"]!;
        resultado.ContagemRedis =
            await _contagemClient.ObterContagemAsync(urlApiRedis);
        _logger.LogInformation($"Valor contagem Redis: {resultado.ContagemRedis!.ValorAtual}");
        
        var urlApiPostgres = _configuration["APIContagemPostgreSQL"]!;
        resultado.ContagemPostgreSQL =
            await _contagemClient.ObterContagemAsync(urlApiPostgres);
        _logger.LogInformation($"Valor contagem PostgreSQL: {resultado.ContagemPostgreSQL!.ValorAtual}");

        return resultado;
    }
}
