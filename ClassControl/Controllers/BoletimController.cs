using ClassControl.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClassControl.Controllers
{
    public class BoletimController : Controller
    {
        private readonly AlunoService _alunoService;
        public BoletimController()
        {
            _alunoService = new AlunoService();
        }
        public async Task<ActionResult> Index()
        {
            var alunos = await _alunoService.Listar();
            return View(alunos);
        }
    }
}
