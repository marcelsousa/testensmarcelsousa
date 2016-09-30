using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteStream
{
    public class Program
    {
        static List<char> vogais = new List<char>() { 'A', 'E', 'O', 'I', 'U' };

        /// <summary>
        /// Método de entrada da aplicação para a pesquisa de Vogal, após uma consoante e que não se repita no resto da stream. 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Insira uma sequencia de caracteres. O programa irá encontrar o primeiro caracter Vogal, após uma consoante e que não se repita no resto da stream.");
            Console.Write("Digite :");
            var str = Console.ReadLine();
            var tStr = new TesteStream(str);
            var vogal = firstChar(tStr);
            Console.WriteLine(vogal == null ? "Não foi encontrada nenhuma vogal, conforme a regra acima." : $"O primeiro caracter Vogal da stream que não se repete após a primeira Consoante é: " + vogal);
            Console.ReadLine();
        }

        /// <summary>
        /// Método para pesquisar o primeiro caracter Vogal, após uma consoante e que não se repita no resto da stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>char?</returns>
        public static char? firstChar(TesteStream stream)
        {
            char? chrVogal = null;
            char? chrUltimo = null;
            List<char> vogaisEncontradas = new List<char>();

            while (stream.hasNext())
            {
                char chrAtual = stream.getNext();

                if (Char.IsLetter(chrAtual))
                {
                    if (chrUltimo != null)
                    {
                        //Se o ultimo char nao for vogal e o atual for...
                        if (!isVogal(chrUltimo.Value) && isVogal(chrAtual))
                        {
                            //Adiciona na lista de vogais encontradas
                            chrVogal = chrAtual;
                            vogaisEncontradas.Add(char.ToUpper(chrVogal.Value));
                        }
                    }

                    chrUltimo = chrAtual;
                }
            }

            //Agrupa por total de vogais encontradas
            var lstVogais = vogaisEncontradas.GroupBy(g => g);

            //Pega a primeira vogal que não repetiu
            var primeiraVogal = lstVogais.Where(x => x.Count() == 1).FirstOrDefault();

            if (primeiraVogal != null)
                return primeiraVogal.Key;
            else if (chrVogal != null)
                return chrVogal;
            else
                return null;
        }

        /// <summary>
        /// Verifica se é uma vogal
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool isVogal(char chr)
        {
            return vogais.Contains(char.ToUpper(chr));
        }
    }
}
