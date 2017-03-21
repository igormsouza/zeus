using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BHS.ProjetoBaseMvc.Util
{
    public static class ErrosValidacao
    {
        /// <summary>
        /// Adiciona erros caso exista. Gerenciador p/ Web.
        /// </summary>
        /// <param name="erros">Modelo de erros Web.</param>
        /// <param name="resultado">Dicionario de Erros usado no BL.</param>
        public static void ExibeErros(this ModelStateDictionary model, Dictionary<string, string> errosValidacao)
        {
            foreach (var item in errosValidacao)
            {
                model.AddModelError(item.Key, item.Value);
            }
        }

        public static void ExibeErros(this ModelStateDictionary model, Dictionary<string, string> errosValidacao, Exception ex)
        {
            if (errosValidacao == null)
                errosValidacao = new Dictionary<string, string>();

            errosValidacao.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
        }
    }
}
