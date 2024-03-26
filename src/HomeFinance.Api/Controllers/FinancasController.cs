using HomeFinance.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FinancasController : ControllerBase
    {
        private readonly IFinancesService _service;
        private readonly ILogger<FinancasController> _logger;
       
        public FinancasController(IFinancesService service,ILogger<FinancasController> logger)
        {
            _service = service; 
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> BuscarTodasFinancas()
        {
            try
            {
                var financas = await _service.BuscarTodasFinancas();
                return Ok(financas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro na api ao listar a listagem de financas");
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> BuscarFinancaPorId(Guid id)
        {
            var financa = _service.BuscarFinancaPorId(id);
            return Ok(financa);
        }
    }
}
