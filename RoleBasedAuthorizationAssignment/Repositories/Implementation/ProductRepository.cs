using RoleBasedAuthorizationAssignment.Models;
using RoleBasedAuthorizationAssignment.Models.Domain;
using RoleBasedAuthorizationAssignment.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly DatabaseContext _db;

        public ProductRepository(DatabaseContext db) : base(db)
        {
            _db = db;

        }


        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if(objFromDb !=null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.Product_Image = obj.Product_Image;
                objFromDb.Product_Name = obj.Product_Name;
                objFromDb.Product_Description = obj.Product_Description;
                objFromDb.Product_Price = obj.Product_Price;
                objFromDb.CategoryId = obj.CategoryId;

            }
        }
    }
}
