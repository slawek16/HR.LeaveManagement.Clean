using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mock
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypesRepository()
        {
            List<LeaveType> leaveTypes = new()
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 10,
                    Name = "Test vacation"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 10,
                    Name = "Test vacation"
                },
            };

            Mock<ILeaveTypeRepository> mockRepo = new();

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    return Task.FromResult(leaveTypes.First());
                });

            return mockRepo;
        }
    }
}
