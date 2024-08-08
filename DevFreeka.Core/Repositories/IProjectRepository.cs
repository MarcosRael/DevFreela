using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositorios
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAll();
    }
}
