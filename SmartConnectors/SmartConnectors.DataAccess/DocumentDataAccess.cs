using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.Models;
using System.Threading.Tasks;

namespace SmartConnectors.DataAccess
{
    public class DocumentDataAccess : BaseDataAccess<Document>, IDocumentDataAccess
    {
        public DocumentDataAccess(string conStr) : base(conStr)
        {

           
        }

        public async Task<int> CreateAsync(Document document)
        {
           
            return await ExecuteScalarAsync<int>(
                "insert Documents " +
                "(FileName, Type, FileSize, Content, Description, WorkflowId, CreatedDate, ModifiedDate, WebUrl) " +
                "output inserted.Id " +
                "values(@FileName, @Type, @FileSize, @Content, @Description, @WorkflowId, getDate(), getDate(), @WebUrl" +
                ")", document);
        }
    }
}
