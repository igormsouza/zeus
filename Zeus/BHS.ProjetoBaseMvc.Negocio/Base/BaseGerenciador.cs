using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHS.ProjetoBaseMvc.Dominio.Base;
using BHS.ProjetoBaseMvc.Dados;
using BHS.ProjetoBaseMvc.Dados.Base;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Entity;

namespace BHS.ProjetoBaseMvc.Negocio.Base
{
    public abstract class BaseGerenciador<T> where T : DominioBase, new()
    {
        protected readonly Adaptador adaptador;

        private RepositorioGenerico<T> retorno;
        protected RepositorioGenerico<T> RepositorioBase
        {
            get
            {
                if (retorno == null)
                {
                    var propriedades = typeof(Adaptador).GetProperties();
                    foreach (PropertyInfo i in propriedades)
                    {
                        var propriedade = i.GetValue(adaptador);
                        if (propriedade is RepositorioGenerico<T>)
                        {
                            retorno = propriedade as RepositorioGenerico<T>;
                            break;
                        }
                    }
                }

                return retorno;
            }
        }

        private string incluirEntidades;
        protected virtual string IncluirEntidades { get { return incluirEntidades; } }

        private string ignorarAoEditar;
        public virtual string IgnorarAoEditar
        {
            get { return ignorarAoEditar; }
            set { ignorarAoEditar = value; }
        }

        protected virtual void ValidaInsercao(T entidade, ref Dictionary<string, string> errosValidacao)
        {

        }
        protected virtual void ValidaEdicao(T entidade, ref Dictionary<string, string> errosValidacao, string contextoEdicao = "")
        {

        }
        protected virtual void ValidaExclusao(T entidade, ref Dictionary<string, string> errosValidacao)
        {

        }

        public BaseGerenciador()
        {
            this.adaptador = new Adaptador();
        }

        public BaseGerenciador(Adaptador adaptador)
        {
            this.adaptador = adaptador;
        }

        public BaseGerenciador(Contexto contextoExistente)
        {
            this.adaptador = new Adaptador(contextoExistente);
        }

        public IQueryable<T> Query
        {
            get
            {
                return RepositorioBase.DBSetQuerable;
            }
        }

        public virtual IList<T> Listar(string incluirPropriedades = null)
        {
            if (!string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                incluirEntidades = incluirPropriedades;
            }

            IList<T> retorno = null;

            if (string.IsNullOrEmpty(IncluirEntidades))
                retorno = RepositorioBase.Listar();
            else
                retorno = RepositorioBase.Listar(propriedadesAIncluir: this.IncluirEntidades);

            return retorno;
        }

        public virtual IList<T> Listar(IQueryable<T> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao = null, string incluirPropriedades = null)
        {
            IList<T> retorno = null;

            if (string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                incluirEntidades = this.IncluirEntidades;
            }

            retorno = RepositorioBase.Listar(filtro, ordenacao, propriedadesAIncluir: incluirPropriedades);

            return retorno;
        }

        public virtual IList<T> Listar(Func<T, bool> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao = null, string incluirPropriedades = null)
        {
            if (!string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                incluirEntidades = incluirPropriedades;
            }

            var consulta = Query;

            if (!string.IsNullOrWhiteSpace(incluirEntidades))
            {
                consulta = incluirPropriedades
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(consulta, (current, includeProperty) => current.Include(incluirEntidades));
            }

            IQueryable<T> filtroAux = consulta.Where(filtro).AsQueryable<T>();
            var retorno = Listar(filtroAux, ordenacao, incluirEntidades);

            return retorno;
        }

        public virtual int Count(Func<T, bool> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao = null, string incluirPropriedades = null)
        {
            if (string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                incluirEntidades = this.IncluirEntidades;
            }

            IQueryable<T> filtroAux = Query.Where(filtro).AsQueryable<T>();
            var retorno = Count(filtroAux, ordenacao, incluirEntidades);

            return retorno;
        }

        public virtual int Count(IQueryable<T> filtro, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao = null, string incluirPropriedades = null)
        {
            int retorno = 0;

            if (string.IsNullOrWhiteSpace(incluirPropriedades))
            {
                incluirEntidades = this.IncluirEntidades;
            }

            retorno = RepositorioBase.Count(filtro, ordenacao, propriedadesAIncluir: incluirPropriedades);

            return retorno;
        }

        public virtual IList<T> ListarPaginado(
           out int totalItens,
           IQueryable<T> filtro = null,
           string propriedadesAIncluir = "",
           int paginaAtual = 1,
           int quantidadePorPagina = 10,
           string ordenacao = "",
           string direcaoOrdenacao = ""
           )
        {
            IList<T> retorno = null;

            if (string.IsNullOrEmpty(IncluirEntidades))
                retorno = RepositorioBase.ListarPaginado(out totalItens, filtro, paginaAtual: paginaAtual, quantidadePorPagina: quantidadePorPagina, ordenacao: ordenacao, direcaoOrdenacao: direcaoOrdenacao);
            else
                retorno = RepositorioBase.ListarPaginado(out totalItens, filtro, paginaAtual: paginaAtual, quantidadePorPagina: quantidadePorPagina, ordenacao: ordenacao, direcaoOrdenacao: direcaoOrdenacao, propriedadesAIncluir: this.IncluirEntidades);

            return retorno;
        }

        public virtual IList<T> ListarPaginado(out int totalItens, int paginaAtual = 0, string ordenacao = "", string direcaoOrdenacao = "", int quantidadePorPagina = 10)
        {
            IList<T> retorno = ListarPaginado(out totalItens, null, paginaAtual, ordenacao, direcaoOrdenacao, quantidadePorPagina);
            return retorno;
        }

        internal virtual IList<T> ListarPaginado(out int totalItens, Expression<Func<T, bool>> filtro = null, int paginaAtual = 0, string ordenacao = "", string direcaoOrdenacao = "", int quantidadePorPagina = 10)
        {
            IList<T> retorno = null;

            if (string.IsNullOrEmpty(IncluirEntidades))
                retorno = RepositorioBase.ListarPaginado(out totalItens, filtro, paginaAtual: paginaAtual, quantidadePorPagina: quantidadePorPagina, ordenacao: ordenacao, direcaoOrdenacao: direcaoOrdenacao);
            else
                retorno = RepositorioBase.ListarPaginado(out totalItens, filtro, paginaAtual: paginaAtual, quantidadePorPagina: quantidadePorPagina, ordenacao: ordenacao, direcaoOrdenacao: direcaoOrdenacao, propriedadesAIncluir: this.IncluirEntidades);

            return retorno;
        }

        public virtual T BuscarPorId(int chavePrimaria)
        {
            T retorno = null;

            retorno = RepositorioBase.Listar(o => o.ID == chavePrimaria, propriedadesAIncluir: this.IncluirEntidades).FirstOrDefault();

            return retorno;
        }

        public virtual T BuscarPorId(string chavePrimaria)
        {
            int id; int.TryParse(chavePrimaria, out id);

            T retorno = null;

            retorno = RepositorioBase.Listar(o => o.ID == id, propriedadesAIncluir: this.IncluirEntidades).FirstOrDefault();

            return retorno;
        }

        public virtual T BuscarPorIdSemReferencial(int id)
        {
            T retorno = null;

            retorno = RepositorioBase.BuscarPorId(id);

            return retorno;
        }

        public virtual void PreInserir(ref T entidade)
        {

        }

        public virtual bool Criar(T objetoNovo, out Dictionary<string, string> errosValidacao)
        {
            errosValidacao = new Dictionary<string, string>();

            ValidaInsercao(objetoNovo, ref errosValidacao);

            if (errosValidacao.Count > 0)
                return false;

            int quantidadeModificacoes = 0;

            try
            {
                PreInserir(ref objetoNovo);
                RepositorioBase.Inserir(objetoNovo);
                quantidadeModificacoes = adaptador.SalvarModificacoes(errosValidacao);
            }
            catch (Exception ex)
            {
                errosValidacao.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
            }

            return quantidadeModificacoes > 0;
        }

        public virtual bool Editar(T objetoEditado, out Dictionary<string, string> errosValidacao, string contextoEdicao = "", params string[] propriedadesPreservadas)
        {
            if (propriedadesPreservadas.Length == 0 && !string.IsNullOrWhiteSpace(IgnorarAoEditar))
            {
                propriedadesPreservadas = IgnorarAoEditar.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            errosValidacao = new Dictionary<string, string>();

            ValidaEdicao(objetoEditado, ref errosValidacao, contextoEdicao);

            if (errosValidacao.Count > 0)
                return false;

            int quantidadeModificacoes = 0;
            T objetoAntigo = null;
            try
            {
                objetoAntigo = BuscarPorId(objetoEditado.ID);
                PreEditar(ref objetoEditado);
                PreCondicaoEditar(objetoEditado, objetoAntigo, propriedadesPreservadas);
                PosCondicaoEditar(objetoEditado, ref objetoAntigo, contextoEdicao);

                RepositorioBase.Atualizar(objetoAntigo);
                quantidadeModificacoes = adaptador.SalvarModificacoes(errosValidacao);
            }
            catch (Exception ex)
            {
                errosValidacao.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
            }

            return quantidadeModificacoes > 0;
        }

        public virtual void PreCondicaoEditar(T objetoEditado, T objetoAntigo, params string[] propriedadesPreservadas)
        {
            if (propriedadesPreservadas == null)
                propriedadesPreservadas = new string[] { };

            var propriedades = typeof(T).GetProperties().Where(p => !propriedadesPreservadas.Contains(p.Name));
            IDictionary<string, int> dicionarioAuxiliares = new Dictionary<string, int>();

            foreach (PropertyInfo i in propriedades)
            {
                dynamic valorAntigo = i.GetValue(objetoAntigo);
                dynamic valorNovo = i.GetValue(objetoEditado);

                if (i.Name.Contains("ID_") && valorAntigo != null)
                    dicionarioAuxiliares.Add(new KeyValuePair<string, int>(i.Name, (int)valorAntigo));

                if (!(valorAntigo is DominioBase))
                {
                    if (i.PropertyType.Name.Contains("ICollection"))
                    {
                        if (valorNovo != null)
                            i.SetValue(objetoAntigo, valorNovo);
                    }
                    else
                        if (valorAntigo != valorNovo)
                        i.SetValue(objetoAntigo, valorNovo);
                }
                else
                {
                    var dominioAuxiliar = valorAntigo as DominioBase;
                    string id = i.Name.Replace("TB_", "ID_");
                    if (dicionarioAuxiliares.ContainsKey(id))
                    {
                        if (dominioAuxiliar.ID != dicionarioAuxiliares[id])
                        {
                            i.SetValue(objetoAntigo, null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Chamada apos o PreCondicaoEditar
        /// Obs: Metodo usado parar corrigir a falha ao voltar os valores para um boleano nullable.
        /// </summary>
        /// <param name="objetoEditado"></param>
        /// <param name="objetoAntigo"></param>
        /// <param name="contextoEdicao"></param>
        public virtual void PosCondicaoEditar(T objetoEditado, ref T objetoAntigo, string contextoEdicao)
        {

        }

        public virtual void PreCondicaoEditar(T objetoEditado, T objetoAntigo)
        {
            this.PreCondicaoEditar(objetoEditado, objetoAntigo, null);
        }

        public virtual bool Excluir(int id, out Dictionary<string, string> errosValidacao)
        {
            bool retorno = false;
            errosValidacao = new Dictionary<string, string>();

            T objetoExcluido = BuscarPorId(id);

            if (objetoExcluido != null)
            {
                retorno = Excluir(objetoExcluido, out errosValidacao);
            }

            return retorno;
        }

        public virtual bool Excluir(IList<int> id, out Dictionary<string, string> errosValidacao)
        {
            bool retorno = false;
            errosValidacao = new Dictionary<string, string>();

            IList<T> objetoExcluido = RepositorioBase.Listar(o => id.Contains(o.ID));

            if (objetoExcluido != null)
            {
                retorno = Excluir(objetoExcluido, out errosValidacao);
            }

            return retorno;
        }

        public virtual bool Excluir(T objetoExcluido, out Dictionary<string, string> errosValidacao)
        {
            errosValidacao = new Dictionary<string, string>();

            ValidaExclusao(objetoExcluido, ref errosValidacao);

            if (errosValidacao.Count > 0)
                return false;

            int quantidadeModificacoes = 0;

            try
            {
                PreEditar(ref objetoExcluido);
                RepositorioBase.Excluir(objetoExcluido);
                quantidadeModificacoes = adaptador.SalvarModificacoes(errosValidacao);
            }
            catch (Exception ex)
            {
                errosValidacao.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
            }

            return quantidadeModificacoes > 0;
        }

        public virtual void PreEditar(ref T objetoExcluido)
        {

        }

        public virtual bool Excluir(IList<T> listaObjetoExcluido, out Dictionary<string, string> errosValidacao)
        {
            errosValidacao = new Dictionary<string, string>();
            bool retorno = true;

            foreach (var item in listaObjetoExcluido)
            {
                retorno = Excluir(item, out errosValidacao);
                if (errosValidacao.Count > 0)
                    break;
            }

            return retorno;
        }

        private void Excluir(T objetoExcluido)
        {
            int quantidadeModificacoes = 0;
            RepositorioBase.Excluir(objetoExcluido);
            quantidadeModificacoes = adaptador.SalvarModificacoes();
        }

        public virtual void ExecuteSqlCommand(string query, params object[] parametros)
        {
            RepositorioBase.ExecuteSqlCommand(query, parametros);
        }

        public void AtualizaContexto()
        {
            adaptador.AtualizaEntidades();
        }

        public void MudarStatusItens(DominioBase dominio, EntityState novoStatus)
        {
            adaptador.MudarStatusItens(dominio, novoStatus);
        }

        public void MudarStatusItens(IList<DominioBase> dominios, EntityState novoStatus)
        {
            foreach (var item in dominios)
            {
                adaptador.MudarStatusItens(item, novoStatus);
            }
        }

        public void MudarStatusItensParaRemocao(DominioBase dominio)
        {
            adaptador.MudarStatusItensParaRemocao(dominio);
        }

        public void MudarStatusItensParaRemocao(IList<DominioBase> dominios)
        {
            foreach (var item in dominios)
            {
                adaptador.MudarStatusItensParaRemocao(item);
            }
        }

        public virtual IList<T> Pesquisar(out int countItens, BaseSearchCodeDescription<T> manterFiltros, string propriedadesAIncluir = null, IList<Tuple<int, string>> entidadesAprofundar = null)
        {
            int quantidadeNiveis = 1;
            IQueryable<T> consulta = Query;

            var classeEntidade = typeof(T);
            var parametroLambda = Expression.Parameter(classeEntidade, "a");

            CriaConstulta(ref consulta, manterFiltros.Query, classeEntidade, parametroLambda, 1, quantidadeNiveis, entidadesAprofundar);

            if (string.IsNullOrWhiteSpace(propriedadesAIncluir))
            {
                propriedadesAIncluir = IncluirEntidades;
            }

            return RepositorioBase.ListarPaginado(out countItens, consulta, propriedadesAIncluir, manterFiltros.Page, manterFiltros.CountPerPage, manterFiltros.Sort, manterFiltros.SortDir);
        }

        private void CriaConstulta(ref IQueryable<T> consulta, object valor, Type classeEntidade, ParameterExpression parametroLambda, int nivelAtual, int quantidadeNiveis, IList<Tuple<int, string>> entidadesAprofundar = null)
        {
            if (nivelAtual <= quantidadeNiveis)
            {
                entidadesAprofundar = entidadesAprofundar ?? new List<Tuple<int, string>>();
                var entidadeAprofundarNivel = entidadesAprofundar.Where(o => o.Item1 == nivelAtual).Select(o => o.Item2.ToUpper()).ToList();

                foreach (var propriedade in classeEntidade.GetProperties())
                {
                    var PropriedadeEntidade = Expression.PropertyOrField(parametroLambda, propriedade.Name);

                    // Criar lambda dinamicamente
                    if (propriedade.PropertyType == typeof(String))
                    {
                        var Valor = propriedade.GetValue(valor) as String;
                        if (!string.IsNullOrEmpty(Valor))
                        {
                            var Corpo = Expression.Call(PropriedadeEntidade, typeof(string).GetMethod("Contains"), new[] { Expression.Constant(Valor) });
                            var Predicado = (Expression<Func<T, bool>>)Expression.Lambda(Corpo, parametroLambda);
                            consulta = consulta.Where(Predicado);
                        }
                    }
                    else if (propriedade.PropertyType == typeof(DateTime) || propriedade.PropertyType == typeof(DateTime?))
                    {
                        var Valor = propriedade.GetValue(valor) as DateTime?;
                        if (Valor != null && Valor != DateTime.MinValue)
                        {
                            var ProximoDia = Valor.Value.AddDays(1);

                            var PropriedadePesquisa = Expression.Constant(Valor, propriedade.PropertyType);
                            var PropriedadePesquisa2 = Expression.Constant(ProximoDia, propriedade.PropertyType);
                            var MaiorQue = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, PropriedadeEntidade, PropriedadePesquisa);
                            var MenorQue = Expression.MakeBinary(ExpressionType.LessThan, PropriedadeEntidade, PropriedadePesquisa2);
                            var Comparacao = Expression.MakeBinary(ExpressionType.And, MaiorQue, MenorQue);
                            var Predicado = (Expression<Func<T, bool>>)Expression.Lambda(Comparacao, parametroLambda);
                            consulta = consulta.Where(Predicado);
                        }
                    }
                    else if (propriedade.PropertyType == typeof(bool) || propriedade.PropertyType == typeof(bool?))
                    {
                        var Valor = propriedade.GetValue(valor) as bool?;

                        if (Valor != null && Valor.Value == true)
                        {
                            var PropriedadePesquisa = Expression.Constant(Valor.Value, propriedade.PropertyType);
                            var Predicado = (Expression<Func<T, bool>>)Expression.Lambda(Expression.MakeBinary(ExpressionType.Equal, PropriedadeEntidade, PropriedadePesquisa), parametroLambda);
                            consulta = consulta.Where(Predicado);
                        }
                    }
                    else if (propriedade.PropertyType == typeof(int) || propriedade.PropertyType == typeof(int?))
                    {
                        var Valor = propriedade.GetValue(valor) as int?;

                        if (Valor != null && Valor.Value > 0)
                        {
                            var PropriedadePesquisa = Expression.Constant(Valor.Value, propriedade.PropertyType);
                            var Predicado = (Expression<Func<T, bool>>)Expression.Lambda(Expression.MakeBinary(ExpressionType.Equal, PropriedadeEntidade, PropriedadePesquisa), parametroLambda);
                            consulta = consulta.Where(Predicado);
                        }
                    }
                    else if (propriedade.PropertyType.IsSubclassOf(typeof(DominioBase)) && entidadeAprofundarNivel.Count > 0)
                    {
                        if (!entidadeAprofundarNivel.Contains(propriedade.Name.ToUpper()))
                        {
                            continue;
                        }

                        var propriedadeLambda = parametroLambda.Name + "." + propriedade.Name.ToUpper();
                        var lambda = Expression.Parameter(propriedade.PropertyType, propriedadeLambda);

                        PropertyInfo propriedadeNova = valor.GetType().GetProperties().First(o => o.Name.ToUpper() == propriedade.Name.ToUpper());
                        object valorNovo = propriedade.GetValue(valor);
                        if (valorNovo != null)
                        {
                            CriaConstulta(ref consulta, valorNovo, propriedade.PropertyType, lambda, nivelAtual + 1, quantidadeNiveis, entidadesAprofundar);
                        }
                    }
                }
            }
        }
    }
}


