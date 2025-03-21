using SalesDatePrediction.Dto;

namespace SalesDatePrediction.Repository
{
    public interface IEmployeeRepository
    {
        List<EmployeeDto> GetAllEmployees();
    }
}
