using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Zeus.Util
{
    public static class ValidationError
    {
        public static void ShowErros(this ModelStateDictionary model, Dictionary<string, string> validataion)
        {
            foreach (var item in validataion)
            {
                model.AddModelError(item.Key, item.Value);
            }
        }

        public static void ShowErros(this ModelStateDictionary model, Dictionary<string, string> validation, Exception ex)
        {
            if (validation == null)
                validation = new Dictionary<string, string>();

            validation.Add("erro_nao_tratado", ex.Message + " - " + ex.InnerException);
        }
    }
}
