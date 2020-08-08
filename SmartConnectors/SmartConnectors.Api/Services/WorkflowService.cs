using SmartConnectors.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SmartConnectors.Api.Services.Connectors;
using Newtonsoft.Json;
using System.IO;
using SmartConnectors.Models;
using System;

namespace SmartConnectors.Api.Services
{
    public class WorkflowService : OperationLogService
    {
        private readonly string _conStr;
        private readonly int _workflowId;

        public WorkflowService(string conStr, int workflowId) : base(conStr, workflowId)
        {
            _conStr = conStr;
            _workflowId = workflowId;
        }

        public async Task<object> RunOperation()
        {
            using (var da = new WorkflowDataAccess(_conStr))
            {
                var operations = await da.GetOperations(_workflowId);
                var transformationPayload = operations.Where(o => o.Name == "Transformation Payload").FirstOrDefault();

                if (operations.Any())
                {
                    List<IDictionary<string, object>> transformedList = new List<IDictionary<string, object>>();
                    dynamic salesforceResult = null;

                    foreach (var item in operations)
                    {
                        var excelList = new List<Dictionary<string, object>>();

                        switch (item.Type.ToString())
                        {
                            case "Salesforce":
                                if (item.Pos == 0)
                                {
                                    var sfService = new SalesforceService(_conStr, _workflowId);
                                    var intermediateJson = JsonConvert.DeserializeObject<string>(item.Content);

                                    if (!string.IsNullOrWhiteSpace(intermediateJson) && intermediateJson.Contains("SELECT"))
                                    {
                                        salesforceResult = await sfService.ExecuteSourceOperation(intermediateJson);
                                    }
                                }
                                else if(item.Name == "Salesforce Target Object Selection")
                                {
                                    var sfService = new SalesforceService(_conStr, _workflowId);
                                    salesforceResult = await sfService.ExecuteTargetOperation(item, transformedList);

                                }
                                break;

                            case "Excel":
                                var excelService = new ExcelService(_conStr, _workflowId);

                                if (item.Pos == 0)
                                {
                                    excelList = (await excelService.ExecuteSourceOperation(item)).ToList();
                                }
                                else
                                {
                                    var content = JsonConvert.DeserializeObject<ExcelConfig>(item.Content);

                                    if (!string.IsNullOrWhiteSpace(content.FileName))
                                    {
                                        var blobFileName = await excelService.ExecuteTargetOperation(content.FileName, salesforceResult);

                                        if (!string.IsNullOrWhiteSpace(blobFileName))
                                        {
                                            using (var ds = new DocumentService())
                                            {
                                                await ds.CreateDocument(content.FileName, "", null, ".xls", _workflowId, blobFileName);

                                                return new
                                                {
                                                    localFileSystem = content.IsLocalFileSystem,
                                                    fileName = blobFileName
                                                };
                                            }
                                        }
                                    }
                                }

                                break;


                            default:
                                break;
                        }

                        if (item.Pos == 0 && excelList.Any())
                        {
                            var tService = new TransformationService(_conStr, _workflowId);
                            transformedList = (await tService.ExecuteOperation(transformationPayload, excelList)).Take(1).ToList();
                        }
                    }
                }
            }

            return new
            {
                Success = false,
                Message = "Operation Unsuccesfully"
            }; ;
        }
    }
}
