using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dev_WordRand_test2
{
    class Program
    {
        static string memory_Word = "";
        static string[] vovels = { "ai", "al", "ar", "au", "aw", "ay", "ea", "ear", "ee", "ei", "er", "eu", "ew", "ey", "ie", "ir", "oo", "or", "ou", "ought", "ow", "oy", "ue", "ui", "ur", "uy", "wa", "a", "e", "i", "o", "u", "y" };
        static string[] twol_vovels = { "ai", "al", "ar", "au", "aw", "ay", "ea", "ear", "ee", "ei", "er", "eu", "ew", "ey", "ie", "ir", "oo", "or", "ou", "ought", "ow", "oy", "ue", "ui", "ur", "uy", "wa" };
        static string[] onel_vovels = { "a", "e", "i", "o", "u", "y" };
        static void Main(string[] args)
        {
            for(int x = 0; x < 30; x++)
            {
                //Console.WriteLine(RandomChar());

                //Console.WriteLine(RandomCharsNoRepat(5));

                string wordx = RandomWord(8);

                Console.WriteLine("Słowo przed obróbką: "+ wordx);
                Console.WriteLine("   Słowo po obróbce: " + FilterizeGoodVovels(wordx));
                Console.WriteLine();
            }
            Console.Read();
        }

        static string RandomChar()
        {
            int x = StaticRandom.Instance.Next(1, 10000);
            if (x >= 1 && x < 7) //Z 7
            {
                return "z";
            }
            if (x >= 7 && x < 17) //Q 10
            {
                return "q";
            }
            if (x >= 17 && x < 30) //X 13
            {
                return "x";
            }
            if (x >= 30 && x < 45) //J 15
            {
                return "j";
            }
            if (x >= 45 && x < 122) //K 77
            {
                return "k";
            }
            if (x >= 122 && x < 220) //V 98
            {
                return "v";
            }
            if (x >= 220 && x < 369) //B 149
            {
                return "b";
            }
            if (x >= 369 && x < 562) //P 193 
            {
                return "p";
            }
            if (x >= 562 && x < 759) //Y 197
            {
                return "y";
            }
            if (x >= 759 && x < 961) //G 202
            {
                return "g";
            }
            if (x >= 961 && x < 1184) //F 223
            {
                return "f";
            }
            if (x >= 1184 && x < 1420) //W 236
            {
                return "w";
            }
            if (x >= 1420 && x < 1661) //M 241
            {
                return "m";
            }
            if (x >= 1661 && x < 2215) //C 276
            {
                return "c";
            }
            if (x >= 2215 && x < 2618) //L 278
            {
                return "l";
            }
            if (x >= 2618 && x < 3034) //D 425
            {
                return "d";
            }
            if (x >= 3034 && x < 3642) //R 599
            {
                return "r";
            }
            if (x >= 3642 && x < 4251) //H 609
            {
                return "h";
            }
            if (x >= 4251 && x < 4884) //S 633
            {
                return "s";
            }
            if (x >= 4884 && x < 5559) //N 675
            {
                return "n";
            }
            if (x >= 5559 && x < 6256) //I 697
            {
                return "i";
            }
            if (x >= 6256 && x < 7007) //O 751
            {
                return "o";
            }
            if (x >= 7007 && x < 7824) //A 817
            {
                return "a";
            }
            if (x >= 7824 && x < 8730) //T 906
            {
                return "t";
            }
            if (x >= 8730 && x < 10000) //E 1270
            {
                return "e";
            }

            return x.ToString();
        }
        static string RandomCharNoRepeat()
        {
            string norepeat = RandomChar();
            if (string.IsNullOrEmpty(memory_Word))
            {
                memory_Word += norepeat;
                return norepeat;
            }
            else
            {
                while (memory_Word.Last() == norepeat[0])
                {
                    norepeat = RandomChar();
                }
                memory_Word += norepeat;
                return norepeat;
            }

        }
        static string RandomWord(int x)
        {
            string val = "";
            
            for (int i = 0; i <= x; i++)
            {
                string let = RandomChar();
                while (IsLikeLast(let, val))
                {
                    let = RandomChar();
                }
                val += let;
            }
            if(ContainsAnyVovel(val))
            {
                return val;
            }
            else
            {
                return RandomWord(x);
            }
            
        }

        static bool IsLikeLast(string letter, string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            if (word.Last().ToString() == letter)
            {
                return true;
            }
            return false;
        }
        static bool ContainsAnyVovel(string word)
        {
            foreach (string x in vovels)
            {
                if (word.Contains(x))
                {
                    return true;
                }
            }
            
            return false;
        }
        static List<string> VovelsInWord(string word)
        {
            string interword = word;
            List<string> vov = new List<string>();
            foreach(string vovl in vovels)
            {
                if (interword.Contains(vovl))
                {
                    vov.Add(vovl);
                    interword = interword.Replace(vovl, "_");
                }
            }
            
            return vov;

        }

        static string FilterizeGoodVovels(string word)
        {
            List<string> vo = VovelsInWord(word);
            string val = word;
            foreach (string v in vo)
            {
                if (v.Count() == 2)
                {
                    val = val.Replace(v, "__");
                }
                else
                {
                    val = val.Replace(v, "_");
                }
            }

            int count = -1;
            string inmem = "";
            val += "0";
            string val2 = word;
            foreach (char ch in val)
            {
                count++;

                if(ch == '_')
                {
                    inmem += word[count];
                }
                else
                {
                    if (!string.IsNullOrEmpty(inmem))
                    {
                        if (inmem.Count() > 2)
                        {
                            foreach (string twvl in twol_vovels)
                            {
                                if (inmem.Contains(twvl))
                                {
                                    val2 = val2.Replace(inmem, twvl);
                                    break;
                                }
                            }
                        }
                    }
                    inmem = "";
                }
            }
            return val2;
        }
        //static string FilterizeBadVovels(string word)
        //{
        //    List<string> vo = VovelsInWord(word);

        //}
        public static class StaticRandom
        {
            private static int seed;

            private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
                (() => new Random(Interlocked.Increment(ref seed)));

            static StaticRandom()
            {
                seed = Environment.TickCount;
            }

            public static Random Instance { get { return threadLocal.Value; } }
        }
    }
}
