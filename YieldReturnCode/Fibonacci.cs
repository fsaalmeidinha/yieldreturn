using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YieldReturn
{
    public class Fibonacci
    {
        public static IEnumerable<int> RecuperarSerieFibonacci_ComYield()
        {
            yield return 1;
            int lastFib = 1;
            int aux = lastFib;
            for (int actualFib = 1; ; aux = lastFib, lastFib = actualFib, actualFib += aux)
            {
                yield return actualFib;
            }
        }

        public static List<int> RecuperarSerieFibonacci_SemYield(int qtd)
        {
            List<int> serieFibo = new List<int>() { 1 };

            int lastFib = 1;
            int actualFib = 1;
            int aux = lastFib;
            while (qtd > 0)
            {
                serieFibo.Add(actualFib);

                aux = lastFib;
                lastFib = actualFib;
                actualFib += aux;

                qtd--;
            }
            return serieFibo;
        }
    }
}
