using DataAccess.Models;

namespace DataAccess.Interfaces.IServices
{
    public interface IProjectService
    {
        User AssignToUser(int userId, int projectId);
        void Create(Project project);
        void CreateOrUpdate(Project project);
        void Delete(Project project);
    }
}
