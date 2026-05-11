using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;



namespace Lab9.Green
{
    public class Task2 : Green
    {
        private char[] _output;
        public char[] Output => _output;

        public Task2(string text) : base(text)
        {
        }

        public char[] Resenie()
        {
            //char[] rus_alf = {
            //'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о',
            //'п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я'
            //};
            //char[] eng_alf = {
            //'a','b','c','d','e','f','g','h','i','j','k','l','m',
            //'n','o','p','q','r','s','t','u','v','w','x','y','z'
            //};

            char[] separators =  {
                '.', '!', '?', ',', ':', '\"', ';', '–',
                '(', ')', '[', ']', '{', '}', '/'
            };

            var parts = Input.Split(' ');
            double[] count_letters = new double[1000];
            char[] letters = new char[1000];
            foreach (var raw in parts)
            {
                string word = "";
                word = raw.Trim(separators);

                if (word.Length == 0)
                    continue;

                char first = word[0];

                if (!char.IsLetter(first))
                    continue;

                char lower = char.ToLower(first);
                int index = -1;
                for (int i = 0; i < count_letters.Length; i++)
                {
                    if (letters[i] == lower)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    count_letters[index]++; // увеличиваем частоту
                else
                {
                    for (int i = 0; i < count_letters.Length; i++)
                    {
                        if (letters[i] == '\0')
                        {
                            letters[i] = lower;
                            count_letters[i]++;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < letters.Length - 1; i++)
            {
                for (int j = 0; j < letters.Length - i - 1; j++)
                {
                    bool swap = false;

                    if (count_letters[j] < count_letters[j + 1])
                        swap = true;
                    else if (count_letters[j] == count_letters[j + 1] &&
                             letters[j] > letters[j + 1])
                        swap = true;

                    if (swap)
                    {
                        (letters[j], letters[j + 1]) = (letters[j + 1], letters[j]);
                        (count_letters[j], count_letters[j + 1]) = (count_letters[j + 1], count_letters[j]);
                    }
                }
            }
            int size = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] != '\0')
                    size++;
            }

            // новый массив нужного размера
            char[] result = new char[size];

            int k = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] != '\0')
                {
                    result[k++] = letters[i];
                }
            }

            return result;
        }

        public override void Review()
        {
            _output = Resenie();
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";

            string result = _output[0].ToString();

            for (int i = 1; i < _output.Length; i++)
            {
                result += ", " + _output[i];
            }

            return result;
        }
    }
}