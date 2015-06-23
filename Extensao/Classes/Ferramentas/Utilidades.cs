using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace Genrec
{
    public class Utilidades
    {
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
                numeros[i] = int.Parse(valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++) soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0) return false;

            }
            else if (numeros[9] != 11 - resultado) return false;
            soma = 0;
            for (int i = 0; i < 10; i++) soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0) return false;
            }
            else
                if (numeros[10] != 11 - resultado) return false;
            return true;
        }
        public static Boolean ValidaCnpj(String cnpj)
        {
            Int32[] digitos, soma, resultado;
            Int32 nrDig;
            String ftmt;
            Boolean[] cnpjOk;
            if (cnpj == "00000000000000") return false;
            ftmt = "6543298765432";
            digitos = new Int32[14];
            soma = new Int32[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new Int32[2];
            resultado[0] = 0;
            resultado[1] = 0;
            cnpjOk = new Boolean[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11) soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12) soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1)) cnpjOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else cnpjOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }
                return (cnpjOk[0] && cnpjOk[1]);
            }
            catch
            {
                return false;
            }
        }
        public static string GetDataValida(string data)
        {
            if (data == "01/01/0001 00:00:00") return "";
            if (data == "01/01/0001 00:00") return "";
            if (data == "01/01/0001") return "";
            return data;
        }
        public static string AbreviarNomes(string s)
        {
            string[] nomes = s.Split(' ');
            int inicio = 1;
            int fim = nomes.Length - 1;
            string retorno = "";
            for (int i = 0; i < nomes.Length; i++)
            {
                bool pr = false;
                if (nomes[i].Length < 4)
                {
                    pr = PalavrasExcecoes(nomes[i]);
                    if (pr) continue;
                }
                if ((!pr) && (i >= inicio && i < fim))
                    //retorno += nomes[i][0] + ". ";
                    retorno += nomes[i][0] + " ";
                else
                    retorno += nomes[i] + " ";
            }
            return retorno.Trim();
        }
        private static bool PalavrasExcecoes(string palavra)
        {
            System.Text.RegularExpressions.Regex regExc = new System.Text.RegularExpressions.Regex("(DA|da|DE|de|DO|do|DAS|das|DOS|dos)$");
            return regExc.Match(palavra).Success;
        }
        public static Boolean IsNumero(string strNumero)
        {
            for (int i = 0; i < strNumero.Length; i++)
            {
                if (!char.IsNumber(strNumero, i)) return false;
            }
            return true;
        }
        public static bool isMobileBrowser(HttpContext context)
        {
            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static string MD5Hash(string text)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] md5password = md5.ComputeHash(Encoding.ASCII.GetBytes(text));
            StringBuilder result = new StringBuilder();
            foreach (byte b in md5password)
                result.Append(b.ToString("x2"));
            return result.ToString();
        }
    }
}