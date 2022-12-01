using HomeFinance.Application.Interfaces;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeFinance.MVC.Controllers
{
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
                var result = await _service.BuscarFinancas();
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Problema ao carregar a INDEX", "Erro: " + ex.Message);
                return StatusCode(500, "Erro Interno");
            }
            
        }
        public async Task<IActionResult> Details(Guid? id)
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
        public async Task<IActionResult> Create([Bind("FinancesId,FinanceName,DueDate,Price")] Finances finances)
        {
            if (ModelState.IsValid)
            {
                finances.FinancesId = Guid.NewGuid();
                await _service.AdicionarNovasDividas(finances);
                return RedirectToAction(nameof(Index));
            }
            return View(finances);
        }
        public async Task<IActionResult> Edit(Guid? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FinancesId,FinanceName,DueDate,Price")] Finances finances)
        {
            if (id != finances.FinancesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AtualizarDadosFinancas(finances);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!FinancesExists(finances.FinancesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Método "+nameof(Edit),$"Erro: {ex.Message}");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(finances);
        }

        public async Task<IActionResult> Delete(Guid? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_service.BuscarFinancaPorId(id) == null)
            {
                return Problem("Entity set 'AppDbContext.Finances'  is null.");
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
