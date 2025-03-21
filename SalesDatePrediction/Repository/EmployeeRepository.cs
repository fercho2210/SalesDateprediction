using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MiDbContext _context;

        public EmployeeRepository(MiDbContext context)
        {
            _context = context;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            return _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmpId = e.Empid,
                    FullName = e.Firstname + " " + e.LastName
                })
                .ToList();
        }
    }
}
