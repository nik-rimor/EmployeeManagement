using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            {
                new Employee { Id = 1, Name = "Maria", Department = "HR", Email = "maria@testdom.test" },
                new Employee { Id = 2, Name = "Spyros", Department = "IT", Email = "spyros@testdom.test" },
                new Employee { Id = 3, Name = "Gina", Department = "IT", Email = "gina@testdom.test" },
            };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
