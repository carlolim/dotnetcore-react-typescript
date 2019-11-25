using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using FastMember;
using LambdaToSql;
using Microsoft.Extensions.Options;
using Quality.Common;
using Quality.Common.AppSettings;

namespace Quality.DataAccess
{
    public class GenericBase<T> : IGenericBase<T> where T : class, new()
    {
        private readonly ExpressionTranslator _expressionTranslator;
        public string ConnectionString { get; set; }
        public GenericBase(IOptions<DatabaseConnections> connectionStrings)
        {
            _expressionTranslator = new ExpressionTranslator();
            ConnectionString = connectionStrings.Value.MainDatabase;
        }

        //all(ok), by id(ok), insert(ok), update(ok), delete(ok), bulk insert, bulk update, bulk delete
        public async Task<IEnumerable<T>> All()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
                return await db.GetAllAsync<T>();
        }

        public async Task<T> ById(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
                return await db.GetAsync<T>(id);
        }

        public async Task<Result> SoftDeleteAsync(int id)
        {
            var entity = new T();
            var entityDetails = entity.GetEntityDetails();
            var sqlQuery = $"UPDATE [{entityDetails.TableName}] SET [IsDeleted]=1 WHERE [{entityDetails.PrimaryKeyName}]=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.ExecuteAsync(sqlQuery, new { id });
                return new Result()
                {
                    IsSuccess = result > 0
                };
            }
        }

        public async Task<Result> Insert(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.InsertAsync(data);
                return new Result { Id = result, IsSuccess = result > 0 };
            }
        }

        public async Task<Result> Update(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.UpdateAsync(data);
                return new Result { IsSuccess = result };
            }
        }

        public async Task<Result> Delete(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.DeleteAsync(data);
                return new Result { IsSuccess = result };
            }
        }

        public Result BulkInsert(List<T> data)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllFilteredBy(Expression<Func<T, bool>> condition)
        {
            var entity = new T();
            var entityDetails = entity.GetEntityDetails();
            var translatedCondition = _expressionTranslator.TranslateToQueryResult(condition);
            var sqlQuery = $"SELECT * FROM [{entityDetails.TableName}] WHERE {translatedCondition.SqlQueryParameterized}";

            using (IDbConnection db = new SqlConnection(ConnectionString))
                return (await db.QueryAsync<T>(sqlQuery, translatedCondition.Parameters)).ToList();
        }

        public async Task<T> GetSingleFilteredByAsync(Expression<Func<T, bool>> condition)
        {
            var entity = new T();
            var entityDetails = entity.GetEntityDetails();
            var translatedCondition = _expressionTranslator.TranslateToQueryResult(condition);
            var sqlQuery = $"SELECT * FROM [{entityDetails.TableName}] WHERE {translatedCondition.SqlQueryParameterized}";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return await db.QueryFirstOrDefaultAsync<T>(sqlQuery, translatedCondition.Parameters);
            }
        }

        public async Task<Result> InsertBulkAsync(List<T> entities)
        {
            var result = new Result();
            var sampleEntity = new T();
            var entityDetails = sampleEntity.GetEntityDetails();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var copyParameters = sampleEntity.ToArrayOfNames();
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null))
                {
                    foreach (var column in copyParameters)
                    {
                        if (column != entityDetails.PrimaryKeyName)
                            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column, column));
                    }

                    bulkCopy.DestinationTableName = entityDetails.TableName;
                    bulkCopy.BatchSize = 500;
                    using (var reader = ObjectReader.Create(entities, copyParameters))
                    {
                        await bulkCopy.WriteToServerAsync(reader);
                        result.IsSuccess = true;
                    }
                }
                connection.Close();
            }
            return result;
        }
    }
}
