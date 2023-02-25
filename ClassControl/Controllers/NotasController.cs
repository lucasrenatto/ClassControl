using ClassControl.Domain.Entities;
using ClassControl.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClassControl.Controllers
{
    public class NotasController : Controller
    {
        private readonly NotasService _NotasService;
        private readonly MateriasService _MateriasService;
        private readonly AlunoService _AlunoService;
        public NotasController()
        {
            _NotasService = new NotasService();
            _MateriasService = new MateriasService();
            _AlunoService = new AlunoService();
        }
        public async Task<ActionResult> Index()
        {
            var materias = await _MateriasService.Listar();
            var alunos = await _AlunoService.Listar();
            var notas = await _NotasService.Listar();
            ViewBag.Materias = materias;
            ViewBag.Alunos = alunos;
            return View(notas);
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarNota(NotasEntity nota)
        {
            try
            {
                await _NotasService.Adicionar(nota);
                return Json(new
                {
                    Success = true,
                    Message = "nota Inserido com Sucesso!",
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
        public async Task<ActionResult> EditarNota(NotasEntity nota)
        {
            try
            {
                await _NotasService.Editar(nota);
                return Json(new
                {
                    Success = true,
                    Message = "nota editado com Sucesso!",
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
        public async Task<ActionResult> ExcluirNota(int id)
        {
            try
            {
                await _NotasService.Excluir(id);
                return Json(new
                {
                    Success = true,
                    Message = "nota excluido com Sucesso!",
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
                var nota = await _NotasService.BuscarPorID(ID);
                return Json(new
                {
                    Object = nota,
                    Success = true,
                    Message = "nota Inserido com Sucesso!",
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
        public async Task<ActionResult> BuscarPorAluno(int ID)
        {
            try
            {
                var nota = await _NotasService.BuscarPorAluno(ID);
                return Json(new
                {
                    Object = nota,
                    Success = true,
                    Message = "nota Inserido com Sucesso!",
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
