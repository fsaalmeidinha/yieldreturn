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

            Console.WriteLine("A palavra procurada estava proxima ao início do arquivo");
            TimeSpan tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoInicio);
            TimeSpan tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoInicio);
            Console.Write("Busca do índice da palavra do texto com yield foi ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks) + " vezes mais rápida");
            Console.ResetColor();
            Console.WriteLine(" do que sem o yield");
            Console.WriteLine();

            Console.WriteLine("A palavra procurada estava proxima ao meio do arquivo");
            tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoMeio);
            tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoMeio);
            Console.Write("Busca do índice da palavra do texto com yield foi ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks) + " vezes mais rápida");
            Console.ResetColor();
            Console.WriteLine(" do que sem o yield");
            Console.WriteLine();

            Console.WriteLine("A palavra procurada estava proxima ao final do arquivo");
            tsSemYield = AnalisarTempoChamada_SemYield(pathArquivoPalavras, palavraProcurarProximaAoFim);
            tsComYield = AnalisarTempoChamada_ComYield(pathArquivoPalavras, palavraProcurarProximaAoFim);
            Console.Write("Busca do índice da palavra do texto com yield foi ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(obterMultiplicador(tsSemYield.Ticks, tsComYield.Ticks) + " vezes mais rápida");
            Console.ResetColor();
            Console.WriteLine(" do que sem o yield");

            Console.ReadKey();
        }

        private static TimeSpan AnalisarTempoChamada_SemYield(string filePath, string palavra)
        {
            stopwatch.Reset();
            stopwatch.Start();
            int indicePalavraNoArquivo_SemYieldReturn = IndicePalavraNoArquivo_SemYieldReturn(filePath, palavra);
            stopwatch.Stop();
            TimeSpan tempoSemYield = stopwatch.Elapsed;

            Console.Write(String.Format("Índice: {0}, tempo: ", indicePalavraNoArquivo_SemYieldReturn));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tempoSemYield.ToString() + " NÃO utilizando Yield Return");
            Console.ResetColor();

            return tempoSemYield;
        }

        private static TimeSpan AnalisarTempoChamada_ComYield(string filePath, string palavra)
        {
            stopwatch.Reset();
            stopwatch.Start();
            int indicePalavraNoArquivo_ComYieldReturn = IndicePalavraNoArquivo_ComYieldReturn(filePath, palavra);
            stopwatch.Stop();
            TimeSpan tempoComYield = stopwatch.Elapsed;

            Console.Write(String.Format("Índice: {0}, tempo: ", indicePalavraNoArquivo_ComYieldReturn));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tempoComYield.ToString() + " utilizando Yield Return");
            Console.ResetColor();

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
