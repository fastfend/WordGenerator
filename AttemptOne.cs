using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dev_WordRand_test1
{
    class Program
    {
        static string Word = "";
        static char[] set_a = { 'e', 'g', 'h', 'w' }; //33%
        static int prob_a = 33;
        static char[] set_b = { 'a', 'k', 't', 'n', 'r' }; //28%
        static int prob_b = 28;
        static char[] set_c = { 's', 'i', 'y', 'l' }; //19%
        static int prob_c = 19;
        static char[] set_d = { 'u', 'd', 'f', 'z', 'v', 'p' }; //13%
        static int prob_d = 13;
        static char[] set_e = { 'j', 'o' }; //7%
        static int prob_e = 7;
        static char[] set_f = { 'b', 'c', 'm', 'x' }; //0%
        static int prob_f = 0;

        static void Main(string[] args)
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(RandomWord());
            }
            Console.Read();
        }
        static char RandomChar()
        {
            char val = 'a';
            int rand = StaticRandom.Instance.Next(1, 100);

            if (rand == 0)
            {
                int f = StaticRandom.Instance.Next(0, set_f.Count() - 1);
                val = set_f[f];
            }
            if (rand > 0 && rand <= 7)
            {
                int e = StaticRandom.Instance.Next(0, set_e.Count() - 1);
                val = set_e[e];
            }
            if (rand > 7 && rand <= 20)
            {
                int d = StaticRandom.Instance.Next(0, set_d.Count() - 1);
                val = set_d[d];
            }
            if (rand > 20 && rand <= 39)
            {
                int c = StaticRandom.Instance.Next(0, set_c.Count() - 1);
                val = set_c[c];
            }
            if (rand > 39 && rand <= 67)
            {
                int b = StaticRandom.Instance.Next(0, set_b.Count() - 1);
                val = set_b[b];
            }
            if (rand > 67 && rand <= 100)
            {
                int a = StaticRandom.Instance.Next(0, set_a.Count() - 1);
                val = set_a[a];
            }
            if (Word != "" && val == Word.Last())
            {
                return RandomChar();
            }
            else
            {
                return val;
            }
            
        }
        static char RandomChar(char exclude)
        {
            char val = 'a';
            int rand = StaticRandom.Instance.Next(1, 100);

            if (rand == 0)
            {
                int f = StaticRandom.Instance.Next(0, set_f.Count() - 1);
                val = set_f[f];
            }
            if (rand > 0 && rand <= 7)
            {
                int e = StaticRandom.Instance.Next(0, set_e.Count() - 1);
                val = set_e[e];
            }
            if (rand > 7 && rand <= 20)
            {
                int d = StaticRandom.Instance.Next(0, set_d.Count() - 1);
                val = set_d[d];
            }
            if (rand > 20 && rand <= 39)
            {
                int c = StaticRandom.Instance.Next(0, set_c.Count() - 1);
                val = set_c[c];
            }
            if (rand > 39 && rand <= 67)
            {
                int b = StaticRandom.Instance.Next(0, set_b.Count() - 1);
                val = set_b[b];
            }
            if (rand > 67 && rand <= 100)
            {
                int a = StaticRandom.Instance.Next(0, set_a.Count() - 1);
                val = set_a[a];
            }

            if (val == exclude)
            {
                return RandomChar(exclude);
            }
            else
            {
                return val;
            }
        }
        static string RandomWord()
        {
            string valx = RandomChar().ToString();

            for(int u = 0; u <= 6; u++)
            {
                string fin = RandomChar().ToString();
                int x = StaticRandom.Instance.Next(1, 100);
                bool idf_a = (x <= 45);
                bool idf_b = (x > 45 && x <= 90);
                bool idf_c = (x > 90);
                bool idx_a = (x <= 20);
                bool idx_b = (x > 20 && x <= 40);
                bool idx_c = (x > 40 && x <= 60);
                bool idx_d = (x > 60);

                if (valx[valx.Count() - 1] == 'g')
                {
                    if(idf_a)
                    {
                        fin = "r";
                    }
                    if(idf_b)
                    {
                        fin = "h";
                    }
                    if(idf_c)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "h" || fin == "r")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                if (valx[valx.Count() - 1] == 'w')
                {
                    if (idf_a)
                    {
                        fin = "ra";
                    }
                    if (idf_b)
                    {
                        fin = "r";
                    }
                    if(idf_c)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "r")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                if (valx[valx.Count() - 1] == 'n')
                {
                    if (idf_a)
                    {
                        fin = "gh";
                    }
                    if (idf_b || idf_c)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "g")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                if (valx[valx.Count() - 1] == 'r')
                {
                    if(idx_a)
                    {
                        fin = "ah";
                    }
                    if(idx_b)
                    {
                        fin = "g";
                    }
                    if(idx_c)
                    {
                        fin = "h";
                    }
                    if(idx_d)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "g" || fin == "h" || fin == "a")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                if (valx[valx.Count() - 1] == 'a')
                {
                    if (idx_a)
                    {
                        fin = "h";
                    }
                    if (idx_b)
                    {
                        fin = "kh";
                    }
                    if (idx_c || idx_d)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "h" || fin == "k")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                if (valx[valx.Count() - 1] == 't')
                {
                    if (idx_a)
                    {
                        fin = "r";
                    }
                    if (idx_b || idx_c || idx_d)
                    {
                        fin = RandomChar().ToString();
                        while (fin == "r")
                        {
                            fin = RandomChar().ToString();
                        }
                    }
                }

                valx += fin;
                Word = valx;
            }

            
            return valx;
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