using ClassControl.Domain.Entities;
using ClassControl.Repository;

namespace ClassControl.Service
{
    public class AlunoService
    {
        private readonly AlunosRepository _alunoRepository;
        public AlunoService()
        {
            _alunoRepository = new AlunosRepository();
        }

        public async Task Adicionar(AlunosEntity aluno)
        {
            try
            {
                await _alunoRepository.AdicionarAluno(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AlunosEntity> BuscarPorID(int ID)
        {
            try
            {
                return await _alunoRepository.BuscarPorID(ID);
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
                await _alunoRepository.ExcluirAluno(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Editar(AlunosEntity alunos)
        {
            try
            {
                await _alunoRepository.EditarAluno(alunos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AlunosEntity>> Listar()
        {
            try
            {
                return await _alunoRepository.ListarAlunos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
