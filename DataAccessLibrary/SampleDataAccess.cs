using Microsoft.Extensions.Caching.Memory;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<EmployeeModel> GetEmployees()
        {
            Thread.Sleep(3000);

            return new List<EmployeeModel>()
            {
                new EmployeeModel{ FirstName = "Andei", LastName = "Bagan" },
                new EmployeeModel{ FirstName = "Vadim", LastName = "Vadya" },
                new EmployeeModel{ FirstName = "Denis", LastName = "Myasnik" }
            };
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            await Task.Delay(3000);

            return new List<EmployeeModel>()
            {
                new EmployeeModel{ FirstName = "Andei", LastName = "Bagan" },
                new EmployeeModel{ FirstName = "Vadim", LastName = "Vadya" },
                new EmployeeModel{ FirstName = "Denis", LastName = "Myasnik" }
            };
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            var employees = _memoryCache.Get<List<EmployeeModel>>("employees");

            if (employees is null)
            {
                employees = await GetEmployeesAsync();
                _memoryCache.Set("employees", employees, TimeSpan.FromMinutes(1));
            }

            return employees;
        }
    }
}
