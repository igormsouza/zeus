using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using Client.Zeus.Data.Repository;
using Client.Zeus.Domain.Base;
using System.Reflection;
using System.Data.Entity;

namespace Client.Zeus.Data
{
    public partial class Adapter
    {
        public Adapter()
        {
            context = new Context();
        }

        public int Save(Dictionary<string, string> erros = null)
        {
            int coutModified = 0;

            try
            {
                coutModified = context.SaveChanges();
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
                            erros.Add("erro", "erro: " + erro.Message + " exception: " + erro.InnerException + " pilha execução:" + erro.StackTrace);
                        }
                        else
                        {
                            erros.Add("erro", "erro: " + ex.Message + " exception: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (erros != null)
                {
                    erros.Add("erro", "erro: " + ex.Message + " exception: " + ex.InnerException + " pilha execução:" + ex.StackTrace);
                }
            }
            return coutModified;
        }

        public void ReloadEntities()
        {
            foreach (DbEntityEntry item in context.ChangeTracker.Entries())
                item.Reload();
        }

        public void VerificaEntidadesAuxiliares(string propriedadesAIncluir)
        {
            string[] property = propriedadesAIncluir.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (DbEntityEntry item in context.ChangeTracker.Entries())
            {
                var tipo = ((MemberInfo)((item.Entity.GetType().BaseType).UnderlyingSystemType)).Name;
                if (property.Contains(tipo))
                {
                    item.State = EntityState.Modified;
                }
            }
        }

        public void LoadList<T>(T entity, string nameList) where T : BaseDomain
        {
            context.Entry(entity).Collection(nameList).Load();
        }

        public void ChangeEntityStatusToDelete(BaseDomain domain)
        {
            ChangeEntityStatus(domain, EntityState.Deleted);
        }

        public void ChangeEntityStatus(BaseDomain domain, EntityState novoStatus)
        {
            context.Entry(domain).State = novoStatus;
        }
    }
}
