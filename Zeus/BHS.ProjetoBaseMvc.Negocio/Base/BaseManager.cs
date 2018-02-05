using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Zeus.Domain.Base;
using Client.Zeus.Data;
using Client.Zeus.Data.Base;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Entity;

namespace Client.Zeus.Business.Base
{
    public abstract class BaseManager<T> where T : BaseDomain, new()
    {
        protected readonly Adapter adapter;

        private GenericRepository<T> baseRepository;
        protected GenericRepository<T> BaseRepository
        {
            get
            {
                if (baseRepository == null)
                {
                    var properties = typeof(Adapter).GetProperties();
                    foreach (PropertyInfo i in properties)
                    {
                        var property = i.GetValue(adapter);
                        if (property is GenericRepository<T>)
                        {
                            baseRepository = property as GenericRepository<T>;
                            break;
                        }
                    }
                }

                return baseRepository;
            }
        }

        private string includedEntities;
        protected virtual string IncludedEntities { get { return includedEntities; } }

        private string ignorarAoEditar;
        public virtual string IgnorarAoEditar
        {
            get { return ignorarAoEditar; }
            set { ignorarAoEditar = value; }
        }

        protected virtual void InsertValidation(T entity, ref Dictionary<string, string> erros)
        {

        }
        protected virtual void UpdateValidation(T entity, ref Dictionary<string, string> erros, string editContext = "")
        {

        }
        protected virtual void DeleteValidation(T entity, ref Dictionary<string, string> erros)
        {

        }

        public BaseManager()
        {
            this.adapter = new Adapter();
        }

        public BaseManager(Adapter adaptador)
        {
            this.adapter = adaptador;
        }

        public BaseManager(Context context)
        {
            this.adapter = new Adapter(context);
        }

        public IQueryable<T> Query
        {
            get
            {
                return BaseRepository.DBSetQuerable;
            }
        }

        public virtual IList<T> List(string includedProperties = null)
        {
            if (!string.IsNullOrWhiteSpace(includedProperties))
            {
                includedEntities = includedProperties;
            }

            IList<T> result = null;

            if (string.IsNullOrEmpty(IncludedEntities))
                result = BaseRepository.List();
            else
                result = BaseRepository.List(includedProperties: this.IncludedEntities);

            return result;
        }

        public virtual IList<T> List(IQueryable<T> filter, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string incluirPropriedades = null)
        {
            IList<T> retorno = null;

            if (string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                includedEntities = this.IncludedEntities;
            }

            retorno = BaseRepository.List(filter, order, includedProperties: incluirPropriedades);

            return retorno;
        }

        public virtual IList<T> List(Func<T, bool> filter, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string includedProperties = null)
        {
            if (!string.IsNullOrWhiteSpace(includedProperties))
            {
                includedEntities = includedProperties;
            }

            var query = Query;

            if (!string.IsNullOrWhiteSpace(includedEntities))
            {
                query = includedProperties
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includedEntities));
            }

            IQueryable<T> filterAux = query.Where(filter).AsQueryable<T>();
            var result = List(filterAux, order, includedEntities);

            return result;
        }

        public virtual int Count(Func<T, bool> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao = null, string includedProperties = null)
        {
            if (string.IsNullOrWhiteSpace(includedProperties))
            {
                includedEntities = this.IncludedEntities;
            }

            IQueryable<T> filtroAux = Query.Where(filtro).AsQueryable<T>();
            var retorno = Count(filtroAux, ordenacao, includedEntities);

            return retorno;
        }

        public virtual int Count(IQueryable<T> filter, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string includedProperties = null)
        {
            int result = 0;

            if (string.IsNullOrWhiteSpace(includedProperties))
            {
                includedEntities = this.IncludedEntities;
            }

            result = BaseRepository.Count(filter, order, includedProperties: includedProperties);

            return result;
        }

        public virtual IList<T> PagingList(
           out int totalItems,
           IQueryable<T> filter = null,
           string includedProperties = "",
           int currentPage = 1,
           int countPerPage = 10,
           string order = "",
           string orderDirection = ""
           )
        {
            IList<T> result = null;

            if (string.IsNullOrEmpty(IncludedEntities))
                result = BaseRepository.PagingList(out totalItems, filter, currentPage: currentPage, countPerPage: countPerPage, order: order, orderDirection: orderDirection);
            else
                result = BaseRepository.PagingList(out totalItems, filter, currentPage: currentPage, countPerPage: countPerPage, order: order, orderDirection: orderDirection, includedProperties: this.IncludedEntities);

            return result;
        }

        public virtual IList<T> PagingList(out int totalItems, int currentPagel = 0, string order = "", string orderDirection = "", int countPerPage = 10)
        {
            IList<T> result = PagingList(out totalItems, null, currentPagel, order, orderDirection, countPerPage);
            return result;
        }

        internal virtual IList<T> PagingList(out int totalItems, Expression<Func<T, bool>> filter = null, int currentPage = 0, string order = "", string orderDirection = "", int countPerPage = 10)
        {
            IList<T> result = null;

            if (string.IsNullOrEmpty(IncludedEntities))
                result = BaseRepository.PagingList(out totalItems, filter, currentPage: currentPage, countPerPage: countPerPage, order: order, orderDirection: orderDirection);
            else
                result = BaseRepository.PagingList(out totalItems, filter, currentPage: currentPage, countPerPage: countPerPage, order: order, orderDirection: orderDirection, includedProperties: this.IncludedEntities);

            return result;
        }

        public virtual T GetById(int id)
        {
            T result = null;

            result = BaseRepository.SearchById(id);

            return result;
        }

        public virtual T GetById(string key)
        {
            int id; int.TryParse(key, out id);

            T result = GetById(id);

            return result;
        }

        public virtual void PreInsert(ref T enitty)
        {

        }

        public virtual bool Create(T entity, out Dictionary<string, string> erros)
        {
            erros = new Dictionary<string, string>();

            InsertValidation(entity, ref erros);

            if (erros.Count > 0)
                return false;

            int countModified = 0;

            try
            {
                PreInsert(ref entity);
                BaseRepository.Insert(entity);
                countModified = adapter.Save(erros);
            }
            catch (Exception ex)
            {
                erros.Add("error", ex.Message + " - " + ex.InnerException);
            }

            return countModified > 0;
        }

        public virtual bool Edit(T entity, out Dictionary<string, string> erros, string editContext = "", params string[] preservedProperties)
        {
            if (preservedProperties.Length == 0 && !string.IsNullOrWhiteSpace(IgnorarAoEditar))
            {
                preservedProperties = IgnorarAoEditar.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            erros = new Dictionary<string, string>();

            UpdateValidation(entity, ref erros, editContext);

            if (erros.Count > 0)
                return false;

            int countModified = 0;
            T oldEntity = null;
            try
            {
                oldEntity = GetById(entity.ID);
                PreEdit(ref entity);
                PreEditCondition(entity, oldEntity, preservedProperties);
                PostEditCondition(entity, ref oldEntity, editContext);

                BaseRepository.Update(oldEntity);
                countModified = adapter.Save(erros);
            }
            catch (Exception ex)
            {
                erros.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
            }

            return countModified > 0;
        }

        public virtual void PreEditCondition(T entity, T oldEntity, params string[] preservedProperties)
        {
            if (preservedProperties == null)
                preservedProperties = new string[] { };

            var properties = typeof(T).GetProperties().Where(p => !preservedProperties.Contains(p.Name));
            IDictionary<string, int> auxiliar = new Dictionary<string, int>();

            foreach (PropertyInfo i in properties)
            {
                dynamic oldValue = i.GetValue(oldEntity);
                dynamic newValue = i.GetValue(entity);

                if (i.Name.Contains("ID_") && oldValue != null)
                    auxiliar.Add(new KeyValuePair<string, int>(i.Name, (int)oldValue));

                if (!(oldValue is BaseDomain))
                {
                    if (i.PropertyType.Name.Contains("ICollection"))
                    {
                        if (newValue != null)
                            i.SetValue(oldEntity, newValue);
                    }
                    else
                        if (oldValue != newValue)
                        i.SetValue(oldEntity, newValue);
                }
                else
                {
                    var auxDomain = oldValue as BaseDomain;
                    string id = i.Name.Replace("TB_", "ID_");
                    if (auxiliar.ContainsKey(id))
                    {
                        if (auxDomain.ID != auxiliar[id])
                        {
                            i.SetValue(oldEntity, null);
                        }
                    }
                }
            }
        }

        public virtual void PostEditCondition(T entity, ref T oldEntity, string editContext)
        {

        }

        public virtual bool Delete(int id, out Dictionary<string, string> erros)
        {
            bool result = false;
            erros = new Dictionary<string, string>();

            T entity = GetById(id);

            if (entity != null)
            {
                result = Delete(entity, out erros);
            }

            return result;
        }

        public virtual bool Delete(IList<int> id, out Dictionary<string, string> erros)
        {
            bool result = false;
            erros = new Dictionary<string, string>();

            IList<T> entity = BaseRepository.List(o => id.Contains(o.ID));

            if (entity != null)
            {
                result = Delete(entity, out erros);
            }

            return result;
        }

        public virtual bool Delete(T entity, out Dictionary<string, string> erros)
        {
            erros = new Dictionary<string, string>();

            DeleteValidation(entity, ref erros);

            if (erros.Count > 0)
                return false;

            int countModified = 0;

            try
            {
                PreDelete(ref entity);
                BaseRepository.Delete(entity);
                countModified = adapter.Save(erros);
            }
            catch (Exception ex)
            {
                erros.Add("error", ex.Message + " - " + ex.InnerException);
            }

            return countModified > 0;
        }

        public virtual void PreEdit(ref T entity)
        {

        }

        public virtual void PreDelete(ref T entity)
        {

        }

        public virtual bool Delete(IList<T> list, out Dictionary<string, string> erros)
        {
            erros = new Dictionary<string, string>();
            bool result = true;

            foreach (var item in list)
            {
                result = Delete(item, out erros);
                if (erros.Count > 0)
                {
                    break;
                }
            }

            return result;
        }

        private void Delete(T entity)
        {
            int countModified = 0;
            BaseRepository.Delete(entity);
            countModified = adapter.Save();
        }

        public virtual void ExecuteSqlCommand(string query, params object[] parameters)
        {
            BaseRepository.ExecuteSqlCommand(query, parameters);
        }

        public void ReloadEntities()
        {
            adapter.ReloadEntities();
        }

        public void ChangeEntityStatus(BaseDomain entity, EntityState newStatus)
        {
            adapter.ChangeEntityStatus(entity, newStatus);
        }

        public void ChangeEntityStatus(IList<BaseDomain> list, EntityState newStatus)
        {
            foreach (var item in list)
            {
                adapter.ChangeEntityStatus(item, newStatus);
            }
        }

        public void ChangeEntityStatusToDelete(BaseDomain entity)
        {
            adapter.ChangeEntityStatusToDelete(entity);
        }

        public void ChangeEntityStatusToDelete(IList<BaseDomain> list)
        {
            foreach (var item in list)
            {
                adapter.ChangeEntityStatusToDelete(item);
            }
        }

        public virtual IList<T> Search(out int countItens, BaseSearchCodeDescription<T> filter, string includedProperties = null, IList<Tuple<int, string>> futherLevels = null)
        {
            int countLevels = 1;
            IQueryable<T> query = Query;

            var classType = typeof(T);
            var lambdaParameter = Expression.Parameter(classType, "a");

            DynamicSearch(ref query, filter.Query, classType, lambdaParameter, 1, countLevels, futherLevels);

            if (string.IsNullOrWhiteSpace(includedProperties))
            {
                includedProperties = IncludedEntities;
            }

            return BaseRepository.PagingList(out countItens, query, includedProperties, filter.Page, filter.CountPerPage, filter.Sort, filter.SortDir);
        }

        private void DynamicSearch(ref IQueryable<T> query, object value, Type classType, ParameterExpression lambdaParameter, int currentLevel, int maxLevel, IList<Tuple<int, string>> futherLevels = null)
        {
            if (currentLevel <= maxLevel)
            {
                futherLevels = futherLevels ?? new List<Tuple<int, string>>();
                var nextLevel = futherLevels.Where(o => o.Item1 == currentLevel).Select(o => o.Item2.ToUpper()).ToList();

                foreach (var item in classType.GetProperties())
                {
                    var PropriedadeEntidade = Expression.PropertyOrField(lambdaParameter, item.Name);

                    // Criar lambda dinamicamente
                    if (item.PropertyType == typeof(String))
                    {
                        var itemValue = item.GetValue(value) as String;
                        if (!string.IsNullOrEmpty(itemValue))
                        {
                            var body = Expression.Call(PropriedadeEntidade, typeof(string).GetMethod("Contains"), new[] { Expression.Constant(itemValue) });
                            var predicate = (Expression<Func<T, bool>>)Expression.Lambda(body, lambdaParameter);
                            query = query.Where(predicate);
                        }
                    }
                    else if (item.PropertyType == typeof(DateTime) || item.PropertyType == typeof(DateTime?))
                    {
                        var itemValue = item.GetValue(value) as DateTime?;
                        if (itemValue != null && itemValue != DateTime.MinValue)
                        {
                            var nextDay = itemValue.Value.AddDays(1);

                            var propertySearch1 = Expression.Constant(itemValue, item.PropertyType);
                            var propertySearch2 = Expression.Constant(nextDay, item.PropertyType);
                            var biggerThan = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, PropriedadeEntidade, propertySearch1);
                            var smallerThan = Expression.MakeBinary(ExpressionType.LessThan, PropriedadeEntidade, propertySearch2);
                            var equals = Expression.MakeBinary(ExpressionType.And, biggerThan, smallerThan);
                            var predicate = (Expression<Func<T, bool>>)Expression.Lambda(equals, lambdaParameter);
                            query = query.Where(predicate);
                        }
                    }
                    else if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool?))
                    {
                        var itemValue = item.GetValue(value) as bool?;

                        if (itemValue != null && itemValue.Value == true)
                        {
                            var propertySearch1 = Expression.Constant(itemValue.Value, item.PropertyType);
                            var predicate = (Expression<Func<T, bool>>)Expression.Lambda(Expression.MakeBinary(ExpressionType.Equal, PropriedadeEntidade, propertySearch1), lambdaParameter);
                            query = query.Where(predicate);
                        }
                    }
                    else if (item.PropertyType == typeof(int) || item.PropertyType == typeof(int?))
                    {
                        var itemValue = item.GetValue(value) as int?;

                        if (itemValue != null && itemValue.Value > 0)
                        {
                            var propertySearch1 = Expression.Constant(itemValue.Value, item.PropertyType);
                            var predicate = (Expression<Func<T, bool>>)Expression.Lambda(Expression.MakeBinary(ExpressionType.Equal, PropriedadeEntidade, propertySearch1), lambdaParameter);
                            query = query.Where(predicate);
                        }
                    }
                    else if (item.PropertyType.IsSubclassOf(typeof(BaseDomain)) && nextLevel.Count > 0)
                    {
                        if (!nextLevel.Contains(item.Name.ToUpper()))
                        {
                            continue;
                        }

                        var lambdaProperty = lambdaParameter.Name + "." + item.Name.ToUpper();
                        var lambda = Expression.Parameter(item.PropertyType, lambdaProperty);

                        PropertyInfo newProperty = value.GetType().GetProperties().First(o => o.Name.ToUpper() == item.Name.ToUpper());
                        object newValue = item.GetValue(value);
                        if (newValue != null)
                        {
                            DynamicSearch(ref query, newValue, item.PropertyType, lambda, currentLevel + 1, maxLevel, futherLevels);
                        }
                    }
                }
            }
        }
    }
}


