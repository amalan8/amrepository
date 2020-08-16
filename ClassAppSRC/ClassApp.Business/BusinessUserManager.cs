using System;
using System.Collections.Generic;
using System.Text;
using ClassApp.Repository;

namespace ClassApp.Business
{
	public interface IUserManager
	{
		BusinessUserModel LogIn(string email, string password);
		BusinessUserModel Register(string email, string password);
///*		BusinessUserModel User(int userId)*/;
	}

	public class BusinessUserModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
	}

	public class UserManager : IUserManager
	{
		private readonly IUserRepository userRepository;
		public UserManager(IUserRepository userRepostory)
		{
			this.userRepository = userRepostory;
		}

		public BusinessUserModel LogIn(string email, string password)
		{
			var user = userRepository.Login(email, password);

			if (user == null)
			{
				return null;
			}

			return new BusinessUserModel { Id = user.Id, Email = user.Email };
		}

		public BusinessUserModel Register(string email, string password)
		{
			var user = userRepository.Register(email, password);

			if (user == null)
			{
				return null; 
			}

			return new BusinessUserModel { Id = user.Id, Email = user.Email };
		}
		//public BusinessUserModel User(int userId)
		//{
		//	var userModel = userRepository.;
		//	return new CategoryModel(categoryModel.Id, categoryModel.Name);
		//}

	}



}
