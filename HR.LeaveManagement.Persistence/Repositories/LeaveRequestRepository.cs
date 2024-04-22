using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await _context.LeaveRequest
                .Include(q => q.LeaveType)
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            return await _context.LeaveRequest
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveType = _context.LeaveRequest
                .Include (q => q.LeaveType)
                .FirstOrDefault(q => q.Id == id);
            return leaveType;
        }
    }
}
