using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class ProjectDataAccess : BaseDataAccess<Project>, IProjectDataAccess
    {
        public ProjectDataAccess(string conStr) : base(conStr)
        {
           
        }
        public async Task<int> CreateAsync(Project project)
        {
            return await ExecuteScalarAsync<int>(
                "insert Projects (Name, CreatedDate, ModifiedDate) output inserted.Id values(@Name, getDate(), getDate())",
                project);
        }
    }
}
