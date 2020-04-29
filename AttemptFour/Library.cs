using System;
using NHunspell;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordRand_base
{

    interface IWord
    {
        string NewWord
        {
            get;
        }
        string LastWord
        {
            get;
        }
        int MaxLetters
        {
            get;
            set;
        }
        int MinLetters
        {
            get;
            set;
        }
        int VovelRandom
        {
            get;
            set;
        }
        int Letters
        {
            get;
        }
        List<string> Syllabes
        {
            get;
        }
    }

    public class RandomWord : IWord
    {
        private Random r_letter;
        private string s_word = "";
        private string s_lastword = "";
        private int i_maxletters = 10;
        private int i_minletters = 3;
        private int i_letters = 5;
        private int i_randvovel = 80;
        private List<string> st_nonpseudosyllabes = new List<string>(); 
        private List<string> st_pseudosyllabes = new List<string>(); 

        private int ri_letterscount
        {
            get
            {
                Random rand = new Random();
                int letters = rand.Next(MinLetters, MaxLetters);
                return letters;
            }
        }
        private char ri_vovel
        {
            get
            {
                return Libs.VovelCharSet[r_letter.Next(0, Libs.VovelCharSet.Count()-1)];
            }
        }
        private char ri_nonvovel
        {
            get
            {
                return Libs.NonVovelCharSet[r_letter.Next(0, Libs.NonVovelCharSet.Count()-1)];
            }
        }

        private bool IsVovel(char x)
        {
            foreach (char y in Libs.VovelCharSet)
            {
                if (x == y)
                {
                    return true;
                }
            }
            return false;
        }

        public RandomWord()
        {
            r_letter = new Random();
            Libs = new Loader();
        }
        public RandomWord(string seed)
        {
            r_letter = new Random(Convert.ToInt32(seed));
            Libs = new Loader();
        }

        public Loader Libs;

        public string NewWord
        {
            get
            {
                HyphenPseudoSyllabe();
                foreach (string s in Syllabes)
                {
                    s_word += s;
                }
                s_lastword = s_word;
                return s_word;
            }
        }
        public string LastWord
        {
            get
            {
                return s_lastword;
            }
        }

        public List<string> Syllabes
        {
            get
            {
                return st_pseudosyllabes;
            }
            
        }

        public int VovelRandom
        {
            get
            {
                return i_randvovel;
            }
            set
            {
                if(value > 100)
                {
                    i_randvovel = 100;
                }
                else
                {
                    if (value < 0)
                    {
                        i_randvovel = 1;
                    }
                    else
                    {
                        i_randvovel = value;
                    }
                }
            }
        }
        public int MaxLetters
        {
            get
            {
                return i_maxletters;
            }
            set
            {
                if (value < 10001 && value > 1)
                {
                    if (value >= i_minletters)
                    {
                        i_maxletters = value;
                    }
                    else
                    {
                        throw new Exception("Maximum can't be smaller than minimum!");
                    }
                }
                else
                {
                    throw new Exception("Provided value is too big or too low. Range: 2-10000");
                }
            }
        }
        public int MinLetters
        {
            get
            {
                return i_minletters;
            }
            set
            {
                if (value <= 10000 && value >= 2)
                {
                    if (value <= i_maxletters)
                    {
                        i_minletters = value;
                    }
                    else
                    {
                        throw new Exception("Maximum can't be smaller than minimum!");
                    }
                }
                else
                {
                    throw new Exception("Provided value is too big or too low. Range: 2-10000");
                }
            }
        }
        public int Letters
        {
            get
            {
                return i_letters;
            }
        }

        private string CreateNewPseudoSyllabe()
        {
            string pseudoword = "";
            int random_pseudowordlenght = r_letter.Next(2, i_maxletters);

            pseudoword += ri_vovel; //Vovel always first char
            for (int x = 1; x <= random_pseudowordlenght; x++)
            {
                if (r_letter.Next(0, 1) == 0) //Beginning
                {
                    if(IsVovel(pseudoword[0])) //If the first letter is vovel, add nonvovel
                    {
                        bool good_l = false;
                        char propose = 'a';
                        while (good_l == false)
                        {
                            propose = ri_nonvovel;
                            if (pseudoword[0] == propose)
                            {
                                good_l = false;
                            }
                            else
                            {
                                good_l = true;
                                break;
                            }
                        }

                        pseudoword = ri_nonvovel + pseudoword;
                    }
                    else
                    {
                        if (r_letter.Next(0, 100) <= i_randvovel) //If less than VovelRandom add nonvovel
                        {
                            bool good_l = false;
                            char propose = 'a';
                            while (good_l == false)
                            {
                                propose = ri_nonvovel;
                                if (pseudoword[0] == propose)
                                {
                                    good_l = false;
                                }
                                else
                                {
                                    good_l = true;
                                    break;
                                }
                            }

                            pseudoword = propose + pseudoword;
                        }
                        else //Else add Vovel
                        {
                            pseudoword = ri_vovel + pseudoword;
                        }
                    }
                }
                else //End
                {
                    if(IsVovel(pseudoword[pseudoword.Count()-1])) //If the last char is vovel, add nonvovel
                    {
                        bool good_l = false;
                        char propose = 'a';
                        while (good_l == false)
                        {
                            propose = ri_nonvovel;
                            if (pseudoword[pseudoword.Count() - 1] == propose)
                            {
                                good_l = false;
                            }
                            else
                            {
                                good_l = true;
                                break;
                            }
                        }

                        pseudoword += propose;
                    }
                    else
                    {
                        if(r_letter.Next(0,100) <= i_randvovel) //If less than VovelRandom add nonvovel
                        {
                            bool good_l = false;
                            char propose = 'a';
                            while (good_l == false)
                            {
                                propose = ri_nonvovel;
                                if (pseudoword[pseudoword.Count() - 1] == propose)
                                {
                                    good_l = false;
                                }
                                else
                                {
                                    good_l = true;
                                    break;
                                }
                            }

                            pseudoword += propose;
                        }
                        else //Else add Vovel
                        {
                            pseudoword += ri_vovel;
                        }
                    }
                }
            }
            return pseudoword;
        }
        private void HyphenPseudoSyllabe()
        {
            //Create list of compund syllabes
            for (int x = 0; x < i_maxletters; x++)
            {
                st_nonpseudosyllabes.Add(CreateNewPseudoSyllabe());
            }

            //Split them to syllabes
            foreach (string pseuodsyllabe in st_nonpseudosyllabes)
            {
                Hyphen hyphen = new Hyphen(Libs.Dictionary);
                HyphenResult hr = hyphen.Hyphenate(pseuodsyllabe);
                string presyllabe = hr.HyphenatedWord;

                List<string> syllabes = new List<string>();
                string mem = "";
                int count = 0;
                foreach(char ch in presyllabe)
                {
                    count++;
                    if (ch == '=')
                    {
                        syllabes.Add(mem);
                        mem = "";
                        continue;
                    }
                    else
                    {
                        mem += ch;
                    }

                    if (presyllabe.Count() == count)
                    {
                        syllabes.Add(mem);
                        mem = "";
                        continue;
                    }
                }

                foreach(string syllabe in syllabes)
                {
                    st_pseudosyllabes.Add(syllabe);
                }
            }         
        }
    }
    public class Loader
    {
        public Loader()
        {

        }

        private byte[] _dictionary;
        public byte[] Dictionary
        {
            get
            {
                if (_dictionary != null)
                {
                    return _dictionary;
                }
                else
                {
                    throw new Exception("First load dictionary!");
                }
            }
        }

        private char[] _nonvovelcharset;
        public char[] NonVovelCharSet
        {
            get
            {
                if (_nonvovelcharset != null)
                {
                    return _nonvovelcharset;
                }
                else
                {
                    throw new Exception("First load charset!");
                }
            }
        }

        private char[] _vovelcharset;
        public char[] VovelCharSet
        {
            get
            {
                if (_vovelcharset != null)
                {
                    return _vovelcharset;
                }
                else
                {
                    throw new Exception("First load charset!");
                }
            }
        }

        public void LoadDict(string Location)
        {
            if(File.Exists(Location) == true)
            {
                if(Path.GetExtension(Location) == ".dic")
                {
                    try
                    {
                        _dictionary = File.ReadAllBytes(Location);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Provided file is corrupted.", ex);
                    }
                }
                else
                {
                    throw new Exception("Provided file is corrupted. Dictionary must be in '.dic' format.");
                }
            }
            else
            {
                throw new Exception("Provided path don't exist.");
            }
        }
        public void LoadCharSet(string Location)
        {
            if (File.Exists(Location) == true)
            {
                if (Path.GetExtension(Location) == ".keyset")
                {
                    FileInfo f_info = new FileInfo(Location);
                    int f_size = (int)f_info.Length;

                    if (f_size <= 2097152)
                    {
                        try
                        {
                            string[] file = File.ReadAllLines(Location, Encoding.UTF8);
                            if (file[0] == "KEYSET")
                            {
                                _vovelcharset = file[1].ToCharArray();
                                _nonvovelcharset = file[2].ToCharArray();
                            }
                            else
                            {
                                throw new Exception("Provided file is not valid charset.");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Provided file is corrupted.", ex);
                        }
                    }
                    else
                    {
                        throw new Exception("Provided file is too big.");
                    }
                }
                else
                {
                    throw new Exception("Provided file is not '.keyset' format.");
                }
            }
            else
            {
                throw new Exception("Provided path don't exist.");
            }
        }
    }

}
