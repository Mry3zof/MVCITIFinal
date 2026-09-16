using Microsoft.AspNetCore.Http;

namespace ProjectName.Models
{
    public class EmployeeFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
    }
}