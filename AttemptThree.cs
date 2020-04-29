using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dev_WordRand_test3
{
    class Program
    {
        static string[] vovels = { "a", "ą", "e", "ę", "i", "o", "ó", "u", "y" };
        static string[] nonvovels = { "b", "c", "ć", "d", "f", "g", "h", "j", "k", "l", "ł", "m", "n", "ń", "p", "r", "s", "ś", "t", "w", "z", "ż", "ź" };

        static bool IsVovel(char isx)
        {
            foreach (string vovel in vovels)
            {
                if (isx == vovel[0])
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            List<string> sl = new List<string>();
            List<List<string>> Vx = new List<List<string>>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] sx = File.ReadAllLines("words.txt", Encoding.UTF8);
            foreach (string s in sx)
            {
                List<string> VCV = new List<string>();
                string ins = s.ToLowerInvariant();
                foreach (string vov in vovels)
                {
                    ins = ins.Replace(vov, "V");
                }
                foreach (string nonvov in nonvovels)
                {
                    ins = ins.Replace(nonvov, "C");
                }

                int couter = -1;
                
                string temp = "";
                foreach (char xads in ins) //CVVCVC
                {
                    couter++;
                    
                    if (xads == 'C')
                    {
                        if (couter == (s.Count() - 1))
                        {
                            temp += s[couter];
                            VCV.Add(temp);
                            temp = "";
                            temp += s[couter];
                        }
                        else
                        {
                            temp += s[couter];
                        }
                    }
                    else //if V
                    {
                        if (ins.Count() > (couter + 1) && ins[couter + 1] == 'V')
                        {
                            temp += s[couter];
                        }
                        else
                        {
                            if (couter == 0)
                            {
                                temp += s[couter];
                            }
                            else
                            {
                                temp += s[couter];
                                VCV.Add(temp);
                                temp = "";
                                temp += s[couter];
                            }
                        }
                        
                    }
                }
                Vx.Add(VCV);
            }

            List<List<string>> listoflists = new List<List<string>>();

            int howmany = 0;
            foreach (List<string> x in Vx)
            {
                if(howmany < x.Count())
                {
                    howmany = x.Count();
                }
            }

            for (int i = 0; i < howmany; i++)
            {
                List <string> list = new List<string>();
                listoflists.Add(list);
            }

            int counterx = -1;
            foreach (List<string> x0 in Vx)
            {
                counterx++;
                if (x0.Count == howmany)
                {
                    listoflists[howmany-1].Add(x0.Last());
                }
                if (counterx > 0 && counterx < (howmany-1))
                {
                    foreach (List<string> x1 in Vx)
                    {
                        try
                        {
                             listoflists[counterx].Add(x1[counterx]);
                        }
                        catch
                        {

                        }
                    }    
                }
                listoflists[0].Add(x0.First());
            }

            string alstchosed = "";
            string builder = "";

            List<string> word = new List<string>();
            for (int i = 0; i < 31; i++)
            {
                foreach (List<string> xeds in listoflists)
                {
                    string Rand = "";
                    while (Rand == alstchosed)
                    {
                        Rand = xeds[StaticRandom.Instance.Next(0, (xeds.Count() - 1))];
                    }
                    builder += Rand;
                }
                word.Add(builder);
                builder = "";
                alstchosed = "";
            }
            File.WriteAllLines("exec.txt", word.ToArray());
        }
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
