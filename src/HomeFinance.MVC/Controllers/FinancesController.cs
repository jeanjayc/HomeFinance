using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Response.Financas;
using HomeFinance.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeFinance.MVC.Controllers
{
    [Authorize]
    public class FinancesController : Controller
    {
        private readonly IFinancesService _service;
        private readonly ILogger<FinancesController> _logger;
        public FinancesController(IFinancesService service, ILogger<FinancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var listFinances = await _service.BuscarTodasFinancasNaoPagas();

                if (listFinances is null || !listFinances.Any())
                {
                    return View("Error");
                }

                //mapper
                var viewModel = listFinances.Select(f => new FinanceVM
                {
                    Descricao = f.DescricaoFinanca,
                    DataVencimento = f.DataVencimento.ToString("dd/MM/yyyy"),
                    Valor = f.Valor,
                    Pago = f.Pago
                });

                ViewBag.Total = "Total: ";
                ViewBag.TotalDividas = await _service.SomarTotalFinancas();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problema ao carregar a INDEX", "Erro: " + ex.Message);
                return StatusCode(500, "Erro Interno");
            }

        }

        public async Task<IActionResult> BuscarFinancaPorDescricao(string descricao)
        {
            try
            {
                var result = await _service.BuscarFinancaPorNome(descricao);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IActionResult> BuscarTodasFinancas()
        {
            try
            {
                var listFinances = await _service.BuscarTodasFinancas();

                if (listFinances is null || !listFinances.Any())
                {
                    return View("Error");
                }

                var viewModel = listFinances.Select(f => new FinanceVM
                {
                    FinancaId = f.FinancaId,
                    Descricao = f.Descricao,
                    DataVencimento = f.DataVencimento.ToString("dd/MM/yyyy"),
                    Valor = f.Valor,
                    Pago = f.Pago,
                    QtdParcelas = f.QtdParcelas
                });

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problema ao carregar a lista de todas as financas", "Erro: " + ex.Message);
                return StatusCode(500, "Erro Interno");
            }
        }


        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _service.BuscarFinancaPorId(id) == null)
            {
                return NotFound();
            }

            var finances = await _service.BuscarFinancaPorId(id);
            if (finances == null)
            {
                return NotFound();
            }

            return View(finances);
        }
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinancaDTO financa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //mapper
                    var novaFinanca = new Finances(financa.DescricaoFinanca, financa.DataVencimento, financa.Valor);

                    await _service.AdicionarNovasDividas(novaFinanca);

                    return RedirectToAction(nameof(Index));
                }

                return View(financa);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problema ao criar nova divida", "Erro: " + ex.Message);
                throw;
            }

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || await _service.BuscarFinancaPorId(id) == null)
            {
                return NotFound();
            }

            var finances = await _service.BuscarFinancaPorId(id);
            if (finances == null)
            {
                return NotFound();
            }
            return View(finances);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Finances finances)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AtualizarDadosFinancas(id, finances);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Método " + nameof(Edit), $"Erro: {ex.Message}");
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(finances);
        }

        public async Task<IActionResult> AlterarValorPago(Guid id)
        {
            var financaAtualizada = await _service.AlterarValorPago(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var finances = await _service.BuscarFinancaPorId(id);
            if (finances != null)
            {
                await _service.DeletarFinancas(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FinancesExists(Guid id)
        {
            var result = _service.BuscarFinancaPorId(id);
            if (result is null)
                return false;

            return true;
        }
    }
}
