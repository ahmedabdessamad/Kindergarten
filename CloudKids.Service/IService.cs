using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CloudKids.Service
{
    public interface IService<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(Expression<Func<T, bool>> Condition);
        void Commit();
        T GetById(int id);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> Condition = null, Expression<Func<T, bool>> orderBy = null);
        IEnumerable<T> GetAll();

        //Cart Service
        T GetCartByParentId(int parentId);


        //CartLine Service
        List<T> GetCartLinesByCartId(int cartId);

        //Product Service (MyProduct)
        List<T> GetMyProduct(int directorId);
    }
}