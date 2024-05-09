﻿using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Model.Identity
{
	public class RegistrationRequest
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		[MinLength(6)]
		public string UserName { get; set; }
		[Required]
		[MinLength(6)]
		public string Password { get; set; }
	}
}