using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<Project> GetByIdAsync(int id);

        Task<int> AddAsync(Project? project);

        Task DeleteAsync(int id);

        Task FinishAsync(Project? project);

        Task StartAsync(Project? project);

        Task SaveChangeAsync(Project? project);
    }
}
