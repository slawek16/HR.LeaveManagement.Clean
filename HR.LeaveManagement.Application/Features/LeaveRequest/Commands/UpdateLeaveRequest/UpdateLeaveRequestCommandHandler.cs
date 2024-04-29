using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Model;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            UpdateLeaveRequestCommandValidator validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException(nameof(UpdateLeaveRequestCommand), validationResult);

            Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            _mapper.Map(request, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                EmailMessage emailMessage = new EmailMessage
                {
                    To = string.Empty,
                    Body = "Your leave request has been updated.",
                    Subject = "Leave Request Updated."
                };

                await _emailSender.SendEmail(emailMessage);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message, ex);
            }
            return Unit.Value;
        }
    }
}
