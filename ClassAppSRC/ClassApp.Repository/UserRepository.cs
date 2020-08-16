using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClassApp.Database;

namespace ClassApp.Repository
{


	public interface IUserRepository
	{
		UserModel Login(string email, string password);

		UserModel Register(string email, string password);
		
	}

	public class UserModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
	}


	public class UserRepository : IUserRepository
	{
		public UserModel Login(string email, string password)
		{
			var user = DatabaseAccessor.Instance.User
				.FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower() &&
				t.UserPassword == password);

			if (user == null)
			{
				return null;
			}

			return new UserModel { Id = user.UserId, Email = user.UserEmail, IsAdmin = user.UserIsAdmin };
		}

		public UserModel Register(string email, string password)
		{


			var user = DatabaseAccessor.Instance.User.Add(new ClassApp.Database.User
			{
				UserEmail = email,
				UserPassword = password,
				UserIsAdmin = false

			});


			DatabaseAccessor.Instance.SaveChanges();

			return new UserModel { Id = user.Entity.UserId, Email = user.Entity.UserEmail, IsAdmin = user.Entity.UserIsAdmin };
		}



	}
}
