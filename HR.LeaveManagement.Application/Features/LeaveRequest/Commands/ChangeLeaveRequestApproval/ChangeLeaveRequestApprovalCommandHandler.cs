using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Model;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommand> _logger;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender, IAppLogger<ChangeLeaveRequestApprovalCommand> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _logger = logger;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            ChangeLeaveRequestApprovalCommandValidator validations = new ChangeLeaveRequestApprovalCommandValidator();
            var validationResult = validations.Validate(request);
            if (validationResult.Errors.Any())
                throw new BadRequestException(nameof(ChangeLeaveRequestApprovalCommand), validationResult);

            Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            leaveRequest.Approved = true;

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                EmailMessage emailMessage = new EmailMessage
                {
                    To = string.Empty,
                    Body = "Your leave request has been approved.",
                    Subject = "Leave Request Approved."
                };

                await _emailSender.SendEmail(emailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message, ex);
            }

            return Unit.Value;
        }
    }
}
