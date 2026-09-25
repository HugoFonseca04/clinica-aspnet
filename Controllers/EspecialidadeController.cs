using ClinicaASPNet.Models;
using ClinicaASPNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaASPNet.Controllers
{
    public class EspecialidadeController : Controller
    {
        private readonly RepositoryEspecialidade _repository;
        public EspecialidadeController(RepositoryEspecialidade repository) => _repository = repository;

        public async Task<IActionResult> Index() => View(await _repository.ListarComProfissionaisAsync());
        public async Task<IActionResult> Details(int id) => await GetView(id, "Details");
        [HttpGet] public IActionResult Create() => View(new Especialidade { DuracaoConsulta = 60 });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Especialidade especialidade)
        {
            if (_repository.NomeJaCadastrado(especialidade.Nome)) ModelState.AddModelError(nameof(especialidade.Nome), "Especialidade já cadastrada.");
            if (!ModelState.IsValid) return View(especialidade);
            especialidade.CriadoEm = DateTime.Now;
            await _repository.IncluirAsync(especialidade);
            TempData["Mensagem"] = "Especialidade cadastrada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id) => await GetView(id, "Edit");
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Especialidade especialidade)
        {
            if (id != especialidade.Id) return BadRequest();
            if (_repository.NomeJaCadastrado(especialidade.Nome, id)) ModelState.AddModelError(nameof(especialidade.Nome), "Especialidade já cadastrada.");
            if (!ModelState.IsValid) return View(especialidade);
            await _repository.AlterarAsync(especialidade);
            TempData["Mensagem"] = "Especialidade alterada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) => await GetView(id, "Delete");
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = (await _repository.ListarComProfissionaisAsync()).FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            if (item.Profissionais.Any()) { TempData["Erro"] = "Não é possível excluir uma especialidade vinculada a profissionais."; return RedirectToAction(nameof(Index)); }
            await _repository.ExcluirAsync(item);
            TempData["Mensagem"] = "Especialidade excluída com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        private async Task<IActionResult> GetView(int id, string view) { var item = await _repository.SelecionarPorIdAsync(id); return item == null ? NotFound() : View(view, item); }
    }
}
