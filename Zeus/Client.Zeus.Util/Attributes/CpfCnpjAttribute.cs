using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Zeus.Util.Attributes
{
    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CpfCnpjAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CpfCnpjAttribute() { }

        /// <summary>
        /// Server Side
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            value = Regex.Replace(value.ToString(), @"\D", "");

            bool valido = value.ToString().Length > 11 ? Util.ValidateCnpj(value.ToString()) : Util.ValidateCpf(value.ToString());
            return valido;
        }

        /// <summary>
        /// Cliente Side
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(metadata.SimpleDisplayText),
                ValidationType = "cpfcnpj"
            };
        }
    }
}
