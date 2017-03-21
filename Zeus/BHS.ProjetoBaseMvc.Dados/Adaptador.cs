using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using BHS.ProjetoBaseMvc.Dados.Repositorio;
using BHS.ProjetoBaseMvc.Dominio.Base;
using System.Reflection;
using System.Data.Entity;

namespace BHS.ProjetoBaseMvc.Dados
{
    public partial class Adaptador
    {
        public Adaptador()
        {
            contexto = new Contexto();
        }

        /// <summary>
        /// Persiste as modificações
        /// </summary>
        /// <param name="errosValidacao">Mensagens de erro</param>
        /// <returns>Quantidade de registros modificados</returns>
        public int SalvarModificacoes(Dictionary<string, string> errosValidacao = null)
        {
            int quantidadeModificacoes = 0;

            try
            {
                quantidadeModificacoes = contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (errosValidacao != null)
                    errosValidacao.Add("erro", "O registro foi modificado por outro usuário");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        errosValidacao.Add(ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;
                    if (errosValidacao != null)
                    {
                        if (number == 547)
                        {
                            Exception erro = (((Exception)(ex)).InnerException).InnerException;
                            errosValidacao.Add("erro", "erro: " + erro.Message + " exeção: " + erro.InnerException + " pilha execução:" + erro.StackTrace);

                            //errosValidacao.Add("impossivelExcluirRegistroFK", Mensagens.ImpossivelExcluirRegistroFK.ToString());
                        }
                        else
                        {
                            errosValidacao.Add("erro", "erro: " + ex.Message + " exeção: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (errosValidacao != null)
                {
                    errosValidacao.Add("erro", "erro: " + ex.Message + " exeção: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
                }
            }
            return quantidadeModificacoes;
        }

        public void AtualizaEntidades()
        {
            foreach (DbEntityEntry entry in contexto.ChangeTracker.Entries())
                entry.Reload();
        }

        public void VerificaEntidadesAuxiliares(string propriedadesAIncluir)
        {
            string[] propriedades = propriedadesAIncluir.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (DbEntityEntry entry in contexto.ChangeTracker.Entries())
            {
                var tipo = ((MemberInfo)((entry.Entity.GetType().BaseType).UnderlyingSystemType)).Name;
                if (propriedades.Contains(tipo))
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void CarregaColecao<T>(T entidade, string colecao) where T : DominioBase
        {
            contexto.Entry(entidade).Collection(colecao).Load();
        }

        public void MudarStatusItensParaRemocao(DominioBase dominio)
        {
            MudarStatusItens(dominio, EntityState.Deleted);
        }

        public void MudarStatusItens(DominioBase dominio, EntityState novoStatus)
        {
            contexto.Entry(dominio).State = novoStatus;
        }
    }
}
