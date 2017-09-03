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
        #endregion
        #region patterns
        string compiler = @"((\s|\t)*(compiler)(\s|\t)*(\w)+(\s|\t)*\.)";
        string units = @"(units)(\s|\t)*(\w*\,\w+)+\.";
        string ssets = @"sets";
        string sets1 = @"(\w*)(\s|\t)*\=(\s|\t)*(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets2 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))+(\'\_\')*\.";
        string set3 = @"(\w*)(\s|\t)*\=(\s|\t)*((\'(\w|\d)\')\.\.(\'(\w|\d)\')(\+))(\'(\w|\d)\')\.\.(\'(\w|\d)\')\.";
        string sets4 = @"(\w*)(\s|\t)*\=(\s|\t)*chr\(\d+\)\.\.chr\(\d+\)\.";
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
            catch (Exception)
            {
                causaerror = "error inesperado";
            }
        }

        void Cuerpo()
        {
            if (Regex.IsMatch(arreglo[linea].ToLower(), units))
            {
                linea++;
                if (Regex.IsMatch(arreglo[linea].ToLower(), ssets))
                {
                    EstructuraSets();
                }
            }
            else
            {
                if (Regex.IsMatch(arreglo[linea].ToLower(), @"units"))
                {
                    causaerror = "Units mal definido " + linea;
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

        }
    }
}
