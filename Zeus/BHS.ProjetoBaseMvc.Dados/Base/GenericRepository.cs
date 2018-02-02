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

namespace Client.Zeus.Dados.Base
{
    public class GenericRepository<T>
        where T : class
    {
        protected Contexto DBContext { get; set; }
        internal DbSet<T> DBSet;

        public IQueryable<T> DBSetQuerable
        {
            get
            {
                return DBSet;
            }
        }

        public GenericRepository(Contexto context)
        {
            this.DBContext = context;
            this.DBSet = context.Set<T>();

            Database.SetInitializer<Contexto>(null);
            DBContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.DBContext.Configuration.ProxyCreationEnabled = false;
        }

        public GenericRepository()
            : this(new Contexto())
        {
        }

        public virtual List<T> List(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> consulta = Search(filter, order, includedProperties);
            return consulta.ToList();
        }

        public virtual List<T> List(
            IQueryable<T> filter,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> consulta = Search(filter, order, includedProperties);
            return consulta.ToList();
        }

        public virtual int Count(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> consulta = Search(filter, order, includedProperties);
            return consulta.Count();
        }

        public virtual int Count(
            IQueryable<T> filter,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> consulta = Search(filter, order, includedProperties);
            return consulta.Count();
        }

        public virtual List<T> ListarPaginado(
            out int itemsCount,
            IQueryable<T> query = null,
            string propriedadesAIncluir = "",
            int paginaAtual = 1,
            int quantidadePorPagina = 10,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            bool ordenacaoFeita = false
            )
        {
            itemsCount = 0;
            int salto = (paginaAtual - 1) * quantidadePorPagina;

            if (query == null)
            {
                query = DBSet;
            }

            if (!string.IsNullOrWhiteSpace(propriedadesAIncluir))
                query = propriedadesAIncluir
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            itemsCount = query.Count();

            if (ordenacao != null)
            {
                query = ordenacao(query);
            }
            else if (!ordenacaoFeita)
            {
                query = OrderDynamic.OrderBy(query, "ID");
            }

            query = query.Skip(salto).Take(quantidadePorPagina);

            return query.ToList();
        }


        public virtual List<T> ListarPaginado(
            out int itensCount,
            IQueryable<T> consulta = null,
            string propriedadesAIncluir = "",
            int paginaAtual = 1,
            int quantidadePorPagina = 10,
            string ordenacao = "",
            string direcaoOrdenacao = ""
            )
        {
            itensCount = 0;
            int salto = (paginaAtual - 1) * quantidadePorPagina;

            if (consulta == null)
            {
                consulta = DBSet;
            }

            if (!string.IsNullOrWhiteSpace(propriedadesAIncluir))
                consulta = propriedadesAIncluir
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(consulta, (current, includeProperty) => current.Include(includeProperty));

            itensCount = consulta.Count();

            if (!string.IsNullOrWhiteSpace(ordenacao))
            {
                if (direcaoOrdenacao.Trim().ToUpper() == "DESC")
                {
                    consulta = OrderDynamic.OrderByDescending(consulta, ordenacao);
                }
                else
                {
                    consulta = OrderDynamic.OrderBy(consulta, ordenacao);
                }
            }
            else
            {
                consulta = OrderDynamic.OrderBy(consulta, "ID");
            }

            consulta = consulta.Skip(salto).Take(quantidadePorPagina);

            return consulta.ToList();
        }

        public virtual List<T> ListarPaginado(
          out int itensCount,
          Expression<Func<T, bool>> filtro = null,
          string propriedadesAIncluir = "",
          int paginaAtual = 1,
          int quantidadePorPagina = 10,
          string ordenacao = "",
          string direcaoOrdenacao = ""
          )
        {
            if (quantidadePorPagina == 0)
                quantidadePorPagina = 10;

            if (paginaAtual == 0)
                paginaAtual = 1;

            itensCount = 0;
            int salto = (paginaAtual - 1) * quantidadePorPagina;
            IQueryable<T> consulta = DBSet;

            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            if (!string.IsNullOrWhiteSpace(propriedadesAIncluir))
                consulta = propriedadesAIncluir
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(consulta, (current, includeProperty) => current.Include(includeProperty));

            itensCount = consulta.Count();

            if (!string.IsNullOrWhiteSpace(ordenacao))
            {
                if (direcaoOrdenacao.Trim().ToUpper() == "DESC")
                {
                    consulta = OrderDynamic.OrderByDescending(consulta, ordenacao);
                }
                else
                {
                    consulta = OrderDynamic.OrderBy(consulta, ordenacao);
                }
            }
            else
            {
                consulta = OrderDynamic.OrderBy(consulta, "ID");
            }

            consulta = consulta.Skip(salto).Take(quantidadePorPagina);

            return consulta.ToList();
        }

        public virtual IQueryable<T> Search(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> order = null,
            string includedProperties = "")
        {
            IQueryable<T> consulta = DBSet;

            if (filter != null)
            {
                consulta = consulta.Where(filter);
            }

            return Search(consulta, order, includedProperties);
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

        public virtual void Update(T enitty)
        {
            if (this.DBContext.Entry(enitty).State == System.Data.Entity.EntityState.Detached)
            {
                DBSet.Attach(enitty);
            }
            this.DBContext.ChangeTracker.DetectChanges();
            this.DBContext.Entry(enitty).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual IList<T> SearchBySQL(string consultaSQL, params object[] parameters)
        {
            var result = DBSet.SqlQuery(consultaSQL, parameters).ToList();
            return result;
        }

        public virtual void Discart(T entity)
        {
            ((IObjectContextAdapter)this.DBContext).ObjectContext.Detach(entity);
        }

        public virtual void Delete(T entity)
        {
            ((IObjectContextAdapter)this.DBContext).ObjectContext.DeleteObject(entity);
        }

        public virtual void Discart(IList<T> list)
        {
            foreach (var item in list)
                this.Discart(item);
        }

        public virtual void Delete(IList<T> list)
        {
            foreach (var item in list)
                this.Delete(item);
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