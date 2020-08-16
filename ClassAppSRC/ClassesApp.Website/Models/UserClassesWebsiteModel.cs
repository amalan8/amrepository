using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesApp.Website.Models
{
	public class UserClassesWebsiteModel
	{
		public int UserID { get; set; }
		public int ClassID { get; set; }
		public string ClassName { get; set; }
		public decimal ClassPrice { get; set; }
		public UserWebsiteModel User { get; set; }
		public ClassWebsiteModel[] Classes { get; set; }
	}
}
