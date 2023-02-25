using ClassControl.Domain.Entities;
using ClassControl.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClassControl.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AlunoService _alunoService;
        private readonly MateriasService _MateriasService;
        public AlunoController()
        {
            _alunoService = new AlunoService();
            _MateriasService = new MateriasService();
        }
        public async Task<ActionResult> Index()
        {
            var materias = await _MateriasService.Listar();
            var alunos = await _alunoService.Listar();
            ViewBag.Materias= materias;
            return View(alunos);
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarAluno(AlunosEntity aluno)
        {
            try
            {
                await _alunoService.Adicionar(aluno);
                return Json(new
                {
                    Success = true,
                    Message = "Aluno Inserido com Sucesso!",
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditarAluno(AlunosEntity aluno)
        {
            try
            {
                await _alunoService.Editar(aluno);
                return Json(new
                {
                    Success = true,
                    Message = "Aluno editado com Sucesso!",
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult> ExcluirAluno(int id)
        {
            try
            {
                await _alunoService.Excluir(id);
                return Json(new
                {
                    Success = true,
                    Message = "Aluno excluido com Sucesso!",
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult> BuscarPorID(int ID)
        {
            try
            {
                var aluno = await _alunoService.BuscarPorID(ID);
                return Json(new
                {
                    Object = aluno,
                    Success = true,
                    Message = "Aluno Inserido com Sucesso!",
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
