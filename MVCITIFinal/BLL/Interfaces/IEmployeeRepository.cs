using System.Collections.Generic;
using ProjectName.DAL.Entities;

namespace ProjectName.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        void Reject(int id);
    }
}