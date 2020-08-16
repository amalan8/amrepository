using System;
using System.Collections.Generic;
using System.Text;
using ClassApp.Repository;
using System.Linq;

namespace ClassApp.Business
{
    public interface IUserClassManager
    {
        BusinessUserClassModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        BusinessUserClassModel[] GetAll(int userId);
    }

    public class BusinessUserClassModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }

    public class UserClassManager : IUserClassManager
    {
        private readonly IUserClassRepository userClassRepository;
        private readonly IClassRepository classRepository;

        public UserClassManager(IUserClassRepository userClassRepository, IClassRepository classRepository)
        {
            this.userClassRepository = userClassRepository;
            this.classRepository = classRepository;
        }

        public BusinessUserClassModel Add(int userId, int classId)
        {
            var item = userClassRepository.Add(userId, classId);
 
            return new BusinessUserClassModel
            {
                ClassId = item.ClassId,
                UserId = item.UserId 
                
            };
        }

        public BusinessUserClassModel[] GetAll(int userId)
        {
            var items = userClassRepository.GetAll(userId)
                .Select(t =>  
                {
                    var class1 = classRepository.GetClass(t.ClassId);

                    return new BusinessUserClassModel
                    {
                        UserId = t.UserId,
                        ClassId = t.ClassId,
                        ClassName = class1.Name
                    };
      
                })
                .ToArray();

            return items;
        }

        public bool Remove(int userId, int classId)
        {
            return userClassRepository.Remove(userId, classId);
        }
    }
}
