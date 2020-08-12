using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassesApp.Website.Models
{
	public class ClassWebsiteModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public ClassWebsiteModel(int id, string name, string description, decimal price)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
		}

	}
}
