using ClassControl.Domain.Entities;
using ClassControl.Repository;

namespace ClassControl.Service
{
    public class NotasService
    {
        private readonly NotasRepository _notasRepository;
        public NotasService()
        {
            _notasRepository = new NotasRepository();
        }

        public async Task Adicionar(NotasEntity nota)
        {
            try
            {
                await _notasRepository.AdicionarNota(nota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<NotasEntity> BuscarPorID(int ID)
        {
            try
            {
                return await _notasRepository.BuscarPorID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NotasEntity>> BuscarPorAluno(int ID)
        {
            try
            {
                return await _notasRepository.BuscarPorAluno(ID);
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
                await _notasRepository.ExcluirNota(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Editar(NotasEntity notas)
        {
            try
            {
                await _notasRepository.EditarNota(notas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<NotasEntity>> Listar()
        {
            try
            {
                return await _notasRepository.ListarNotas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
