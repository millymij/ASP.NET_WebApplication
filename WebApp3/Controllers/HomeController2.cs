using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

// This class provides ASP.NET Implementation of MVC controller for CRUD operations.
// Provides simple endpoints to return hard-coded name and phone number for demonstration purposes.

namespace RecordWebsite.Controllers
{
    public class HomeController2 : ControllerBase
    {
        private readonly DataContext _context;

        // constructor initialising controller with instance of DataContext 
        public HomeController2(DataContext context)
        {
            _context = context;
        }

        // retrieve a single Company entity by its ID
        [HttpGet]
        public async Task<ActionResult<Company>> Get(int companyId)
        {
            var company = await _context.Companies.Include(c => c.AddressList).ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // retrieve all 'company' entities
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAllCompanies()
        {
            var companies = await _context.Companies.AsNoTracking().Include(c => c.AddressList).ThenInclude(c => c.Country).ToListAsync();
            return Ok(companies);
        }

        // create company
        [HttpPost]
        public async Task<ActionResult<Company>> Create([FromBody] Company company)
        {
            var result = _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(result.Entity);
        }

        // retrieve company.
        [HttpGet]
        public ActionResult GetName()
        {
            var obj = new
            {
                name = "cami"
            };
            return Ok(obj);
        }


        // retrieve company phone
        [HttpGet]
        public ActionResult GetPhone()
        {
            var obj = new
            {
                phone = "123456789"
            };
            return Ok(obj);
        }

        // delete company.
        [HttpDelete]
        public async Task<ActionResult> Delete(int companyId)
        {
            var company = await _context.Companies.Include(c => c.AddressList).ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok(company);
        }

        // update company
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Company company)
        {
            try
            {
                var result = await _context.Companies
                    .Include(c => c.AddressList)
                    .ThenInclude(a => a.Country)
                    .FirstOrDefaultAsync(c => c.CompanyId == company.CompanyId);
                if (result == null)
                {
                    return NotFound();
                }
                result.Name = company.Name;
                result.Phone = company.Phone;
                foreach (var address in result.AddressList)
                {
                    var updatedAddress = company.AddressList.FirstOrDefault(a => a.AddressId == address.AddressId);
                    if (updatedAddress != null)
                    {
                        address.StreetAddress1 = updatedAddress.StreetAddress1;
                        address.StreetAddress2 = updatedAddress.StreetAddress2;
                        address.Region = updatedAddress.Region;
                        address.PostCode = updatedAddress.PostCode;
                        address.Country.Name = updatedAddress.Country.Name;
                    }
                }
                await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return StatusCode(500, "An error occurred while updating the company.");
            }
        }
    }
}
