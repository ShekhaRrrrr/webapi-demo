using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapicrud.Data;
using webapicrud.Models.Entities;

namespace webapicrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAllEmployeesById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound("Please Enter Correct Details");
            }
            return Ok(employee);

        }

        [HttpPost]

        public IActionResult AddEmployees(AddEmployeeDto employeeDto)
        {
            var employeedata = new Employee()
            {
                Email = employeeDto.Email,
                Salary = employeeDto.Salary,
                Phone = employeeDto.Phone,
                Name = employeeDto.Name,


            };
            dbContext.Add(employeedata);
            dbContext.SaveChanges();
            return Ok(employeedata);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateEmployees(Guid id, UpdateEmployeeDto updatedto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Enter the Correct Details");
            }
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Please Select A Employee");
            }
            employee.Name = updatedto.Name;
            employee.Email = updatedto.Email;
            employee.Salary = updatedto.Salary;
            employee.Phone = updatedto.Phone;

            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);

        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("please select data accordingly");
            }
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return Ok(employee);

        }
    }
}

    

    
