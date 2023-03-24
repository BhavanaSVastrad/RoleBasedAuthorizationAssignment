using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
       

        private  DatabaseContext _db;

        public UnitOfWork(DatabaseContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUserRepository = new ApplicationUserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
