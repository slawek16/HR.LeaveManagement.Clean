﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypes()
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

            return mockRepo;
        }
    }
}
