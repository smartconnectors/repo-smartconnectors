using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class SchedulersDataAccess : BaseDataAccess<Scheduler>, ISchedulersDataAccess
    {
        public SchedulersDataAccess(string conStr) : base(conStr)
        {

        }

        public async Task<IEnumerable<Scheduler>> GetSchedulers(int workflowId)
        {
            return await QueryAsync<Scheduler>("select * from Schedulers where WorkflowId=@workflowId", new { workflowId });
        }

        public async Task<int> CreateAsync(Scheduler ops)
        {
            return await ExecuteScalarAsync<int>("insert Schedulers (Name, StartDate, EndDate, StartTime, EndTime, IsRepeated, RepeatOptionId, WorkflowId) output inserted.Id values(@Name, @StartDate, @EndDate, @StartTime, @EndTime, @IsRepeated, @RepeatOptionId, @WorkflowId)",
                ops);
        }

        public async Task<int> UpdateAsync(Scheduler ops)
        {
            return await ExecuteScalarAsync<int>("update Schedulers set Name = @Name, StartDate = @StartDate, EndDate = @EndDate, StartTime = @StartTime, EndTime = @EndTime, IsRepeated = @IsRepeated, RepeatOptionId = @RepeatOptionId, WorkflowId = @WorkflowId output inserted.Id where Id = @Id ", ops);
        }
    }
}
