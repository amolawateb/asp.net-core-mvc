using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SqlEmployeeRepo : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SqlEmployeeRepo(AppDbContext context)
        {
            this.context = context;
        }

        public AppDbContext Context { get; }

        public Employee AddEmployee(Employee emp)
        {
            context.Employees.Add(emp);
            context.SaveChanges();
            return emp;
        }

        public void DeleteEmployee(int Id)
        {
            Employee emp = context.Employees.Find(Id);
            if (emp !=null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
        }

        public Employee EditEmployee(Employee empChnages)
        {
            var emp = context.Employees.Attach(empChnages);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return empChnages;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }
    }
}
