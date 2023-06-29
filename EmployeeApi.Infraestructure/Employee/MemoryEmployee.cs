namespace EmpleadosSharepoint;

public class MemoryEmployee : IEmployee
{
    private static readonly Employee Jose = new Employee("jose.talaveron@employee.com")
    {
        Name = "Jose",
        Surname = "Talaveron",
        BirthDate = new DateTime(1987, 05, 01),

    };

    private static readonly Employee Amian = new Employee("antonio.lopez@employee.com")
    {
        Name = "antonio",
        Surname = "lopez",
        BirthDate = new DateTime(1984, 08, 30),
        Boss = Jose
    };

    private static readonly List<Employee> Employees = new List<Employee>() { Jose, Amian };

    public Task<List<Employee>> GetEmployeesAsync()
    {
        
        return Task.FromResult(Employees);
    }

    public Task<Employee> GetEmployeesByUPNAsync(string upnFilter)
    {
        throw new NotImplementedException();
    }
}