using HR.LeaveManagement.Application.Model.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
	public interface IUserService
	{
		Task<List<Employee>> GetEmployees();
		Task<Employee> GetEmployee(string userId);
	}
}
