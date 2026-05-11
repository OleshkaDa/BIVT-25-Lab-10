using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab9.Green
{
    public class Task3 : Green
    {
        private string _sequence;
        private string[] _output;

        public string[] Output => _output;
        public string Sequence => _sequence;

        public Task3(string input, string sequence) : base(input)
        {
            _sequence = sequence;
            _output = new string[0];


        }

        private string[] Splituem()
        {
            char[] simbols = new char[]
            {
                ' ', '.', ',', '!', '?', ';', ':', '\n', '\r', '\t',
                '-', '—', '(', ')', '[', ']', '{', '}', '"', '\'', ' '
             };

            var words = Input
                    .Split(simbols)
                    .Where(w => w.Length>0)
                    .ToArray();
            return words;
        }

        //private string[] SplitSequenceToWords()
        //{
        //    return _sequence.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '\n', '\r' },
        //                           StringSplitOptions.RemoveEmptyEntries);
        //}

        //public string[] Resenie()
        //{
        //    string[] graznie_slova = new string[10000];
        //    int k = 0;
        //    string[] words = SplitTextToWords();
        //    string[] pattern = SplitSequenceToWords();

        //    for (int i = 0; i < words.Length; i++)
        //    {
        //        for (int j = 0; j < pattern.Length; j++)
        //        {
        //            if (words[i].IndexOf(pattern[j], StringComparison.OrdinalIgnoreCase) >= 0)
        //            {
        //                // проверка на дубликат
        //                bool est = false;
        //                for (int m = 0; m < k; m++)
        //                {
        //                    if (graznie_slova[m] == words[i])
        //                    {
        //                        est = true;
        //                        break;
        //                    }
        //                }
        //                if (est == false)
        //                {
        //                    graznie_slova[k] = words[i];
        //                    k++;
        //                }
        //                break;
        //            }
        //        }
        //    }

        //    //for (int i = 0; i < words.Length; i++)
        //    //{
        //    //    for (int j = 0; j < pattern.Length; j++)
        //    //    {
        //    //        if (words[i].Contains(pattern[j]))
        //    //        {
        //    //            graznie_slova[k] = words[i];
        //    //            k++;
        //    //        }
        //    //    }
        //    //}
        //    //string[] Slovechki_promezhutok = new string[words.Length];
        //    //k = 0;
        //    //for (int i = 0; i < Slovechki_promezhutok.Length; i++)
        //    //{
        //    //    if (Slovechki_promezhutok.Contains(words[i]))
        //    //    {
        //    //        continue;
        //    //    }
        //    //    else
        //    //    {
        //    //        Slovechki_promezhutok[k] = words[i];
        //    //        k++;
        //    //    }
        //    //}

        //    string[] Slovechki = new string[k];
        //    for (int i = 0; i < k; i++)
        //    {
        //        Slovechki[i] = graznie_slova[i];
        //    }

        //    return Slovechki;
        //}

        public string[] Resenie()
        {
            string[] graznie_slova = new string[10000];
            int k = 0;

            string[] words = Splituem();
            string lowerSequence = _sequence.ToLower();

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];              // оригинал
                string lowerWord = word.ToLower();   // для сравнения

                if (lowerWord.Contains(lowerSequence))
                {
                    bool est = false;

                    for (int m = 0; m < k; m++)
                    {
                        // сравниваем без учета регистра
                        if (string.Equals(graznie_slova[m], word, StringComparison.OrdinalIgnoreCase))
                        {
                            est = true;
                            break;
                        }
                    }

                    if (!est)
                    {
                        // добавляем ориг
                        graznie_slova[k++] = word;
                    }
                }
            }

            string[] result = new string[k];
            for (int i = 0; i < k; i++)
            {
                result[i] = graznie_slova[i];
            }

            return result;
        }

        public override void Review()
        {
            _output = Resenie();
        }

        public override string ToString()
        {
            return _output == null || _output.Length == 0
                ? ""
                : string.Join("\r\n", _output);
        }
    }

}

