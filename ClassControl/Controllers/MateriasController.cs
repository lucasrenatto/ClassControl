using ClassControl.Domain.Entities;
using ClassControl.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClassControl.Controllers
{
    public class MateriasController : Controller
    {
        private readonly MateriasService _MateriasService;
        public MateriasController()
        {
            _MateriasService = new MateriasService();
        }
        public async Task<ActionResult> Index()
        {
            var materias = await _MateriasService.Listar();
            return View(materias);
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarMateria(MateriasEntity materia)
        {
            try
            {
                await _MateriasService.Adicionar(materia);
                return Json(new
                {
                    Success = true,
                    Message = "materia Inserido com Sucesso!",
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
        public async Task<ActionResult> VincularMaterias(List<int> ids, int alunoID)
        {
            try
            {
                await _MateriasService.VincularMaterias(ids, alunoID);
                return Json(new
                {
                    Success = true,
                    Message = "materia Inserido com Sucesso!",
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
        public async Task<ActionResult> BuscarMateriasVinculadas(int alunoID)
        {
            try
            {
                var materias = await _MateriasService.BuscarVinculados(alunoID);
                return Json(new
                {
                    Object = materias,
                    Success = true,
                    Message = "materia Inserido com Sucesso!",
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
        public async Task<ActionResult> EditarMateria(MateriasEntity materia)
        {
            try
            {
                await _MateriasService.Editar(materia);
                return Json(new
                {
                    Success = true,
                    Message = "materia editado com Sucesso!",
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
        public async Task<ActionResult> ExcluirMateria(int id)
        {
            try
            {
                await _MateriasService.Excluir(id);
                return Json(new
                {
                    Success = true,
                    Message = "materia excluido com Sucesso!",
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
                var materia = await _MateriasService.BuscarPorID(ID);
                return Json(new
                {
                    Object = materia,
                    Success = true,
                    Message = "materia Inserido com Sucesso!",
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
