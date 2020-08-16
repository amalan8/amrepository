﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassesApp.Website.Models
{
	public class LoginWebsiteModel
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "User email")]

		public string UserEmail { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

	}
}
