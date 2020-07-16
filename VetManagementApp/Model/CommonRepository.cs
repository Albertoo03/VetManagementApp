using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VetManagementApp.Model.DbContexts;
using static VetManagementApp.Interfaces.Interfaces;

namespace VetManagementApp.Model
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected DbContext _dbContext;

        public CommonRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }


        public virtual void Add(T entity)   // virtual in order to allow to override it in derived class
        {
            _dbSet.Add(entity);
        }

        public virtual bool Delete(int entityId)
        {
            //using (var context = new VetManagementAppDbContext())
            //{
                //var entityToRemove = _dbSet.Where(customer => customer.GetType().Id == entityId).FirstOrDefault();
            var entityToRemove = _dbSet.Find(entityId);

            if (entityToRemove == null)
                return false;
            
            _dbSet.Remove(entityToRemove);
                //context.Customers.Remove(entityToRemove);
                //context.SaveChanges();

            return true;
            //}
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var itemsToRemove = _dbSet.Where(predicate);

            _dbSet.RemoveRange(itemsToRemove);

        }

        public virtual ICollection<T> GetAll()
        {
            return _dbSet.ToList();
            //using (var context = new VetManagementAppDbContext())
            //{
                //var queryResult = context.Customers.ToList();

                //return queryResult;
            //}
        }

        public virtual T Get(int entityId)
        {
            return _dbSet.Find(entityId);
        }

        public virtual T GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual ICollection<T> GetMultipleByPredicate(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
        public bool DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet.AsEnumerable());

            return (_dbSet.Count() == 0) ? true : false;
        }

        public void SetAsUnchanged(T entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Unchanged;
            _dbSet.Attach(entity);
        }

        public void SetAsAdded(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }
        //public void Update(T entity)
        //{
        //    _dbSet.Attach(entity);
        //    throw new NotImplementedException();
        //}

        public virtual ICollection<T> All
        {
            get
            {
                _dbSet.Load();
                var queryResult = _dbSet.Local;

                return queryResult;
            }
        }

    }
}
