namespace EmpleadosSharepoint;

public interface IEmployee
{
    public Task<List<Employee>> GetEmployeesAsync();

    public Task<Employee> GetEmployeesByUPNAsync(string upnFilter);
}