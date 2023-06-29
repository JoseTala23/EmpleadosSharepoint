namespace EmpleadosSharepoint;

public interface IEmployee
{
    public Task<List<Employee>> GetEmployeesAsync();
}