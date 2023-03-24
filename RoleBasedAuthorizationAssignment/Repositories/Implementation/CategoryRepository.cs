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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _db;
        
        public CategoryRepository(DatabaseContext db):base(db)
        {
            _db = db;
           
        }
     

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
