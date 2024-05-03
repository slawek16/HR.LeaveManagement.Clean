using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Model;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            CreateLeaveRequestCommandValidator validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException(nameof(CreateLeaveRequestCommand), validationResult);

            Domain.LeaveRequest leaveRequest = _mapper.Map<Domain.LeaveRequest>(request); 

            await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                EmailMessage emailMessage = new EmailMessage
                {
                    To = string.Empty,
                    Body = "Your leave request has been created.",
                    Subject = "Leave Request Created."
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
