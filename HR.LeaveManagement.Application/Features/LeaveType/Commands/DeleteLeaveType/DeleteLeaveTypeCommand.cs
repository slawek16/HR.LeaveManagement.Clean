﻿using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public record DeleteLeaveTypeCommand(int Id) : IRequest<Unit>;
}
