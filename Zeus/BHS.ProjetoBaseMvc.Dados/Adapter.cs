using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using Client.Zeus.Dados.Repository;
using Client.Zeus.Domain.Base;
using System.Reflection;
using System.Data.Entity;

namespace Client.Zeus.Dados
{
    public partial class Adapter
    {
        public Adapter()
        {
            contexto = new Contexto();
        }

        public int Save(Dictionary<string, string> erros = null)
        {
            int quantidadeModificacoes = 0;

            try
            {
                quantidadeModificacoes = contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (erros != null)
                    erros.Add("erro", "O registro foi modificado por outro usuário");
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
                        erros.Add(ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;
                    if (erros != null)
                    {
                        if (number == 547)
                        {
                            Exception erro = (((Exception)(ex)).InnerException).InnerException;
                            erros.Add("erro", "erro: " + erro.Message + " exeção: " + erro.InnerException + " pilha execução:" + erro.StackTrace);

                            //errosValidacao.Add("impossivelExcluirRegistroFK", Mensagens.ImpossivelExcluirRegistroFK.ToString());
                        }
                        else
                        {
                            erros.Add("erro", "erro: " + ex.Message + " exeção: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (erros != null)
                {
                    erros.Add("erro", "erro: " + ex.Message + " exeção: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
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

        public void CarregaColecao<T>(T entidade, string colecao) where T : BaseDomain
        {
            contexto.Entry(entidade).Collection(colecao).Load();
        }

        public void MudarStatusItensParaRemocao(DomainBase Domain)
        {
            MudarStatusItens(Domain, EntityState.Deleted);
        }

        public void MudarStatusItens(DomainBase Domain, EntityState novoStatus)
        {
            contexto.Entry(Domain).State = novoStatus;
        }
    }
}
