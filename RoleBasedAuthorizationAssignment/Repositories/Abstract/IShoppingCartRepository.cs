using RoleBasedAuthorizationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Abstract
{
    public interface IShoppingCartRepository : IRepository<Cart>
    {
        int IncrementCount(Cart Cart,int Count);

        int DecrementCount(Cart Cart, int Count);

      
    }
}
