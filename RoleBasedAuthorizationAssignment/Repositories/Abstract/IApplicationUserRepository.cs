using RoleBasedAuthorizationAssignment.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Abstract
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
    }
}
