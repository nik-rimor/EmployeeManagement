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
                new Employee { Id = 1, Name = "Maria", Department = Dept.HR, Email = "maria@testdom.test" },
                new Employee { Id = 2, Name = "Spyros", Department = Dept.IT, Email = "spyros@testdom.test" },
                new Employee { Id = 3, Name = "Gina", Department = Dept.IT, Email = "gina@testdom.test" },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employeeFromDb = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employeeFromDb != null)
            {
                employeeFromDb.Name = employeeChanges.Name;
                employeeFromDb.Email = employeeChanges.Email;
                employeeFromDb.Department = employeeChanges.Department;
            }
            return employeeFromDb;
        }
    }
}
