using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class HrDatabaseContextTests
    {
        private readonly HrDatabaseContext _hrDatabaseContext;

        public HrDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDatabaseContext = new HrDatabaseContext(dbOptions);
        }

        [Fact]
        public async Task Save_SetDateCreatedValueAsync()
        {
            //Arrange
            var leaveType = new LeaveType()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test vacation"
            };

            //Act
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async Task Save_SetDateModifiedValueAsync()
        {
            //Arrange
            var leaveType = new LeaveType()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test vacation"
            };

            //Act
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            //Assert
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}