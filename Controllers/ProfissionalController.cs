using ClinicaASPNet.Data;
using ClinicaASPNet.Models;
using ClinicaASPNet.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaASPNet.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly RepositoryProfissional _repository;
        private readonly ClinicaDbContext _context;
        public ProfissionalController(RepositoryProfissional repository, ClinicaDbContext context) { _repository = repository; _context = context; }

        public async Task<IActionResult> Index() => View(await _repository.ListarComEspecialidadeAsync());
        public async Task<IActionResult> Details(int id) => await GetView(id, "Details");
        [HttpGet] public async Task<IActionResult> Create() { await CarregarEspecialidades(); return View(new Profissional { DataNascimento = DateTime.Today.AddYears(-18) }); }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profissional profissional)
        {
            if (_repository.RegistroJaCadastrado(profissional.RegistroProfissional)) ModelState.AddModelError(nameof(profissional.RegistroProfissional), "Registro profissional já cadastrado.");
            if (!ModelState.IsValid) { await CarregarEspecialidades(profissional.EspecialidadeId); return View(profissional); }
            profissional.CriadoEm = DateTime.Now;
            await _repository.IncluirAsync(profissional);
            TempData["Mensagem"] = "Profissional cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id) { var result = await _repository.SelecionarPorIdAsync(id); if (result == null) return NotFound(); await CarregarEspecialidades(result.EspecialidadeId); return View(result); }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profissional profissional)
        {
            if (id != profissional.Id) return BadRequest();
            if (_repository.RegistroJaCadastrado(profissional.RegistroProfissional, id)) ModelState.AddModelError(nameof(profissional.RegistroProfissional), "Registro profissional já cadastrado.");
            if (!ModelState.IsValid) { await CarregarEspecialidades(profissional.EspecialidadeId); return View(profissional); }
            await _repository.AlterarAsync(profissional);
            TempData["Mensagem"] = "Profissional alterado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) => await GetView(id, "Delete");
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _repository.SelecionarPorIdAsync(id);
            if (item == null) return NotFound();
            await _repository.ExcluirAsync(item);
            TempData["Mensagem"] = "Profissional excluído com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> GetView(int id, string view) { var item = await _repository.ListarComEspecialidadeAsync(); var result = item.FirstOrDefault(x => x.Id == id); return result == null ? NotFound() : View(view, result); }
        private async Task CarregarEspecialidades(int? selecionada = null) => ViewBag.Especialidades = new SelectList(await _context.Especialidades.OrderBy(x => x.Nome).ToListAsync(), "Id", "Nome", selecionada);
    }
}
