using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace YieldReturnCode
{
    public class Analisador
    {
        static string pathArquivoPalavras
        {
            get
            {
                string currentDir = System.IO.Directory.GetCurrentDirectory();
                string[] diretorios = currentDir.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                return String.Join("\\", diretorios.Take(diretorios.Length - 2)) + "\\Palavras.txt";
            }
        }
        static string palavraProcurarProximaAoInicio = "inconstitucionalissimamente";
        static string palavraProcurarProximaAoMeio = "paralelepipedo";
        static string palavraProcurarProximaAoFim = "pneumonia";
        static Stopwatch stopwatch = new Stopwatch();

        public static void AnalisarBuscaDePalavras()
        {
            Func<long, long, string> obterMultiplicador = (primeiroVal, segundoVal) => (Convert.ToDouble(primeiroVal) / segundoVal).ToString("N2");

            TimeSpan tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoInicio);
            TimeSpan tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoInicio);
            Console.WriteLine(String.Format("Busca do índice da palavra do texto com yield foi {0} vezes mais rápida do que sem o yield", obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks)));
            Console.WriteLine();

            tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoMeio);
            tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoMeio);
            Console.WriteLine(String.Format("Busca do índice da palavra do texto com yield foi {0} vezes mais rápida do que sem o yield", obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks)));
            Console.WriteLine();

            tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoFim);
            tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoFim);
            Console.WriteLine(String.Format("Busca do índice da palavra do texto com yield foi {0} vezes mais rápida do que sem o yield", obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks)));

            Console.ReadKey();
        }

        private static TimeSpan AnalisarTempoChamada_SemYield(string filePath, string palavra)
        {
            stopwatch.Reset();
            stopwatch.Start();
            int indicePalavraNoArquivo_SemYieldReturn = IndicePalavraNoArquivo_SemYieldReturn(filePath, palavra);
            stopwatch.Stop();
            TimeSpan tempoSemYield = stopwatch.Elapsed;
            Console.WriteLine(String.Format("Índice: {0}, tempo: {1} NÃO utilizando Yield Return", indicePalavraNoArquivo_SemYieldReturn, tempoSemYield.ToString()));

            return tempoSemYield;
        }

        private static TimeSpan AnalisarTempoChamada_ComYield(string filePath, string palavra)
        {
            stopwatch.Reset();
            stopwatch.Start();
            int indicePalavraNoArquivo_ComYieldReturn = IndicePalavraNoArquivo_ComYieldReturn(filePath, palavra);
            stopwatch.Stop();
            TimeSpan tempoComYield = stopwatch.Elapsed;
            Console.WriteLine(String.Format("Índice: {0}, tempo: {1}  utilizando Yield Return", indicePalavraNoArquivo_ComYieldReturn, tempoComYield.ToString()));

            return tempoComYield;
        }

        private static int IndicePalavraNoArquivo_ComYieldReturn(string filePath, string palavraProcurar)
        {
            int indice = 0;
            IEnumerable<string> palavrasComYieldReturn = FileTextHelper.LerPalavrasDoArquivo_ComYieldReturn(filePath);
            foreach (string palavra in palavrasComYieldReturn)
            {
                if (palavra == palavraProcurar)
                    return indice;
                indice++;
            }

            return -1;
        }

        private static int IndicePalavraNoArquivo_SemYieldReturn(string filePath, string palavraProcurar)
        {
            int indice = 0;
            List<string> palavrasSemYieldReturn = FileTextHelper.LerPalavrasDoArquivo_SemYieldReturn(filePath);
            foreach (string palavra in palavrasSemYieldReturn)
            {
                if (palavra == palavraProcurar)
                    return indice;
                indice++;
            }

            return -1;
        }
    }
}
