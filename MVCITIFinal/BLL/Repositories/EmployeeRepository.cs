using System.Collections.Generic;
using System.Linq;
using ProjectName.BLL.Interfaces;
using ProjectName.DAL.Database;
using ProjectName.DAL.Entities;

namespace ProjectName.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll() => _context.Employees.ToList();

        public Employee GetById(int id) => _context.Employees.Find(id);

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
        }

        public void Reject(int id)
        {
            var emp = GetById(id);
            if (emp != null)
            {
                emp.IsApproved = false;
                _context.Employees.Update(emp);
                _context.SaveChanges();
            }
        }
    }
}