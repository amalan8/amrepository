using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ClassApp.Repository
{

		public interface IClassRepository
		{
			ClassModel[] Classes { get; }
		ClassModel GetClass(int classId);

			ClassModel Class1(int classID);

		ClassModel[] ForUser(int userID);

			
		}




	public class ClassModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

	}

	public class ClassRepository : IClassRepository
	{
		public ClassModel[] Classes
		{
			get
			{
				return Database.DatabaseAccessor.Instance.Class
					.Select(t => new ClassModel
					{
						Id = t.ClassId,
						Name = t.ClassName,
						Description = t.ClassDescription,
						Price = t.ClassPrice
					}).ToArray();
			}
		}

		public ClassModel Class1(int classID)
		{
			var class1 = Database.DatabaseAccessor.Instance.Class
				.Where(t => t.ClassId == classID)
				.Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName })
				.First();

			return class1;
		}

		public ClassModel[] ForUser(int userId)
		{
			var classes = Database.DatabaseAccessor.Instance.UserClass.Where(t => t.UserId == userId);

			return classes
					.Select(t => new ClassModel
					{
						Id = t.ClassId,
						Name = t.Class.ClassName,
						Description = t.Class.ClassDescription,
						Price = t.Class.ClassPrice
					})
					.ToArray();
		}

		public ClassModel GetClass(int classId)
		{

			return Database.DatabaseAccessor.Instance.Class
						.Where(t => t.ClassId == classId)
						.Select(t => new ClassModel
						{
							Id = t.ClassId,
							Name = t.ClassName,
							Price = t.ClassPrice
						
						})
						.First(); 
		}

	}
}
