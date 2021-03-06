﻿using System;
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
        public string causaerror, palabra = "";
        string[] arreglo;
        int linea = 0;
        bool check, punto, end, tokens, produccion, start = false;
        int comilla, parentesis, corchete, comillas, lesser = 0;
        #endregion

        #region patterns
        string compiler = @"((\s|\t)*(compiler)(\s|\t)*(?!compiler)(\w)+(\s|\t)*\.)";
        string units = @"(units)(\s|\t)*(\w*\,\w+)+\.";

        string sets1 = @"(\w*)(\s|\t)*\=(\s|\t)*(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets2 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))+(\'_\')*\.";
        string sets3 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets4 = @"(\w*)(\s|\t)*\=(\s|\t)*chr\(\d+\)\.\.chr\(\d+\)\.";
        string set5 = @"(\w+)(\s|\t)*(\=(\s|\t)*)(((\'|\"")(\w|\d)(\'|\""))|(chr))";

        string tokens1 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*\.";
        string tokens2 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*\w*(\*|\+)*\.";
        string tokens3 = @"\w+(\s|\t)*\=(\s|\t)*\w+(\s|\t)*(\(\w+((\|\w+\))|(\))))(\*|\+)*(\s|\t)*(check)*\.";
        string tokens4 = @"\w+(\s|\t)*\=((\s|\t)*\w+(\s|\t)*)+\.";
        string token5 = @"(\w+(\s|\t)*(\=)(\s|\t)*)(?=\w+)(?!chr)|(((\'|\"")(?!\+)(\W)(\'|\"")))";

        string sim1 = @"(((\"" |\')(\w+|\+|\*|\=|\<\>|\<|\>|\>\=|\<\=|\-)(\""|\'))(\,)*)+(\s|\t)((left|right)(?!\w)\.)*";
        string sim2 = @"(\'|\"")(\W|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\"")(\,(\'|\"")(\<\>|\<\=|\>\=|and|mod|div|not)(\'|\""))*(\,)*(\s|\t)*(left(?!\w)(\s|\t)*\.|right(?!\w)(\s|\t)*.)";
        string sim3 = @"((\'|\"")(\=|\<|\>|\,|\*|\+|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\"")(\,(\'|\"")(\=|\<|\>|\,|\*|\+|\<\>|\<\=|\>\=|and|mod|div|not)(\'|\""))*(\,)*(\s|\t)*)(left(?!\w)(\s|\t)*\.|right(?!\w)(\s|\t)*\.)";

        string key1 = @"((\'|\"")\w+(\'|\""))(\,|\.)";

        string comm = @"(comments|comentario)(\s|\t)*(\'|\"")(\W+(\'|\"")(\s|\t)*(to)(\s|\t)*(\'|\"")\W+(\'|\"")(\s|\t)*)(comentario\.)";

        string prod = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*((\'\w+\')|(\<\w+\>|\?|(\'(\-|\+)\')|(\w+)|\|))*(\s|\t)*(\{\w+\})*\.";
        string prod2 = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*(\?)?(\s|\t)*(\{\w+\})\.";
        string prod3 = @"((\<\w+\>)(\s|\t)*(\-\>)?)(\s|\t)*((\<\w+\>)*(\s|\t))*";
        #endregion

        public Analizador()
        {
            causaerror = "";
            linea = 0;
            punto = false;
            check = false;
            end = false;
            start = false;
            comilla = 0; parentesis = 0; corchete = 0; comillas = 0; lesser = 0;
        }
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
            //ver si viene definido units
            if (Regex.IsMatch(arreglo[linea].ToLower(), @"units")) //la palabra units es encontrada
            {
                if (!Regex.IsMatch(arreglo[linea].ToLower(), units)) //verifica que esten bien escritas
                {
                    causaerror = "Units mal definidas" + linea + "\n" + arreglo[linea];
                    return;
                }
                else
                {
                    linea++;
                    if (Regex.IsMatch(arreglo[linea].ToLower(), @"sets"))
                    {
                        linea++;
                        EstructuraSets();
                    }
                    else if (Regex.IsMatch(arreglo[linea].ToLower(), set5))
                    {
                        causaerror = "Palabra set no definida " + linea;
                        return;
                    }
                }
            }
            else if (Regex.IsMatch(arreglo[linea].ToLower(), @"sets"))
            {
                linea++;
                EstructuraSets();
            }
            else
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), @"tokens"))
                {
                    linea++;
                    EstructuraTokens();
                }
                if (Regex.IsMatch(arreglo[linea].ToLower(), sets1) | Regex.IsMatch(arreglo[linea].ToLower(), sets2) | Regex.IsMatch(arreglo[linea].ToLower(), sets3)
               | Regex.IsMatch(arreglo[linea].ToLower(), sets4))
                {
                    causaerror = "La palabra sets no fue encontrada " + linea + "\n" + arreglo[linea];
                    return;
                }
                else if (validarpalabratoken())
                {
                    return;
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
                    if (!validarpalabratoken())
                    {
                        causaerror = "Set mal definido " + linea + "\n" + arreglo[linea]; ;
                        return;
                    }
                    if (causaerror != "")
                    {
                        return;
                    }

                }
            }
            linea++;
            EstructuraTokens();
        }

        bool validarpalabratoken()
        {
            if (Regex.IsMatch(arreglo[linea], token5))
            {
                causaerror = "Palabra token no definida " + linea + "\n" + arreglo[linea];
                return true;
            }
            return false;
        }

        void EstructuraTokens()
        {
            while (!Regex.IsMatch(arreglo[linea].ToLower(), @"keywords") & !Regex.IsMatch(arreglo[linea].ToLower(), @"productions") &
                !Regex.IsMatch(arreglo[linea].ToLower(), @"coments"))
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), tokens1) | Regex.IsMatch(arreglo[linea].ToLower(), tokens2) | Regex.IsMatch(arreglo[linea].ToLower(), tokens3)
                | Regex.IsMatch(arreglo[linea].ToLower(), tokens4))
                {
                    linea++;
                }
                else
                {
                    if (!AnalisisToken())
                    {
                        return;
                    }
                }
            }
            if (Regex.IsMatch(arreglo[linea].ToLower(), @"keywords"))
            {
                linea++;
                EstructuraKeywords();
            }
            else if (Regex.IsMatch(arreglo[linea].ToLower(), @"productions"))
            {
                linea++;
                EstructuraProducctiones();
            }
        }

        void EstructuraKeywords()
        {
            while (!Regex.IsMatch(arreglo[linea].ToLower(), @"productions"))
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), key1) | Regex.IsMatch(arreglo[linea], comm))
                {
                    if (!AnalisisKeywords())
                    {
                        causaerror = "Keywords mal definidas " + linea + "\n" + arreglo[linea];
                        return;
                    }
                    linea++;
                }
                else
                {
                    causaerror = "Keywords mal definidas " + linea + "\n" + arreglo[linea];
                    return;
                }
            }
            linea++;
            EstructuraProducctiones();
        }

        void EstructuraProducctiones()
        {
            while (linea < (arreglo.Count()))
            {
                if (!Regex.IsMatch(arreglo[linea], @"end"))
                {
                    if ((Regex.IsMatch(arreglo[linea].ToLower(), prod) | Regex.IsMatch(arreglo[linea].ToLower(), prod2)
                        | Regex.IsMatch(arreglo[linea].ToLower(), prod3) | Regex.IsMatch(arreglo[linea].ToLower(), @"\|(\s|\t)*(\?)(\.)")) && AnalisisProduction())
                    {

                        punto = false;
                        linea++;
                        if (Regex.IsMatch(arreglo[linea].ToLower(), @"(start)(\s|\t)*\=(\s|\t)*(\<\w+\>(\s|\t)*.)"))
                        {
                            start = true;
                        }
                    }
                    else
                    {
                        if (!AnalisisProduction())
                        {
                            causaerror = "Produccion mal definida " + linea + "\n" + arreglo[linea];
                            return;
                        }
                        linea++;
                    }
                }
                else
                {
                    end = true;
                    if (start != true)
                    {
                        causaerror = "Falta produccion start";
                        return;
                    }
                    return;
                }
            }
            if (end == false)
            {
                causaerror = "Falta End. Al final del archivo";
                return;
            }
            return;
        }

        bool AnalisisToken()
        {
            string line = arreglo[linea];
            char[] cline = line.ToCharArray();
            comilla = 0; parentesis = 0; corchete = 0; comillas = 0;
            punto = false;
            #region MyRegion
            foreach (char item in cline)
            {
                switch (item)
                {
                    case '+':
                        Direccional();
                        break;
                    case '-':
                        Direccional();
                        break;
                    case '*':
                        Direccional();
                        break;
                    case '<':
                        Direccional();
                        break;
                    case '>':
                        Direccional();
                        break;
                    case '=':
                        break;
                    case '\'':
                        comillas++;
                        break;
                    case '"':
                        comilla++;
                        break;
                    case '(':
                        parentesis++;
                        break;
                    case ')':
                        parentesis++;
                        break;
                    case ',':
                        break;
                    case ' ':
                        break;
                    case '.':
                        punto = true;
                        break;
                    case 't':
                        Direccional();
                        break;
                    default:
                        //puede ser una letra

                        if (Char.IsLetter(item))
                        {
                            palabra += item;
                        }
                        break;
                }
            }
            #endregion

            if (comilla % 2 != 0)
            {
                causaerror = "falta o sobra una comilla " + linea + "\n" + arreglo[linea];
                return false;
            }
            if (comillas % 2 != 0)
            {
                causaerror = "falta o sobra una comilla simple " + linea + "\n" + arreglo[linea];
                return false;
            }
            if (parentesis % 2 != 0)
            {
                causaerror = "falta parentesis";
                return false;
            }
            if (punto == false)
            {
                causaerror = "Falta punto al final " + linea + "\n" + arreglo[linea];
            }
            linea++;
            return true;
        }

        bool AnalisisProduction()
        {
            string line = arreglo[linea];
            char[] cline = line.ToCharArray();
            comilla = 0; parentesis = 0; corchete = 0; comillas = 0; lesser = 0;
            #region forech
            foreach (char item in cline)
            {
                switch (item)
                {
                    case '+':
                        break;
                    case '-':
                        break;
                    case '*':
                        break;
                    case '<':
                        lesser++;
                        break;
                    case '>':
                        lesser++;
                        break;
                    case '{':
                        corchete++;
                        break;
                    case '}':
                        corchete++;
                        break;
                    case '=':
                        break;
                    case '\'':
                        comillas++;
                        break;
                    case '"':
                        comilla++;
                        break;
                    case ',':
                        break;
                    case ' ':
                        break;
                    case '.':
                        punto = true;
                        break;
                    case '?':
                        break;
                    default:
                        //puede ser una letra

                        if (Char.IsLetter(item))
                        {
                            //do something
                        }
                        break;
                }
            }
            #endregion
            //(\<\w+\>)(\s|\t)*(\-\>)
            if (comilla % 2 != 0 | comillas % 2 != 0)
            {
                causaerror = "falta o sobra una comilla " + linea + "\n" + arreglo[linea];
                return false;
            }
            if (corchete % 2 != 0)
            {
                causaerror = "falta o sobra un corchete " + linea + "\n" + arreglo[linea];
                return false;
            }
            if (Regex.IsMatch(arreglo[linea].ToLower(), @"(\<\w+\>)(\s|\t)*(\-\>)"))
            {
                if ((lesser - 1) % 2 != 0)
                {
                    causaerror = "falta o sobra un signo < ó > " + linea + "\n" + arreglo[linea];
                    return false;
                }
            }
            else
            {
                if ((lesser) % 2 != 0)
                {
                    causaerror = "falta o sobra un signo < ó > " + linea + "\n" + arreglo[linea];
                    return false;
                }
            }
            if (parentesis % 2 != 0)
            {
                causaerror = "falta parentesis";
                return false;
            }
            if (Regex.IsMatch(arreglo[linea + 1].ToLower(), @"(\<\w+\>)(\s|\t)*(\-\>)") && punto == false)
            {
                causaerror = "Falta punto al final de la produccion";
                return false;
            }
            if (Regex.IsMatch(arreglo[linea].ToLower(), @"(start)(\s|\t)*\=(\s|\t)*(\<\w+\>(\s|\t)*.)"))
            {
                start = true;
            }
            return true;
        }

        void AProduction()
        {
            string line = arreglo[linea];
            char[] cline = line.ToCharArray();
            comilla = 0; parentesis = 0; corchete = 0; comillas = 0; lesser = 0;
            #region forech
            foreach (char item in cline)
            {
                switch (item)
                {
                    case '+':
                        break;
                    case '-':
                        break;
                    case '*':
                        break;
                    case '<':
                        if (!Regex.IsMatch(arreglo[linea], @"\<[^\d]\w +\>"))
                        {
                            causaerror = "Error de ID " + linea + "\n" + arreglo[linea]; ;
                            return;
                        }
                        lesser++;
                        break;
                    case '>':
                        lesser++;
                        break;
                    case '{':
                        corchete++;
                        break;
                    case '}':
                        corchete++;
                        break;
                    case '=':
                        break;
                    case '\'':
                        comillas++;
                        break;
                    case '"':
                        comilla++;
                        break;
                    case ',':
                        break;
                    case ' ':
                        break;
                    case '.':
                        punto = true;
                        break;
                    case '?':
                        break;
                    default:
                        //puede ser una letra

                        if (Char.IsLetter(item))
                        {
                            //do something
                        }
                        break;
                }
            }
            #endregion
            //(\<\w+\>)(\s|\t)*(\-\>)
            if (comilla % 2 != 0 | comillas % 2 != 0)
            {
                causaerror = "falta o sobra una comilla " + linea + "\n" + arreglo[linea];
                return;
            }
            if (corchete % 2 != 0)
            {
                causaerror = "falta o sobra un corchete " + linea + "\n" + arreglo[linea];
                return;
            }
            if (lesser % 2 != 0)
            {
                causaerror = "falta o sobra un signo < ó > " + linea + "\n" + arreglo[linea];
                return;
            }
            if (parentesis % 2 != 0)
            {
                causaerror = "falta parentesis";
                return;
            }

            linea++;
            return;
        }

        bool AnalisisKeywords()
        {
            string line = arreglo[linea];
            char[] cline = line.ToCharArray();
            comilla = 0; parentesis = 0; corchete = 0; comillas = 0; lesser = 0;
            foreach (char item in cline)
            {
                switch (item)
                {
                    case '"':
                        comilla++;
                        break;
                    case '\'':
                        comillas++;
                        break;
                    default:
                        //puede ser una letra
                        if (Char.IsLetter(item))
                        {
                            //concatenar y guardar
                        }
                        break;
                }

            }
            if (comilla % 2 != 0 | comillas % 2 != 0)
            {
                return false;
            }
            return true;
        }

        void Direccional()
        {
            string sentence = arreglo[linea];
            string pattern = @"(left|right)(?!\w)\.";
            int val = 0;
            foreach (Match match in Regex.Matches(sentence, pattern, RegexOptions.IgnoreCase))
            {
                val++;
            }
            foreach (Match match in Regex.Matches(sentence, @"[^left|right][^a-z]\.", RegexOptions.IgnoreCase))
            {
                val++;
            }
            if (val == 0)
            {
                causaerror = "error linea " + linea + "\n" + sentence;
                return;
            }
        }
    }
}
