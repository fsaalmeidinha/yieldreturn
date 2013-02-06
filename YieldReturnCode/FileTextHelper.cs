using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YieldReturnCode
{
    public class FileTextHelper
    {
        static char[] separadorPalavras = new char[] { ' ' };

        public static IEnumerable<string> LerPalavrasDoArquivo_ComYieldReturn(string filePath)
        {
            foreach (string linha in System.IO.File.ReadLines(filePath))
            {
                foreach (string palavra in linha.Split(separadorPalavras, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return palavra;
                }
            }
        }

        public static List<string> LerPalavrasDoArquivo_SemYieldReturn(string filePath)
        {
            List<string> palavras = new List<string>();

            foreach (string linha in System.IO.File.ReadLines(filePath))
            {
                foreach (string palavra in linha.Split(separadorPalavras, StringSplitOptions.RemoveEmptyEntries))
                {
                    palavras.Add(palavra);
                }
            }

            return palavras;
        }
    }
}
