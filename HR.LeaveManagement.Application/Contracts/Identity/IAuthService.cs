﻿using HR.LeaveManagement.Application.Model.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
	public interface IAuthService
	{
		Task<AuthResponse> Login(AuthRequest request);
		Task<RegistrationResponse> Register(RegistrationRequest request);
	}
}
