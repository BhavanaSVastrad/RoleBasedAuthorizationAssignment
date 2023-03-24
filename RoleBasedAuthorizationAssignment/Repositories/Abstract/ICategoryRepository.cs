using RoleBasedAuthorizationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category obj);

      
    }
}
