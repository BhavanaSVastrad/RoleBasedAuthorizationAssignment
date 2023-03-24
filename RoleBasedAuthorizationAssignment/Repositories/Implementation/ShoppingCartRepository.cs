using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorizationAssignment.Models;
using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Implementation
{
    public class ShoppingCartRepository : Repository<Cart>, IShoppingCartRepository
    {
        private readonly DatabaseContext _db;

        public ShoppingCartRepository(DatabaseContext db):base(db)
        {
            _db = db;
        }

        public int DecrementCount(Cart Cart, int Count)
        {
            Cart.Count -= Count;
            return Cart.Count;

        }

        public int IncrementCount(Cart Cart, int Count)
        {
            Cart.Count += Count;
            return Cart.Count;
        }

      
    }
}
