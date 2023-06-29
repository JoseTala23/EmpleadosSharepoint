using System.Text.Json;
using EmpleadosSharepoint;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class EmployeeController:Controller
{

    private readonly IEmployee _employeeDB;
    
    public EmployeeController(IEmployee employeeDb)
    {
        _employeeDB = employeeDb;
    }

    
    [Route("employee/getAll")]
    [HttpGet]
    public Task<IActionResult> EmployeeGetAll()
    {
        var ok = _employeeDB.GetEmployeesAsync();
        var result = JsonSerializer.Serialize(ok, new JsonSerializerOptions { WriteIndented = true });
        
        return Task.FromResult<IActionResult>(Ok(result));
    }

    [Route("employee/getAllByUPN/{upnEmployee}")]
    [HttpGet]
    public Task<IActionResult> GetEmployeeByUPN()
    {
        var ok = _employeeDB.GetEmployeesAsync();
        var result = JsonSerializer.Serialize(ok, new JsonSerializerOptions { WriteIndented = true });

        return Task.FromResult<IActionResult>(Ok(result));
    }
}