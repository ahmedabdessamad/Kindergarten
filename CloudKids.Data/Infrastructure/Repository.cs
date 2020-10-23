using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CloudKids.Data;
using CloudKids.Domain;
using CloudKids.Domain.Entities;
using System.Data.SqlClient;

namespace CloudKids.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private CloudKidsContext context;
        private DbSet<T> dbSet;


        public Repository(IDBFactory dbFactory)
        {
            this.context = dbFactory.Context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T obj)
        {
            dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;

        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
        }

        public void Delete(Expression<Func<T, bool>> condition) // lambda expression
        {
            var result = dbSet.Where(condition);

            if (result != null)
                foreach (T obj in result)
                    dbSet.Remove(obj);
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null)
        {
            return dbSet.Where(condition).OrderBy(orderBy);
        }


        public void Commit()
        {
            context.SaveChanges();
        }

        //Cart Service
        public T GetCartByParentId(int parentId)
        {
            return dbSet.SqlQuery("Select * from Carts where ParentId=@parentId", new SqlParameter("@parentId", parentId)).FirstOrDefault();
        }


        //CartLine Service
        public List<T> GetCartLinesByCartId(int cartId)
        {
            return dbSet.SqlQuery("Select * from CartLines where CartId=@cartId", new SqlParameter("@cartId", cartId)).ToList<T>();
        }

        //Product Service (MyProduct)
        public List<T> GetMyProduct(int directorId)
        {
            return dbSet.SqlQuery("Select * from Products where DirectorId=@directorId", new SqlParameter("@directorId", directorId)).ToList<T>();
        }

    }


}
