using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployee(int Id);
        public IEnumerable<Employee> GetAllEmployee();
        public Employee Add(Employee employee);
        public Employee Update(Employee employeeChanges);
        public Employee Delete(int id);
    }
}
