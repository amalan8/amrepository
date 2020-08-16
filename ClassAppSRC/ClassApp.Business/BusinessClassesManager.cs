using System;
using System.Collections.Generic;
using System.Text;
using ClassApp.Repository;
using System.Linq;

namespace ClassApp.Business
{

        public interface IClassesManager
        {
            BusinessClass1Model[] ForUser(int categoryId);
            BusinessClass1Model[] Classes { get; }

        }

        public class BusinessClass1Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

         }

    public class ClassesManager : IClassesManager
    {
        private readonly IClassRepository classRepository;

        public ClassesManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public BusinessClass1Model[] ForUser(int userId)
        {
            return classRepository.ForUser(userId).Select(t =>
                            new BusinessClass1Model
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Description = t.Description,
                                Price = t.Price

                            })
                            .ToArray();
        }

        public BusinessClass1Model[] Classes
        {
            get
            {
                return classRepository.Classes
                                         .Select(t => new BusinessClass1Model
                                         {
                                             Id = t.Id,
                                             Name = t.Name,
                                             Description = t.Description,
                                             Price = t.Price,
                                         })
                                         .ToArray();
            }

        }
    }
}



