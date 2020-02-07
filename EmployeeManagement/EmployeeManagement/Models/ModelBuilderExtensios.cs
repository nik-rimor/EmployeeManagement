using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensios
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, Name = "Maria", Department = Dept.HR, Email = "maria@testdom.test" },
                    new Employee { Id = 2, Name = "Spyros", Department = Dept.IT, Email = "spyros@testdom.test" },
                    new Employee { Id = 3, Name = "Gina", Department = Dept.IT, Email = "gina@testdom.test" }
                );
        }
    }
}
