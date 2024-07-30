using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using RecordWebsite.Controllers;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        private HomeController2 _controller;
        private DataContext _dataContext;

        [TestInitialize]
        public void Setup()
        {
            // Mock the DataContext
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dataContext = new DataContext(options);
            _controller = new HomeController2(_dataContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dataContext.Database.EnsureDeleted();
        }

        // testing Get method
        [TestMethod]
        public async Task Get_WithValidCompanyId_ReturnsCompany()
        {
            var companyId = 10;
            var company = new Company { CompanyId = companyId, Name = "Company XXX", Phone = "77777" };
            _dataContext.Companies.Add(company);
            await _dataContext.SaveChangesAsync();

            var result = await _controller.Get(companyId);

             // make sure the test returns http 200 ok
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Company));
            var returnedCompany = okResult.Value as Company;
            Assert.AreEqual(companyId, returnedCompany.CompanyId);
        }

        // test Get method with invalid company id
        [TestMethod]
        public async Task Get_WithInvalidCompanyId_ReturnsNotFound()
        {
            var companyId = 100;
            var result = await _controller.Get(companyId);
            // make sure it returns http 404
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        // test GetAllCompanies method
        [TestMethod]
        public async Task GetAllCompanies_ReturnsListOfCompanies()
        {
            // add new companies
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, Name = "Company 1", Phone = "1234567890" },
                new Company { CompanyId = 2, Name = "Company 2", Phone = "9876543210" },
            };
            _dataContext.Companies.AddRange(companies);
            await _dataContext.SaveChangesAsync();
           
            // call method
            var result = await _controller.GetAllCompanies();
            
            // assert
            // make sure returns 200 ok
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Company>));
            var returnedCompanies = okResult.Value as List<Company>;
            Assert.AreEqual(companies.Count, returnedCompanies.Count);
        }

        // test Create method
        [TestMethod]
        public async Task Create_WithValidCompany_ReturnsCreatedCompany()
        {
            var company = new Company { Name = "New Company", Phone = "1234567890" };
            
            var result = await _controller.Create(company);

            // make sure returns 200 ok
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Company));
            var createdCompany = okResult.Value as Company;
            Assert.AreEqual(company.Name, createdCompany.Name);
            Assert.AreEqual(company.Phone, createdCompany.Phone);
        }

        // test Delete method
        [TestMethod]
        public async Task Delete_WithValidCompanyId_ReturnsDeletedCompany()
        {
            var companyId = 1;
            var company = new Company { CompanyId = companyId, Name = "Test Company", Phone = "1234567890" };
            _dataContext.Companies.Add(company);
            await _dataContext.SaveChangesAsync();

            var result = await _controller.Delete(companyId);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Company));
            var deletedCompany = okResult.Value as Company;
            Assert.AreEqual(companyId, deletedCompany.CompanyId);
        }

        // test Delete method with invalid company id
        [TestMethod]
        public async Task Delete_WithInvalidCompanyId_ReturnsNotFound()
        {
            var companyId = 1;

            var result = await _controller.Delete(companyId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        // test Update method
        [TestMethod]
        public async Task Update_WithValidCompany_ReturnsUpdatedCompany()
        {
            var companyId = 1;
            var originalCompany = new Company { CompanyId = companyId, Name = "Original Company", Phone = "1234567890" };
            _dataContext.Companies.Add(originalCompany);
            await _dataContext.SaveChangesAsync();

            var updatedCompany = new Company { CompanyId = companyId, Name = "Updated Company", Phone = "9876543210" };

            var result = await _controller.Update(updatedCompany);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Company));
            var updatedResult = okResult.Value as Company;
            Assert.AreEqual(updatedCompany.Name, updatedResult.Name);
            Assert.AreEqual(updatedCompany.Phone, updatedResult.Phone);
        }

        // test Update method with invalid company
        [TestMethod]
        public async Task Update_WithInvalidCompany_ReturnsNotFound()
        {
            var companyId = 1;
            var company = new Company { CompanyId = companyId, Name = "Test Company", Phone = "1234567890" };

            var result = await _controller.Update(company);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
