namespace SalesDatePrediction.Dto
{
    // Dtos/EmployeeDto.cs
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }= string.Empty;
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
    }
}
