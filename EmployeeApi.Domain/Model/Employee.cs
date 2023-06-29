namespace EmpleadosSharepoint;

public class Employee
{
    public string Upn { get; init; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public Employee? Boss { get; set; }

    public Employee(string upn)
    {
        Upn = upn;
    }
}