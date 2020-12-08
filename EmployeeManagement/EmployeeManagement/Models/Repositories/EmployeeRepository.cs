using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public EmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1, Name="Amol", Department=Dept.IT, Email="amol@abc.com"},
                new Employee(){Id=2, Name="Carol", Department=Dept.HR, Email="carol@abc.com"},
            };
        }

        public Employee AddEmployee(Employee emp)
        {
            emp.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(emp);
            return emp;
        }

        public void DeleteEmployee(int Id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == Id);

            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
        }

        public Employee EditEmployee(Employee empChnages)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == empChnages.Id);

            if (employee != null)
            {
                employee.Name = empChnages.Name;
                employee.Email = empChnages.Email;
                employee.Department = empChnages.Department;
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
