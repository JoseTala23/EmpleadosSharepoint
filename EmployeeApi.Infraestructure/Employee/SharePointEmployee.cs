using System.Security;
using EmpleadosSharepoint.Options;
using Microsoft.Extensions.Options;
using Microsoft.SharePoint.Client;


namespace EmpleadosSharepoint;

public class SharePointEmployee : IEmployee
{
    private readonly IOptionsMonitor<SharepointOptions> _sharepointOptions;

    public SharePointEmployee(IOptionsMonitor<SharepointOptions> sharepointOptions)
    {
        _sharepointOptions = sharepointOptions;
    }
    public async Task<List<Employee>> GetEmployeesAsync()
    {
        using var clientContext = new ClientContext(_sharepointOptions.CurrentValue.Url);
        clientContext.Credentials = new SharePointOnlineCredentials(
            _sharepointOptions.CurrentValue.User,
            _sharepointOptions.CurrentValue.Pass);

        var oList = clientContext.Web.Lists.GetByTitle(_sharepointOptions.CurrentValue.ListName);

        var camlQuery = new CamlQuery
        {
            ViewXml = "<View><RowLimit>100</RowLimit></View>"
        };
        
        var collListItem = oList.GetItems(camlQuery);
                

        clientContext.Load(collListItem);
        
        await clientContext.ExecuteQueryAsync();

        var result = new List<Employee>();
        var boss = new Dictionary<string, string>();


        foreach (var oListItem in collListItem)
        {
            if (oListItem.FieldValues["Responsable"] != null &&
                oListItem.FieldValues["Responsable"].GetType() == typeof(FieldLookupValue))
            {
                var lookup = oListItem.FieldValues["Responsable"] as FieldLookupValue;
                boss.Add(oListItem.FieldValues["UPN"].ToString(), lookup.LookupValue);
            }
            result.Add(new Employee(oListItem.FieldValues["UPN"].ToString() ?? Guid.NewGuid().ToString())
            {
                Name = oListItem.FieldValues["Title"].ToString() ?? "",
                Surname = oListItem.FieldValues["Apellidos"].ToString() ?? "",
                BirthDate = DateTime.Parse(oListItem.FieldValues["FechaNacimiento"].ToString()),

            });

        }

        foreach (var (user, bossUser) in boss)
        {
            var userEmployee = result.FirstOrDefault(e => e.Upn == user);
            if (userEmployee != null)
            {
                userEmployee.Boss = result.FirstOrDefault(e => e.Upn == bossUser);
            }

        }

        return result;
    }

    public async Task<Employee> GetEmployeesByUPNAsync(string upnFilter)
    {
        using var clientContext = new ClientContext(_sharepointOptions.CurrentValue.Url);
        clientContext.Credentials = new SharePointOnlineCredentials(
            _sharepointOptions.CurrentValue.User,
            _sharepointOptions.CurrentValue.Pass);

        var oList = clientContext.Web.Lists.GetByTitle(_sharepointOptions.CurrentValue.ListName);

        
        var camlQuery = new CamlQuery
        {
            ViewXml = $"<View><Query><Where><Eq><FieldRef Name='UPN' /><Value Type='Text'>{upnFilter}</Value></Eq></Where></Query><RowLimit>100</RowLimit></View>"
        };

        var collListItem = oList.GetItems(camlQuery);


        clientContext.Load(collListItem);

        await clientContext.ExecuteQueryAsync();

        Employee result = null;
        var boss = new Dictionary<string, string>();


        foreach (var oListItem in collListItem)
        {
            if (oListItem.FieldValues["Responsable"] != null &&
                oListItem.FieldValues["Responsable"].GetType() == typeof(FieldLookupValue))
            {
                var lookup = oListItem.FieldValues["Responsable"] as FieldLookupValue;
                boss.Add(oListItem.FieldValues["UPN"].ToString(), lookup.LookupValue);
            }
            result = new Employee(oListItem.FieldValues["UPN"].ToString() ?? Guid.NewGuid().ToString())
            {
                Name = oListItem.FieldValues["Title"].ToString() ?? "",
                Surname = oListItem.FieldValues["Apellidos"].ToString() ?? "",
                BirthDate = DateTime.Parse(oListItem.FieldValues["FechaNacimiento"].ToString()),

            };

        }
                

        return result;
    }
}