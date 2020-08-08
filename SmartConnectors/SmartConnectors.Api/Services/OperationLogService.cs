using SmartConnectors.DataAccess;
using SmartConnectors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartConnectors.Api.Services
{
    public enum OperationStatus
    {
        Success,
        Error
    }

    public abstract class OperationLogService
    {
        private readonly string _conStr;
        private OperationLog _operationLog;

        public OperationLogService(string conStr, int workflowId)
        {
            _conStr = conStr;
            _operationLog = new OperationLog
            {
                WorkflowId = workflowId,
                CreatedDate = DateTime.Now
            };
        }

        public async Task LogInfo(string message)
        {
            _operationLog.Status = "Pass";
            _operationLog.Message = message;

            await Execute();
        }
        public async Task LogError(string message, string content)
        {
            _operationLog.Status = "Fail";
            _operationLog.Content = content;
            _operationLog.Message = message;

            await Execute();
        }

        public async Task Execute()
        {
            using (var da = new OperationLogDataAccess(_conStr))
            {
                await da.CreateAsync(_operationLog);
            }
        }
    }
}
