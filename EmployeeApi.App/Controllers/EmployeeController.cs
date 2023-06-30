using System.Text.Json;
using EmpleadosSharepoint;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public Task<IActionResult> EmployeeGetAll()
    {
        var ok = _employeeDB.GetEmployeesAsync();
        var result = JsonSerializer.Serialize(ok, new JsonSerializerOptions { WriteIndented = true });
        
        return Task.FromResult<IActionResult>(Ok(result));
    }

    [Route("employee/getAllByUPN/{upnEmployee}")]
    [HttpGet]
    [Authorize]
    public Task<IActionResult> GetEmployeeByUPN([FromRoute] string upnEmployee)
    {
        var ok = _employeeDB.GetEmployeesByUPNAsync(upnEmployee);
        var result = JsonSerializer.Serialize(ok, new JsonSerializerOptions { WriteIndented = true });

        return Task.FromResult<IActionResult>(Ok(result));
    }
}