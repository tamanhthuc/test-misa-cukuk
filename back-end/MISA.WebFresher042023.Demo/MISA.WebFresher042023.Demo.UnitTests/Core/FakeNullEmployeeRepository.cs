using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.Respond;

namespace MISA.WebFresher042023.Demo.UnitTests.Core
{
    public class FakeNullEmployeeRepository : IEmployeeRepository
    {

        public Guid ActualId { get; set; }  

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ExportDataToExcel()
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> GetAsync(Guid id)
        {
            
            return Task.FromResult<Employee?>(null);
        }

        public Task<PageResult<Employee>> GetListAsync(int page, int pageSize, string? employeeFilter)
        {
            throw new NotImplementedException();
        }

        public Task<int?> InsertAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDuplicated(string code)
        {
            throw new NotImplementedException();
        }


        public Task<bool> IsDuplicatedAsync(string code, Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
