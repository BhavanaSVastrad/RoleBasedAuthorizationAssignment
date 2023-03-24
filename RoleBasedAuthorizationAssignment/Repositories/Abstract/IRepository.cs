using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RoleBasedAuthorizationAssignment.Repositories.Abstract
{
    public interface IRepository<T> where T:class
    {
        T GetFirstOrDefault(Expression<Func<T,bool>>filter,string? includeProperties=null);

        //T -Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        void Add (T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable< T> entity);


    }
}
