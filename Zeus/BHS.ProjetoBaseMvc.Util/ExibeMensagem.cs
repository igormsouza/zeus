using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Zeus.Util
{
    public static class ExibeMensagem
    {
        public static void Show(Controller controller, Dictionary<string, string> erros,
            string mensagemSucesso = "",
            string idMensagemSucesso = "SuccessMessage", string idMensagemFalha = "ErrorMessage",
            bool sumario = false)
        {
            TempDataDictionary temp = ((ControllerBase)controller).TempData;

            if (temp != null)
            {
                mensagemSucesso = string.IsNullOrEmpty(mensagemSucesso) ? Mensagens.OperacaoRealizadaComSucesso : mensagemSucesso;

                if (erros.Count == 0)
                {
                    temp[idMensagemSucesso] = mensagemSucesso;
                }
                else
                {
                    if (!sumario)
                    {
                        string mensagemFalha = erros.Select(o => o.Value).First();
                        temp[idMensagemFalha] = mensagemFalha;
                    }
                    else
                    {
                        ModelStateDictionary model = controller.ModelState;
                        model.ShowErros(erros);
                    }
                }
            }
        }

        /// <summary>
        /// Exibe Mensagem de Warning flutuante na action do controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="mensagem">Mensagem exibida no Warning</param>
        public static void ShowWarning(Controller controller, string mensagem = "")
        {
            TempDataDictionary temp = ((ControllerBase)controller).TempData;

            if (temp != null)
            {
                temp["WarningMessage"] = mensagem;
            }
        }
    }
}
