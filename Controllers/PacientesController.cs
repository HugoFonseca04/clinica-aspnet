using ClinicaASPNet.Models;
using ClinicaASPNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaASPNet.Controllers
{
    public class PacientesController : Controller
    {
        private readonly RepositoryPaciente _repository;
        public PacientesController(RepositoryPaciente repository) => _repository = repository;

        public async Task<IActionResult> Index(string? nome) { ViewBag.Nome = nome; return View(await _repository.ListarAsync(nome)); }

        public async Task<IActionResult> Details(int id) => await GetView(id, "Details");

        [HttpGet] public IActionResult Create() => View(new Paciente { DataNascimento = DateTime.Today.AddYears(-18) });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            paciente.Cpf = (paciente.Cpf ?? string.Empty).Trim();
            if (_repository.CpfJaCadastrado(paciente.Cpf)) ModelState.AddModelError(nameof(paciente.Cpf), "CPF já cadastrado.");
            if (!ModelState.IsValid) return View(paciente);
            paciente.CriadoEm = DateTime.Now; paciente.AtualizadoEm = DateTime.Now;
            await _repository.IncluirAsync(paciente);
            TempData["Mensagem"] = "Paciente cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id) => await GetView(id, "Edit");

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente paciente)
        {
            if (id != paciente.Id) return BadRequest();
            if (_repository.CpfJaCadastrado(paciente.Cpf, id)) ModelState.AddModelError(nameof(paciente.Cpf), "CPF já cadastrado.");
            if (!ModelState.IsValid) return View(paciente);
            paciente.AtualizadoEm = DateTime.Now;
            await _repository.AlterarAsync(paciente);
            TempData["Mensagem"] = "Paciente alterado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) => await GetView(id, "Delete");

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _repository.SelecionarPorIdAsync(id);
            if (paciente == null) return NotFound();
            await _repository.ExcluirAsync(paciente);
            TempData["Mensagem"] = "Paciente excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> GetView(int id, string view)
        {
            var item = await _repository.SelecionarPorIdAsync(id);
            return item == null ? NotFound() : View(view, item);
        }
    }
}
