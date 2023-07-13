using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MISA.WebFresher042023.Demo.Core.Common;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Respond;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.VBA;
using static Dapper.SqlMapper;


namespace MISA.WebFresher042023.Demo.Infrastructure.Repository
{

    public class EmployeeRespository : BaseRespository<Employee>, IEmployeeRepository
    {

        public EmployeeRespository(IConfiguration configuration) : base(configuration)
        {
        }


        public async Task<PageResult<Employee>> GetListAsync(int pageSize, int pageNumber, string? textSearch)
        {
            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("PageSize", pageSize);
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("TextSearch", textSearch);
                parameters.Add("TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);


                var result = await mySqlConnection.QueryAsync<Employee>("Proc_Employee_GetFilter", parameters, commandType: CommandType.StoredProcedure);
                var totalRecord = parameters.Get<int>("@TotalRecord");

                var currentPageRecords = 0;
                if (pageNumber < Math.Ceiling((decimal)totalRecord / pageSize))
                {
                    currentPageRecords = pageSize;
                }
                else if (pageNumber == Math.Ceiling((decimal)totalRecord / pageSize))
                {
                    currentPageRecords = totalRecord - (pageNumber - 1) * pageSize;
                }

                return new PageResult<Employee>
                {
                    TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                    TotalRecord = totalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = currentPageRecords,
                    Data = result.ToList()
                };
            }
        }

        public async Task<string> GetMaxCodeEmployee()
        {
            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT e.EmployeeCode FROM Employee e WHERE e.EmployeeCode LIKE 'NV-%'";


                var employeeCodes = await mySqlConnection.QueryAsync<string>(sqlQuery);

                var result = new List<string>(employeeCodes);

                List<int> list = new List<int>();


                foreach (var item in result)
                {
                    var stringNumber = item.Split('-')[1];
                    list.Add(int.Parse(stringNumber));
                }

                int maxEmployeeCode = 0;
                foreach (var item in list)
                {
                    if (item > maxEmployeeCode)
                    {
                        maxEmployeeCode = item;
                    }
                }

                maxEmployeeCode++;

                string resultString = "NV-" + maxEmployeeCode;

                return resultString;
            }
        }

        public async Task<bool> IsDuplicatedAsync(string code, Guid? id)
        {

            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("@EmployeeCode", code);

                dynamicParameters.Add("@EmployeeId", id);

                string sqlQuery = "";

                int count;

                if (id == null)
                {
                    sqlQuery = "SELECT COUNT(*) FROM Employee WHERE EmployeeCode = @EmployeeCode";

                    count = await mySqlConnection.ExecuteScalarAsync<int>(sqlQuery, dynamicParameters);

                    return count > 0;
                }
                else
                {

                    sqlQuery = "SELECT COUNT(*) FROM Employee WHERE EmployeeCode = @EmployeeCode AND EmployeeId <> @EmployeeId";

                    count = await mySqlConnection.ExecuteScalarAsync<int>(sqlQuery, dynamicParameters);

                    return count > 0;
                }
            }
        }

    }

}
