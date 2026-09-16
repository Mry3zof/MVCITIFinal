namespace ProjectName.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string? ImagePath { get; set; }
        public bool IsApproved { get; set; } = true;
    }
}