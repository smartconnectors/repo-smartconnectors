using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class TransformationDataAccess : BaseDataAccess<Transformation>, ITransformationDataAccess
    {
        public TransformationDataAccess(string connectionString) : base(connectionString)
        {
        }
        public async Task<Transformation> CreateAsync(Transformation entity)
        {
            if (entity == null)
                throw new ArgumentNullException("");

            var result = await ExecuteScalarAsync<int>(
                "insert Transformations (Input,Output,Payload,Script, WorkflowId, CreatedDate, ModifiedDate) output inserted.Id values(@Input, @Output, @Payload, @Script, @WorkflowId, getdate(), getDate())",
                entity);
            return await Get(result);
        }

        public async Task<Transformation> UpdateAsync(Transformation entity)
        {
            if (entity == null)
                throw new ArgumentNullException("");

            var result = await ExecuteScalarAsync<int>(@"update Transformations set 
                                             Input = @Input
                                             ,Output =  @Output
                                             ,Payload =  @Payload
                                            ,Script = @Script
                                            ,WorkflowId =  @WorkflowId
                                            ,ModifiedDate = getdate() where Id = @Id", entity);

            return await Get(entity.Id);
        }

        public async Task<IEnumerable<Transformation>> GetByWorkflow(int workflowId)
        {
            var sql = "Select * from Transformations where WorkflowId = {0} order by ModifiedDate desc";
            return await QueryAsync<Transformation>(string.Format(sql, workflowId));
        }
    }
}
