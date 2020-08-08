using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class WorkflowDataAccess : BaseDataAccess<Workflow>, IWorkflowDataAccess
    {
        public WorkflowDataAccess(string conStr) : base(conStr)
        {

        }
        public async Task<Workflow> CreateAsync(Workflow entity)
        {
            if (entity == null)
                throw new ArgumentNullException("");

            var result = await ExecuteScalarAsync<int>(
                "insert Workflows (Name, ProjectId, PackageName, CreatedDate, ModifiedDate) output inserted.Id values(@Name, @ProjectId, @PackageName, getdate(), getDate())",
                entity);
            return await Get(result);
        }

        public async Task<Workflow> UpdateAsync(Workflow entity)
        {
            if (entity == null)
                throw new ArgumentNullException("");

            var result = await ExecuteScalarAsync<int>(
                "update Workflows set Name = @Name, ProjectId = @ProjectId, PackageName = @PackageName, ModifiedDate = getdate() where Id = @Id", entity);

            return await Get(result);
        }

        public async Task<Workflow> MarkWorkflowActiveAsync(int id, int projectId)
        {
            await ExecuteScalarAsync<int>(
                "update Workflows set IsActive = 0 where projectId = @projectId", new { projectId });

            var result = await ExecuteScalarAsync<int>(
                "update Workflows set IsActive = 1 where id = @id", new { id });

            return await Get(result);
        }

        public new async Task  DeleteAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("");

            await ExecuteScalarAsync<int>("delete from WorkflowConnectors where workflowId = @Id", new { Id = id });

            await ExecuteScalarAsync<int>("delete from Workflows where Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Workflow>> GetByProject(int projectId)
        {
            var sql = "Select * from Workflows where ProjectId = {0} order by ModifiedDate desc";
            return await QueryAsync<Workflow>(string.Format(sql, projectId));
        }

        public async Task<IEnumerable<ActiveOperation>> GetOperations(int workflowId)
        {
            var sql = @"SELECT 
                            op.Id, 
                            op.OperationTypeId, 
                            op.Content , 
                            opt.Name, 
                            opt.Type, 
                            wc.Pos,
                            wc.WorkflowId,
                            op.StepCount
                        FROM [SmartConnectors.Database].[dbo].[Operations] op
                        JOIN [SmartConnectors.Database].[dbo].WorkflowConnectors wc
                        ON op.WorkflowConnectorId = wc.Id
                        Join [SmartConnectors.Database].[dbo].OperationTypes opt
                        ON op.OperationTypeId = opt.Id
                        WHERE wc.WorkflowId = {0}
                        order by op.StepCount";

            return await QueryAsync<ActiveOperation>(string.Format(sql, workflowId));
        }       
    }

    public class ActiveOperation
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public int StepCount { get; set; }

        public string Content { get; set; }

        public int Pos { get;  set; }

        public int OperationTypeId { get; set; }

        public int WorkflowId { get; set; }
    }
}
