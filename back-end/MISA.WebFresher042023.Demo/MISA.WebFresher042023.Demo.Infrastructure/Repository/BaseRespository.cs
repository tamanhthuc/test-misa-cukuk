
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MISA.WebFresher042023.Demo.Core.Common;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Respond;
using MySqlConnector;

namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{
    public abstract class BaseRespository<TEntity> : IBaseRepository<TEntity>
    {

        protected readonly string? _connectionString;

        public BaseRespository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;

            string procName = String.Format(Procedures.DELETE, tableName);

            var mySqlConnection = new MySqlConnection(_connectionString);

            var parameters = new DynamicParameters();

            parameters.Add("id", id);

            var result = await mySqlConnection.ExecuteAsync(procName, parameters, commandType: System.Data.CommandType.StoredProcedure);



            return result;
        }

        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();

                using (var transaction =  mySqlConnection.BeginTransaction()) {
                    try
                    {
                         var tableName = typeof (TEntity).Name; 

                        DynamicParameters dynamicParameters = new DynamicParameters();

                        dynamicParameters.Add("@ids", ids);

                        var sqlQuery = $"DELETE FROM {tableName} WHERE {tableName}Id IN @ids";

                        var result = await mySqlConnection.ExecuteAsync(sqlQuery, dynamicParameters, transaction);

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex) { 
                        // Rollback transaction neu co loi xay ra
                        transaction.Rollback();

                        // Xử lý lỗi
                        throw;
                    }
                }

            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var tableName = typeof(TEntity).Name;

            string procName = String.Format(Procedures.GET_ALL, tableName);

            var mySqlConnection = new MySqlConnection(_connectionString);

            var results = await mySqlConnection.QueryAsync<TEntity>(procName, commandType: System.Data.CommandType.StoredProcedure);

            return results.ToList();
        }

        public async Task<TEntity?> GetAsync(Guid id)
        {

            var tableName = typeof(TEntity).Name;

            var procName = String.Format(Procedures.GET_BY_ID, tableName);

            var mySqlConnection = new MySqlConnection(_connectionString);

            var parameters = new DynamicParameters();

            parameters.Add("id", id);

            var entityDto = await mySqlConnection.QueryFirstOrDefaultAsync<TEntity>(procName, parameters, commandType: CommandType.StoredProcedure);


            return entityDto;
        }


        public virtual async Task<int?> InsertAsync(TEntity entity)
        {
           
            var tableName = typeof(TEntity).Name;

            var mySqlConnection = new MySqlConnection(_connectionString);

            var parameters = new DynamicParameters();

            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name.Contains($"{tableName}Id"))
                {
                    parameters.Add($"{tableName}Id", Guid.NewGuid());
                }
                else if (prop.Name.Contains("CreatedDate"))
                {
                    parameters.Add($"@CreatedDate", DateTime.Now);
                }
                else
                {
                    parameters.Add($"@{prop.Name}", prop.GetValue(entity));
                }
            }
            var result = await mySqlConnection.ExecuteAsync($"Proc_{tableName}_Insert", parameters, commandType: CommandType.StoredProcedure);

            return result;

        }

        public virtual async Task<int> UpdateAsync(Guid id, TEntity entity)
        {

            var tableName = typeof(TEntity).Name;

            DynamicParameters parameters = new DynamicParameters();

            var mySqlConnection = new MySqlConnection(_connectionString);

            foreach (var prop in entity.GetType().GetProperties())
            {

                parameters.Add($"@p_{prop.Name}", prop.GetValue(entity, null));

            }

            var result = await mySqlConnection.ExecuteAsync($"Proc_{tableName}_Update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

      

    }
}
