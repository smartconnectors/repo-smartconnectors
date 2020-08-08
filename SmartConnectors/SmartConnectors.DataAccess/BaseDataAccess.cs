using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SmartConnectors.DataAccess.Contracts;
using SmartConnectors.DataAccess.SqlTemplates;
using SmartConnectors.Models;

namespace SmartConnectors.DataAccess
{
    public class BaseDataAccess<T> : IBaseDataAccess<T> where T : BaseEntity
    {

        protected string ConnectionString { get; }

        public BaseDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private SqlConnection _connection;
        protected SqlConnection Connection => _connection ?? (_connection = new SqlConnection(ConnectionString));

        public async Task<IEnumerable<T>> Get()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryAsync<T>(string.Format(SQLStatements.TemplateGetAll, typeof(T).Name.ToString()));
            }
        }

        public async Task<T> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryFirstOrDefaultAsync<T>(string.Format(SQLStatements.TemplateGetById, typeof(T).Name.ToString(), id));
            }
        }

        public async Task<IEnumerable<T>> GetByParent<T1>(int parentId) 
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryAsync<T>(string.Format(SQLStatements.TemplateGetBySingleParentId, typeof(T).Name.ToString(), typeof(T1).Name.ToString(), parentId));
            }
        }

        public async Task<IEnumerable<T>> GetByParent<T1, T2>(int parentId1, int parentId2)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var query = string.Format(SQLStatements.TemplateGetByMultiParentId, typeof(T).Name.ToString(), typeof(T1).Name.ToString(), parentId1, typeof(T2).Name.ToString(),  parentId2);
                return await db.QueryAsync<T>(query);
            }
        }

        public async Task<T> DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.ExecuteScalarAsync<T>(string.Format(SQLStatements.TemplateDelete, typeof(T).Name.ToString(), id));
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
        }


        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }


        public IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
