using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using BHS.ProjetoBaseMvc.Dominio;
using BHS.ProjetoBaseMvc.Util;
using System.Data.Entity;

namespace BHS.ProjetoBaseMvc.Dados.Base
{
    public class RepositorioGenerico<T>
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

        public RepositorioGenerico(Contexto contexto)
        {
            this.DBContext = contexto;
            this.DBSet = contexto.Set<T>();

            Database.SetInitializer<Contexto>(null);
            DBContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.DBContext.Configuration.ProxyCreationEnabled = false;
        }

        public RepositorioGenerico()
            : this(new Contexto())
        {
        }

        public virtual List<T> Listar(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            string propriedadesAIncluir = "")
        {
            IQueryable<T> consulta = Consultar(filtro, ordenacao, propriedadesAIncluir);
            return consulta.ToList();
        }

        public virtual List<T> Listar(
            IQueryable<T> filtro,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            string propriedadesAIncluir = "")
        {
            IQueryable<T> consulta = Consultar(filtro, ordenacao, propriedadesAIncluir);
            return consulta.ToList();
        }

        public virtual int Count(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            string propriedadesAIncluir = "")
        {
            IQueryable<T> consulta = Consultar(filtro, ordenacao, propriedadesAIncluir);
            return consulta.Count();
        }

        public virtual int Count(
            IQueryable<T> filtro,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            string propriedadesAIncluir = "")
        {
            IQueryable<T> consulta = Consultar(filtro, ordenacao, propriedadesAIncluir);
            return consulta.Count();
        }

        public virtual List<T> ListarPaginado(
            out int itensCount,
            IQueryable<T> consulta = null,
            string propriedadesAIncluir = "",
            int paginaAtual = 1,
            int quantidadePorPagina = 10,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            bool ordenacaoFeita = false
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

            if (ordenacao != null)
            {
                consulta = ordenacao(consulta);
            }
            else if (!ordenacaoFeita)
            {
                consulta = OrderDynamic.OrderBy(consulta, "ID");
            }

            consulta = consulta.Skip(salto).Take(quantidadePorPagina);

            return consulta.ToList();
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

        public virtual IQueryable<T> Consultar(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> ordenacao = null,
            string propriedadesAIncluir = "")
        {
            IQueryable<T> consulta = DBSet;

            if (filtro != null)
            {
                consulta = consulta.Where(filtro);
            }

            return Consultar(consulta, ordenacao, propriedadesAIncluir);
        }

        public virtual IQueryable<T> Consultar(
           IQueryable<T> consulta,
           Func<IQueryable<T>,
           IOrderedQueryable<T>> ordenacao = null,
           string propriedadesAIncluir = "")
        {

            if (!string.IsNullOrWhiteSpace(propriedadesAIncluir))
            {
                consulta = propriedadesAIncluir
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(consulta, (current, includeProperty) => current.Include(includeProperty));
            }

            if (ordenacao != null)
            {
                return ordenacao(consulta);
            }
            return consulta;
        }

        public virtual T BuscarPorId(int chavesPrimarias)
        {
            return DBSet.Find(chavesPrimarias);
        }

        public virtual void Inserir(T entidadeParaInserir)
        {
            DBSet.Add(entidadeParaInserir);
            DBContext.Entry(entidadeParaInserir).State = System.Data.Entity.EntityState.Added;
        }

        public virtual void Excluir(params object[] chavesPrimarias)
        {
            T entidadeParaExcluir = DBSet.Find(chavesPrimarias);
            Excluir(entidadeParaExcluir);
        }

        public virtual void Excluir(T entidadeParaExcluir)
        {
            if (this.DBContext.Entry(entidadeParaExcluir).State == System.Data.Entity.EntityState.Detached)
            {
                DBSet.Attach(entidadeParaExcluir);
            }
            DBSet.Remove(entidadeParaExcluir);
        }

        public virtual void Excluir(ICollection<T> listaParaExcluir)
        {
            var count = listaParaExcluir.Count - 1;

            for (int i = count; i >= 0; i--)
            {
                Excluir(listaParaExcluir.ToList()[i]);
            }
        }

        public virtual void Atualizar(T entidadeParaAtualizar)
        {
            if (this.DBContext.Entry(entidadeParaAtualizar).State == System.Data.Entity.EntityState.Detached)
            {
                DBSet.Attach(entidadeParaAtualizar);
            }
            this.DBContext.ChangeTracker.DetectChanges();
            this.DBContext.Entry(entidadeParaAtualizar).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual IList<T> BuscarPorSQL(string consultaSQL, params object[] parametros)
        {
            var result = DBSet.SqlQuery(consultaSQL, parametros).ToList();
            return result;
        }

        public virtual void Descartar(T entidadeParaDestacar)
        {
            ((IObjectContextAdapter)this.DBContext).ObjectContext.Detach(entidadeParaDestacar);
        }

        public virtual void Deletar(T entidadeParaDestacar)
        {
            ((IObjectContextAdapter)this.DBContext).ObjectContext.DeleteObject(entidadeParaDestacar);
        }

        public virtual void Descartar(IList<T> listaEntidadeParaDestacar)
        {
            foreach (var itemDescartar in listaEntidadeParaDestacar)
                this.Descartar(itemDescartar);
        }

        public virtual void Deletar(IList<T> listaEntidadeParaDestacar)
        {
            foreach (var itemDescartar in listaEntidadeParaDestacar)
                this.Deletar(itemDescartar);
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parametros)
        {
            var result = DBContext.Database.ExecuteSqlCommand(sql, parametros);
            return result;
        }

        public virtual IEnumerable<T> BuscarPorSQL<T>(string sql, params object[] parametros) where T : class
        {
            var result = DBContext.Database.SqlQuery<T>(sql, parametros);
            return result;
        }

        public System.Data.Entity.EntityState VerificaEstadoEntidade(T entidade)
        {
            return this.DBContext.Entry(entidade).State;
        }
    }
}