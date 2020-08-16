using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClassApp.Database;
using System.Data.SqlClient;

namespace ClassApp.Repository
{
    public interface IUserClassRepository
    {
        UserClassModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        UserClassModel[] GetAll(int userId);
    }

    public class UserClassModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
    }

    public class UserClassRepository : IUserClassRepository
    {
        public UserClassModel Add(int userId, int classId)
        {
            var item = DatabaseAccessor.Instance.UserClass.Add(
                new ClassApp.Database.UserClass
                {
                    ClassId = classId,
                    UserId = userId,
                });

            try
            {
                DatabaseAccessor.Instance.SaveChanges();
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new System.ArgumentException("You have already added this class.");
                }
            }

            return new UserClassModel
            {
                UserId = item.Entity.UserId,
                ClassId = item.Entity.ClassId
               
            };
        }

        public UserClassModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.UserClass
                .Where(t => t.UserId == userId)
                .Select(t => new UserClassModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                    
                })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int classId)
        {
            var items = DatabaseAccessor.Instance.UserClass
                                .Where(t => t.UserId == userId && t.ClassId == classId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.UserClass.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }
}
