using ClassControl.Domain.Entities;
using ClassControl.Repository;

namespace ClassControl.Service
{
    public class MateriasService
    {
        private readonly MateriasRepository _materiasRepository;
        public MateriasService()
        {
            _materiasRepository = new MateriasRepository();
        }

        public async Task Adicionar(MateriasEntity aluno)
        {
            try
            {
                await _materiasRepository.AdicionarMateria(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<MateriasEntity> BuscarPorID(int ID)
        {
            try
            {
                return await _materiasRepository.BuscarPorID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Excluir(int id)
        {
            try
            {
                await _materiasRepository.ExcluirMateria(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Editar(MateriasEntity alunos)
        {
            try
            {
                await _materiasRepository.EditarMateria(alunos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<MateriasEntity>> Listar()
        {
            try
            {
                return await _materiasRepository.ListarMaterias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task VincularMaterias(List<int> ids, int alunoID)
        {
            try
            {
                var jaCadastrado = await _materiasRepository.BuscarMateriasJaVinculadas(alunoID);
                ids.RemoveAll(x => jaCadastrado.Any(y => y == x));
                await _materiasRepository.VincularMaterias(ids, alunoID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<AlunoMateriaEntity>> BuscarVinculados(int alunoID)
        {
            try
            {
                return await _materiasRepository.BuscarMateriasVinculadas(alunoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
