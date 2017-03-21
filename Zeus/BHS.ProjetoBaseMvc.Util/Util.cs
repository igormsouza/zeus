using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHS.ProjetoBaseMvc.Util
{
    public static class Util
    {
        /// <summary>
        /// Metodo que converte uma string em criptografia MD5
        /// </summary>
        /// <param name="toConvert">String que sera convertida para MD5</param>
        /// <returns>Retorna valor criptografado</returns>
        public static string StringToMD5(string toConvert)
        {
            var senhaBytes = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(toConvert));
            var sb = new System.Text.StringBuilder();
            for (int x = 0; x < senhaBytes.Length; x++)
                sb.Append(senhaBytes[x].ToString("x2"));
            return sb.ToString();
        }

        public static string SetaPrimeiraLetraMinuscula(this string valor, bool restoMinusculo = false)
        {
            string retorno = string.Empty;

            if (!string.IsNullOrWhiteSpace(valor))
            {
                retorno = valor.Substring(0, 1).ToLower();

                if (restoMinusculo)
                    retorno += valor.Substring(1).ToLower();
                else
                    retorno += valor.Substring(1);
            }

            return retorno;
        }

        public static string SetaPrimeiraLetraMaiuscula(this string valor, bool restoMinusculo = false)
        {
            string retorno = string.Empty;

            if (!string.IsNullOrWhiteSpace(valor))
            {
                retorno = valor.Substring(0, 1).ToUpper();

                if (restoMinusculo)
                    retorno += valor.Substring(1).ToLower();
                else
                    retorno += valor.Substring(1);
            }

            return retorno;
        }

        public static string EspacoEmBranco(int quantidade)
        {
            StringBuilder retorno = new StringBuilder();

            for (int i = 0; i < quantidade; i++)
            {
                retorno.Append(Environment.NewLine);
            }

            return retorno.ToString();
        }

        public static String GeraNomeClasse(string descricao)
        {
            descricao = descricao.Replace("TB_", "").Replace(".cs", "");

            string retorno = "";
            string[] aux = descricao.Split('_');

            int cont = 0;
            foreach (var item in aux)
            {
                if (item == "_")
                    continue;

                retorno += SetaPrimeiraLetraMaiuscula(item, true);
                cont++;
            }

            retorno = AtualizaSiglas(retorno);
            return retorno;
        }

        public static String GeraNomeCampo(string descricao)
        {
            descricao = descricao.Replace("TB_", "").Replace(".cs", "");

            string retorno = "";
            string[] aux = descricao.Split('_');

            int cont = 0;
            foreach (var item in aux)
            {
                if (cont == 0)
                    retorno += SetaPrimeiraLetraMinuscula(item, true);
                else
                    retorno += SetaPrimeiraLetraMaiuscula(item, true);

                cont++;
            }

            retorno = AtualizaSiglas(retorno);
            return retorno;
        }

        public static string GeraNomePropriedade(string descricao)
        {
            string retorno = GeraNomeClasse(descricao);
            return retorno;
        }

        public static string AtualizaSiglas(string retorno)
        {
            string aux = retorno.Replace("Uf", "UF");

            aux = aux.Replace("Cep", "CEP");
            aux = aux.Replace("Cnpj", "CNPJ");
            aux = aux.Replace("Cpf", "CPF");

            return aux;
        }

        //Método que valida o CPF
        public static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;

            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;
            return true;

        }

        //Método que valida o CNPJ 
        public static bool ValidaCNPJ(string vrCNPJ)
        {

            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                     CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }

        }
    }
}
