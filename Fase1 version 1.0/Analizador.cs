using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fase1_version_1._0
{
    class Analizador
    {
        #region variables
        public string causaerror = "";
        string[] arreglo;
        int linea = 0;
        bool check = false;
        #endregion
        #region patterns
        string compiler = @"((\s|\t)*(compiler)(\s|\t)*(\w)+(\s|\t)*\.)";
        string units = @"(units)(\s|\t)*(\w*\,\w+)+\.";

        string ssets = @"sets";
        string sets1 = @"(\w*)(\s|\t)*\=(\s|\t)*(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets2 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))+(\'_\')*\.";
        string sets3 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets4 = @"(\w*)(\s|\t)*\=(\s|\t)*chr\(\d+\)\.\.chr\(\d+\)\.";

        string tokens1 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*\.";
        string tokens2 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*\w*(\*|\+)*\.";
        string tokens3 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*(\(\w+((\|\w+\))|(\))))(\*|\+)*(\s|\t)*(check)*\.";
        string tokens4 = @"\w+(\s|\t)*\=((\s|\t)*\w+(\s|\t)*)+\.";

        string sim1 = @"(((\"" |\')(\w+|\+|\*|\=|\<\>|\<|\>|\>\=|\<\=|\-)(\""|\'))(\,)*)+(\s|\t)((Left|Right)\.)*";
        string sim2 = @"(\'|\"")(\W|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\"")(\,(\'|\"")(\<\>|\<\=|\>\=|and|mod|div|not)(\'|\""))*(\,)*(\s|\t)*(left(\s|\t)*\.|right(\s|\t)*.)";
        string sim3 = @"((\'|\"")(\=|\<|\>|\,|\*|\+|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\"")(\,(\'|\"")(\=|\<|\>|\,|\*|\+|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\""))*(\,)*(\s|\t)*)(left(\s|\t)*\.|right(\s|\t)*\.)";

        string key1 = @"((\'|\"")\w+(\'|\""))(\,|\.)";

        string comm = @"(comments|comentario)(\s|\t)*(\'|\"")(\W+(\'|\"")(\s|\t)*(to)(\s|\t)*(\'|\"")\W+(\'|\"")(\s|\t)*)(comentario\.)";

        string prod = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*((\'\w+\')|(\<\w+\>|\?|(\'(\-|\+)\')|(\w+)|\|))*(\s|\t)*(\{\w+\})*\.";
        string prod2 = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*(\?)?(\s|\t)*(\{\w+\})\.";
        string prod3 = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*((\<\w+\>)*(\s|\t))*";
        #endregion
        public void inicio(string texto)
        {
            try
            {
                arreglo = texto.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (Regex.IsMatch(arreglo[linea].ToLower(), compiler))
                {
                    linea++;
                    Cuerpo();
                }
                else
                {
                    causaerror = "Compiler no definido correctamente o no definido " + arreglo[linea];
                    return;
                }
            }
            catch (Exception e)
            {
                causaerror = "error inesperado " + linea + " " + e.ToString() + "\n" + arreglo[linea]; ;
            }
        }

        void Cuerpo()
        {
            if (Regex.IsMatch(arreglo[linea].ToLower(), units))
            {
                linea++;
                if (Regex.IsMatch(arreglo[linea].ToLower(), ssets))
                {
                    linea++;
                    EstructuraSets();
                }
            }
            else
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), @"units"))
                {
                    causaerror = "Units mal definido " + linea + "\n" + arreglo[linea]; ;
                    return;
                }
                linea++;
                if (Regex.IsMatch(arreglo[linea].ToLower(), ssets))
                {
                    linea++;
                    EstructuraSets();
                }
            }
        }

        void EstructuraSets()
        {
            while (!Regex.IsMatch(arreglo[linea].ToLower(), @"tokens"))
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), sets1) | Regex.IsMatch(arreglo[linea].ToLower(), sets2) | Regex.IsMatch(arreglo[linea].ToLower(), sets3)
                | Regex.IsMatch(arreglo[linea].ToLower(), sets4))
                {
                    linea++;
                }
                else
                {
                    causaerror = "Set mal definido " + linea + "\n" + arreglo[linea]; ;
                    return;
                }
            }
            linea++;
            EstructuraTokens();
        }

        void EstructuraTokens()
        {
            while (!Regex.IsMatch(arreglo[linea].ToLower(), @"keywords"))
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), tokens1) | Regex.IsMatch(arreglo[linea].ToLower(), tokens2) | Regex.IsMatch(arreglo[linea].ToLower(), tokens3)
                | Regex.IsMatch(arreglo[linea].ToLower(), tokens4) | Regex.IsMatch(arreglo[linea].ToLower(), sim1) | Regex.IsMatch(arreglo[linea].ToLower(), sim2)
                | Regex.IsMatch(arreglo[linea].ToLower(), sim3))
                {
                    linea++;
                }
                else
                {
                    causaerror = "Error en Tokens " + linea + "\n" + arreglo[linea];
                    return;
                }
            }
            linea++;
            EstructuraKeywords();
        }

        void EstructuraKeywords()
        {
            while (!Regex.IsMatch(arreglo[linea].ToLower(), @"productions"))
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), key1))
                {
                    linea++;
                    EstructuraKeywords();
                }
                else
                {
                    causaerror = "Keywords mal definidas " + linea + "\n" + arreglo[linea]; ;
                    return;
                }
            }
            linea++;
            EstructuraProducctiones();
        }

        void EstructuraProducctiones()
        {
            //leer linea a linea caracter a caracter
            return;

        }
    }
}
