using HomeFinance.Application.Interfaces;
using HomeFinance.Infra.DTOs.Request.Financas;
using HomeFinance.Infra.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FinancasController : ControllerBase
    {
        private readonly IFinancesService _service;
        private readonly ILogger<FinancasController> _logger;

        public FinancasController(IFinancesService service, ILogger<FinancasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodasFinancas()
        {
            try
            {
                _logger.LogInformation("Listando todas as finanças");
                var financas = await _service.BuscarTodasFinancas();
                return Ok(financas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar finanças");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarFinancaPorId([FromQuery] Guid id)
        {
            try
            {
                var financa = await _service.BuscarFinancaPorId(id);
                if (financa is null)
                    return NotFound();

                return Ok(FinancaMapping.ToDto(financa));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar finança {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarFinanca([FromBody] FinancaCreateRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var created = await _service.CriarFinanca(request);
                return Ok(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar finança");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarFinanca([FromBody] FinancaUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var updated = await _service.AtualizarFinanca(request);
                if (updated is null)
                    return NotFound();

                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar finança {Id}", request.IdFinanca);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarFinanca([FromQuery] Guid id)
        {
            try
            {
                var financa = await _service.BuscarFinancaPorId(id);
                if (financa is null)
                    return NotFound();

                await _service.DeletarFinancas(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar finança {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AlternarPago([FromQuery] Guid id)
        {
            try
            {
                var financa = await _service.BuscarFinancaPorId(id);
                if (financa is null)
                    return NotFound();

                await _service.AlterarValorPago(id);
                var atualizada = await _service.BuscarFinancaPorId(id);
                return Ok(FinancaMapping.ToDto(atualizada!));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alternar pago da finança {Id}", id);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
