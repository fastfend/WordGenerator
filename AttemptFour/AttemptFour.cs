using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WordRand_base;

namespace dev_WordRand_console
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateStaticString();
            AwaitAction();
        }

        static void AwaitAction()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.KeyChar == '1')
            {
                Console.Clear();
                Console.WriteLine("Podaj minimialną ilość liter:");
                int min = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Podaj maksymalną ilość liter:");
                int max = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Podaj poziom losowości samogłoski [0 - 100]:");
                int vovel = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Podaj nazwę pliku znaków:");
                string charset = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Podaj nazwę pliku słownika:");
                string dict = Console.ReadLine();
                GeneratePseudoWord(min, max, vovel, charset, dict);
            }
            if (key.KeyChar == '9')
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                GenerateStaticString();
                AwaitAction();
            }

        }

        static void GenerateStaticString()
        {
            Console.WriteLine("Witaj w konsoli WordRandomizer");
            Console.WriteLine("Opcje:");
            Console.WriteLine("[1] Generuj losowy pseudoword");
            Console.WriteLine("[9] Wyjdź");
        }

        static void GeneratePseudoWord(int min, int max, int vovel, string charset, string dict)
        {
            RandomWord rw = new RandomWord();
            rw.MinLetters = min;
            rw.MaxLetters = max;
            rw.VovelRandom = vovel;
            rw.Libs.LoadCharSet(charset);
            rw.Libs.LoadDict(dict);
            Console.WriteLine(rw.NewWord);
            Console.Read();
            Console.Clear();
            GenerateStaticString();
            AwaitAction();
        }
    }
}
