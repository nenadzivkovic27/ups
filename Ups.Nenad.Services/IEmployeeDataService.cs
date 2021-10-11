using System.Collections.Generic;
using Ups.Nenad.DataTypes;

namespace Ups.Nenad.Services
{
    public interface IEmployeeDataService
    {
        User GetEmployee(int userId);

        User AddEmployee(User user);

        User EditEmployee(User user);

        void DeleteEmployee(User user);

        void DeleteEmployee(int userId);

        User SaveEmployee(User user);

        GetUsersResponse GetEmployees(int? page, string searchTerm = null);

    }
}