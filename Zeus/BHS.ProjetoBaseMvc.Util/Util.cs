using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Zeus.Util
{
    public static class Util
    {
        public static string StringToMD5(string toConvert)
        {
            var passwordBytes = System.Security.Cryptography.MD5.Create().ComputeHash(ASCIIEncoding.ASCII.GetBytes(toConvert));
            var sb = new System.Text.StringBuilder();
            for (int x = 0; x < passwordBytes.Length; x++)
                sb.Append(passwordBytes[x].ToString("x2"));
            return sb.ToString();
        }

        public static string FirstLetterLowerCase(this string value, bool restInLowerCase = false)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value.Substring(0, 1).ToLower();

                if (restInLowerCase)
                    result += value.Substring(1).ToLower();
                else
                    result += value.Substring(1);
            }

            return result;
        }

        public static string FirstLetterUpperCase(this string value, bool restInLowerCase = false)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                result = value.Substring(0, 1).ToUpper();

                if (restInLowerCase)
                    result += value.Substring(1).ToLower();
                else
                    result += value.Substring(1);
            }

            return result;
        }

        public static string WitheSpace(int quantity)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < quantity; i++)
            {
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public static String GenerateClassName(string value)
        {
            value = value.Replace("TB_", "").Replace(".cs", "");

            string result = "";
            string[] aux = value.Split('_');

            int cont = 0;
            foreach (var item in aux)
            {
                if (item == "_")
                    continue;

                result += FirstLetterUpperCase(item, true);
                cont++;
            }

            result = UpdateAlias(result);
            return result;
        }

        public static String GenerateFieldName(string value)
        {
            value = value.Replace("TB_", "").Replace(".cs", "");

            string result = "";
            string[] aux = value.Split('_');

            int cont = 0;
            foreach (var item in aux)
            {
                if (cont == 0)
                    result += FirstLetterLowerCase(item, true);
                else
                    result += FirstLetterUpperCase(item, true);

                cont++;
            }

            result = UpdateAlias(result);
            return result;
        }

        public static string GeneratePropertyName(string descricao)
        {
            string result = GenerateClassName(descricao);
            return result;
        }

        public static string UpdateAlias(string result)
        {
            string aux = result.Replace("Uf", "UF");

            aux = aux.Replace("Cep", "CEP");
            aux = aux.Replace("Cnpj", "CNPJ");
            aux = aux.Replace("Cpf", "CPF");

            return aux;
        }

        #region Brazilian Mask Validator

        public static bool ValidateCpf(string cpf)
        {
            string value = cpf.Replace(".", "");
            value = value.Replace("-", "");

            if (value.Length != 11)
                return false;

            bool equals = true;
            for (int i = 1; i < 11 && equals; i++)
                if (value[i] != value[0])
                    equals = false;

            if (equals || value == "12345678909")
                return false;

            int[] numbers = new int[11];
            for (int i = 0; i < 11; i++)
                numbers[i] = int.Parse(
                value[i].ToString());

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            int result = sum % 11;
            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;

            }
            else
                if (numbers[10] != 11 - result)
                    return false;
            return true;

        }
        
        public static bool ValidateCnpj(string cnpj)
        {

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            int[] digit, sum, result;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digit = new int[14];
            sum = new int[2];
            sum[0] = 0;
            sum[1] = 0;
            result = new int[2];
            result[0] = 0;
            result[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digit[nrDig] = int.Parse(
                     cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        sum[0] += (digit[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        sum[1] += (digit[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    result[nrDig] = (sum[nrDig] % 11);
                    if ((result[nrDig] == 0) || (result[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digit[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digit[12 + nrDig] == (
                        11 - result[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }

        }

        #endregion
    }
}
