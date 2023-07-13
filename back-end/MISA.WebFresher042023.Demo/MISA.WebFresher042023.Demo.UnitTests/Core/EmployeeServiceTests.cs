using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualBasic;
using MISA.WebFresher042023.Demo.Core.DTo;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Repository;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MISA.WebFresher042023.Demo.UnitTests.Core
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        [Test]
        public void  GetAsync_NullEntity_ThrowNotFoundException()
        {
            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employeeRepository = new FakeNullEmployeeRepository();

            var departmenRepository = Substitute.For<IDepartmentRespository>();

            var mapper = new FakeMapper();

            var employeeService = new EmployeeService(employeeRepository, departmenRepository, mapper);

            Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.GetAsync(id));
        }

        [Test]
        public async Task GetAsync_ValidEntity_ValidDto()
        {
            // Arrange

            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeDto = new EmployeeDto() { EmployeeId = id };    

            var employeeRepository = new FakeEmployeeRepository();

            var mapper = Substitute.For<IMapper>();

            var departmenRepository = Substitute.For<IDepartmentRespository>();

            mapper.Map<EmployeeDto>(Arg.Any<Employee>()).Returns(employeeDto);

            var employeeService = new EmployeeService(employeeRepository, departmenRepository, mapper);

            // Act
            var actualResutl = await employeeService.GetAsync(id);

            Assert.That(actualResutl, Is.EqualTo(employeeDto));

            mapper.Received(1).Map<EmployeeDto>(Arg.Any<Employee>());
        }

        [Test]
        public void DeleteAsync_NullEntity_ThrowNotFoundException()
        {
            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAsync(id).ReturnsNull();

            employeeRepository.DeleteAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();

            var departmenRepository = Substitute.For<IDepartmentRespository>();


            var employeeService = new EmployeeService(employeeRepository, departmenRepository, mapper);

            Assert.ThrowsAsync<NotFoundException>(async () => await employeeService.DeleteAsync(id));
        }

        [Test]
        public async Task DeleteAsync_ValidEntity_ValidDto()
        {
            // Arrange

            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();

            employeeRepository.GetAsync(id).Returns(employee);

            employeeRepository.DeleteAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();

            var departmenRepository = Substitute.For<IDepartmentRespository>();


            var employeeService = new EmployeeService(employeeRepository, departmenRepository, mapper);

            // Act
            var actualResutl = await employeeService.DeleteAsync(id);

            // Assert

            Assert.That(actualResutl, Is.EqualTo(1));

            await employeeRepository.Received(1).DeleteAsync(id);
        }

        [Test]

        public async Task InsertAsync_ValidEntity_CreatedNum()
        {

            // Arrange
            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var departmentRepository = Substitute.For<IDepartmentRespository>();

            var mapper = Substitute.For<IMapper>();

            var employeeCreatedDto = new EmployeeCreateDto();

            var employee = new Employee()
            {
                EmployeeId = id
            };

            // Act

            employeeRepository.IsDuplicatedAsync(employeeCreatedDto.EmployeeCode, id).Returns(false);

            departmentRepository.GetAsync(employeeCreatedDto.DepartmentId).Returns(new Department());

             mapper.Map<Employee>(Arg.Any<EmployeeCreateDto>()).Returns(employee);

            employeeRepository.InsertAsync(employee).Returns(1);

            var employeeService = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actualResult = await employeeService.InsertAsync(employeeCreatedDto);

            // Assert

            Assert.That(actualResult, Is.EqualTo(1));

            await employeeRepository.Received(1).InsertAsync(employee);

            
      
        }

        [Test]
        public async Task UpdateAsync_ValidEntity_UpdateNum()
        {
            // Arrange
            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var departmentId = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var departmentRepository = Substitute.For<IDepartmentRespository>();

            var mapper = Substitute.For<IMapper>();

            // Action

            employeeRepository.GetAsync(id).Returns(new Employee());

            employeeRepository.IsDuplicatedAsync(employeeUpdateDto.EmployeeCode, id).Returns(false);

            departmentRepository.GetAsync(employeeUpdateDto.DepartmentId).Returns(new Department());

            mapper.Map<Employee>(Arg.Any<EmployeeCreateDto>()).Returns(employee);

            var employeeSevice = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actutalResult = await employeeSevice.UpdateAsync(id, employeeUpdateDto);

            // Assert

            Assert.That(actutalResult, Is.EqualTo(1));

            await employeeRepository.Received(1).UpdateAsync(id, employee);


        }

        [Test]
        public async Task UpdateAsync_InValidDepartment_ThrowNotFoundException()
        {

            // Arrange
            var id = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var departmentId = Guid.Parse("c0875ce4-e82c-4157-81df-7801eacaa1a9");

            var employeeUpdateDto = new EmployeeUpdateDto();

            var employee = new Employee()
            {
                EmployeeId = id
            };

            var employeeRepository = Substitute.For<IEmployeeRepository>();

            var departmentRepository = Substitute.For<IDepartmentRespository>();

            var mapper = Substitute.For<IMapper>();

            // Action

            employeeRepository.GetAsync(id).Returns(new Employee());

            employeeRepository.IsDuplicatedAsync(employeeUpdateDto.EmployeeCode, id).Returns(true);

            departmentRepository.GetAsync(employeeUpdateDto.DepartmentId).Returns(new Department());

            mapper.Map<Employee>(Arg.Any<EmployeeCreateDto>()).Returns(employee);

            var employeeSevice = new EmployeeService(employeeRepository, departmentRepository, mapper);

            var actutalResult = await employeeSevice.UpdateAsync(id, employeeUpdateDto);

            // Assert

            Assert.ThrowsAsync<NotFoundException>(async () => await employeeSevice.UpdateAsync(id, employeeUpdateDto), "Id Khong ton tai");
        }
    }

}
