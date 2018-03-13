using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using Client.Zeus.Domain;
using Client.Zeus.Util;
using System.Data.Entity;

namespace Client.Zeus.Data.Base
{
    public class GenericRepository<T>
        where T : class
    {
        protected Context DBContext { get; set; }
        internal DbSet<T> DBSet;

        public IQueryable<T> DBSetQuerable
        {
            get
            {
                return DBSet;
            }
        }

        public GenericRepository(Context context)
        {
            this.DBContext = context;
            this.DBSet = context.Set<T>();

            Database.SetInitializer<Context>(null);
            DBContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.DBContext.Configuration.ProxyCreationEnabled = false;
        }

        public GenericRepository()
            : this(new Context())
        {
        }

        public virtual List<T> List(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> query = Search(filter, order, includedProperties);
            return query.ToList();
        }

        public virtual List<T> List(
            IQueryable<T> filter,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> query = Search(filter, order, includedProperties);
            return query.ToList();
        }

        public virtual int Count(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> query = Search(filter, order, includedProperties);
            return query.Count();
        }

        public virtual int Count(
            IQueryable<T> filter,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> query = Search(filter, order, includedProperties);
            return query.Count();
        }

        public virtual List<T> PagingList(
            out int itemsCount,
            IQueryable<T> query = null,
            string includedProperties = "",
            int currentPage = 1,
            int countPerPage = 10,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            bool defaultOrder = false
            )
        {
            itemsCount = 0;
            int skip = (currentPage - 1) * countPerPage;

            if (query == null)
            {
                query = DBSet;
            }

            if (!string.IsNullOrWhiteSpace(includedProperties))
                query = includedProperties
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            itemsCount = query.Count();

            if (order != null)
            {
                query = order(query);
            }
            else if (!defaultOrder)
            {
                query = OrderDynamic.OrderBy(query, "ID");
            }

            query = query.Skip(skip).Take(countPerPage);

            return query.ToList();
        }


        public virtual List<T> PagingList(
            out int itemsCount,
            IQueryable<T> query = null,
            string includedProperties = "",
            int currentPage = 1,
            int countPerPage = 10,
            string order = "",
            string orderDirection = ""
            )
        {
            itemsCount = 0;
            int skip = (currentPage - 1) * countPerPage;

            if (query == null)
            {
                query = DBSet;
            }

            if (!string.IsNullOrWhiteSpace(includedProperties))
                query = includedProperties
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            itemsCount = query.Count();

            if (!string.IsNullOrWhiteSpace(order))
            {
                if (orderDirection.Trim().ToUpper() == "DESC")
                {
                    query = OrderDynamic.OrderByDescending(query, order);
                }
                else
                {
                    query = OrderDynamic.OrderBy(query, order);
                }
            }
            else
            {
                query = OrderDynamic.OrderBy(query, "ID");
            }

            query = query.Skip(skip).Take(countPerPage);

            return query.ToList();
        }

        public virtual List<T> PagingList(
          out int itemsCount,
          Expression<Func<T, bool>> filter = null,
          string includedProperties = "",
          int currentPage = 1,
          int countPerPage = 10,
          string order = "",
          string orderDirection = ""
          )
        {
            if (countPerPage == 0)
                countPerPage = 10;

            if (currentPage == 0)
                currentPage = 1;

            itemsCount = 0;
            int skip = (currentPage - 1) * countPerPage;
            IQueryable<T> query = DBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includedProperties))
                query = includedProperties
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            itemsCount = query.Count();

            if (!string.IsNullOrWhiteSpace(order))
            {
                if (orderDirection.Trim().ToUpper() == "DESC")
                {
                    query = OrderDynamic.OrderByDescending(query, order);
                }
                else
                {
                    query = OrderDynamic.OrderBy(query, order);
                }
            }
            else
            {
                query = OrderDynamic.OrderBy(query, "ID");
            }

            query = query.Skip(skip).Take(countPerPage);

            return query.ToList();
        }

        public virtual IQueryable<T> Search(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> query = DBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return Search(query, order, includedProperties);
        }

        public virtual IQueryable<T> Search(
           IQueryable<T> query,
           Func<IQueryable<T>,
           IOrderedQueryable<T>> order = null,
           string includedProperties = "")
        {

            if (!string.IsNullOrWhiteSpace(includedProperties))
            {
                query = includedProperties
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            if (order != null)
            {
                return order(query);
            }
            return query;
        }

        public virtual T SearchById(int id)
        {
            return DBSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            DBSet.Add(entity);
            DBContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public virtual void Delete(params object[] primaryKeys)
        {
            T entity = DBSet.Find(primaryKeys);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (this.DBContext.Entry(entity).State == System.Data.Entity.EntityState.Detached)
            {
                DBSet.Attach(entity);
            }

            DBSet.Remove(entity);
        }

        public virtual void Delete(ICollection<T> list)
        {
            var count = list.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                Delete(list.ToList()[i]);
            }
        }

        public virtual void Update(T entity)
        {
            if (this.DBContext.Entry(entity).State == System.Data.Entity.EntityState.Detached)
            {
                DBSet.Attach(entity);
            }
            this.DBContext.ChangeTracker.DetectChanges();
            this.DBContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual IList<T> SearchBySQL(string query, params object[] parameters)
        {
            var result = DBSet.SqlQuery(query, parameters).ToList();
            return result;
        }

        public virtual void Discart(T entity)
        {
            ((IObjectContextAdapter)this.DBContext).ObjectContext.Detach(entity);
        }

        public virtual void Discart(IList<T> list)
        {
            foreach (var item in list)
            {
                this.Discart(item);
            }
        }

        public virtual void Delete(IList<T> list)
        {
            foreach (var item in list)
            {
                this.Delete(item);
            }
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parametros)
        {
            var result = DBContext.Database.ExecuteSqlCommand(sql, parametros);
            return result;
        }

        public virtual IEnumerable<T> SearchBySQL<T>(string sql, params object[] parameters) where T : class
        {
            var result = DBContext.Database.SqlQuery<T>(sql, parameters);
            return result;
        }

        public System.Data.Entity.EntityState EntityState(T entity)
        {
            return this.DBContext.Entry(entity).State;
        }
    }
}