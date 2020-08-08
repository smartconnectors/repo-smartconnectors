using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class WorkflowConnectorDataAccess : BaseDataAccess<WorkflowConnector>, IWorkflowConnectorDataAccess
    {
        public WorkflowConnectorDataAccess(string conStr) : base(conStr)
        {

        }
        public async Task<WorkflowConnector> CreateAsync(WorkflowConnector entity)
        {
            var result = await ExecuteScalarAsync<int>(
                "insert WorkflowConnectors (ConnectorId, WorkflowId, Pos) output inserted.Id values(@ConnectorId, @WorkflowId, @Pos)",
                entity);
            return await Get(result);
        }

        public async Task<IEnumerable<WorkflowConnector>> CreateMultiAsync(WorkflowConnector entity)
        {
            if (entity.IsPreScriptingEnabled)
            {
                await AddPreScriptingConnector(entity.WorkflowId, 1);
            }

            if (entity.IsPostScriptingEnabled)
            {
                await AddPostScriptingConnector(entity.WorkflowId, 3);
            }

            if ((entity.IsPreScriptingEnabled || entity.IsPostScriptingEnabled) && entity.IsTransformationEnabled)
            {
                await AddTransformationConnector(entity.WorkflowId, 2);
            }
            else if (entity.IsTransformationEnabled)
            {
                await AddTransformationConnector(entity.WorkflowId, 1);
            }

            await ExecuteScalarAsync<int>(
                "insert WorkflowConnectors (ConnectorId, WorkflowId, Pos) output inserted.Id values(@ConnectorId, @WorkflowId, @Pos)",
                entity);

            return await GetByWorkflow(entity.WorkflowId);
        }

        public async Task<WorkflowConnector> UpdateAsync(WorkflowConnector entity)
        {
            if (entity == null)
                throw new ArgumentNullException("");

            var result = await ExecuteScalarAsync<int>(
                "update WorkflowConnectors set ConnectorId = @ConnectorId, WorkflowId = @WorkflowId, Pos = @Pos where Id = @Id", entity);

            return await Get(result);
        }

        public async Task UpdateConnectorPositionAsync(int workflowId, List<int> connectorIds)
        {
            if (connectorIds.Any())
            {
                for (int i = 0; i < connectorIds.Count; i++)
                {
                    var workflowConnectorId = (await GetByWorkflowConnectorId(workflowId, connectorIds[i]));
                    await ExecuteScalarAsync<int>(
                        "update WorkflowConnectors set ConnectorId = @ConnectorId, Pos = @Pos where Id = @Id", new
                        {
                            ConnectorId = connectorIds[i],
                            Pos = i,
                            Id = workflowConnectorId
                        });
                }
            }
        }

        public async Task<IEnumerable<WorkflowConnector>> GetByWorkflow(int workflowId)
        {
            var sql = "Select * from WorkflowConnectors where WorkflowId = {0} order by pos";
            return await QueryAsync<WorkflowConnector>(string.Format(sql, workflowId));
        }

        public async Task<WorkflowConnector> GetByWorkflowConnectorId(int workflowId, int connectorId)
        {
            var sql = @"Select * from WorkflowConnectors where WorkflowId = {0} and ConnectorId = {0}";
            return await QueryFirstOrDefaultAsync<WorkflowConnector>(string.Format(sql, workflowId, connectorId));
        }

        public async Task<Connector> GetConnectorByName(string name)
        {
            var sql = @"Select * from Connectors where Name = {0}";
            return await QueryFirstOrDefaultAsync<Connector>(string.Format(sql, name));
        }

        private async Task AddTransformationConnector(int workflowId, int pos)
        {
            var result = await ExecuteScalarAsync<int>(
                    "insert WorkflowConnectors (ConnectorId, WorkflowId, Pos) output inserted.Id values((Select id from [Connectors] where name = 'Transformation'), @WorkflowId, @Pos)",
                    new { WorkflowId = workflowId, Pos = pos });
        }

        private async Task AddPreScriptingConnector(int workflowId, int pos)
        {
            var result = await ExecuteScalarAsync<int>(
                    "insert WorkflowConnectors (ConnectorId, WorkflowId, Pos) output inserted.Id values((Select id from [Connectors] where name = 'Script'), @WorkflowId, @Pos)",
                    new { WorkflowId = workflowId, Pos = pos });
        }

        private async Task AddPostScriptingConnector(int workflowId, int pos)
        {
            await ExecuteScalarAsync<int>(
                    "insert WorkflowConnectors (ConnectorId, WorkflowId, Pos) output inserted.Id values((Select id from [Connectors] where name = 'Script'), @WorkflowId, @Pos)",
                    new { WorkflowId = workflowId, Pos = pos });
        }
    }
}
