using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


namespace Lab9.Green
{
    public class Task1 : Green
    {
        private (char, double)[] _output;
        public (char, double)[] Output => _output;
        public Task1(string text) : base(text)
        {
            //_output = Array.Empty<(char, double)>();
        }

        private int count =0;

        public (int[], double) Count()
        {
            string text = Input.ToLower();
            char[] rus_letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            int[] count = new int[rus_letters.Length];
            double total_letters = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char cur_letter = text[i];
                if (char.IsLetter(cur_letter))
                {
                    total_letters++;
                    for (int j = 0; j < rus_letters.Length; j++)
                    {
                        if (cur_letter == rus_letters[j])
                        {
                            count[j]++;
                            break;
                        }
                    }
                }
            }
            return (count, total_letters);
        }

        private (char, double)[] Resenie()
        {
            string text = Input.ToLower();
            char[] rus_letters = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
            var (count_bukv, total_letters) = Count();
            //double total_letters = 0;

            //for (int i = 0; i < text.Length; i++)
            //{
            //    char cur_letter = text[i];
            //    if (char.IsLetter(cur_letter))
            //    {
            //        total_letters++;
            //        for (int j = 0; j < rus_letters.Length; j++)
            //        {
            //            if (cur_letter == rus_letters[j])
            //            {
            //                count_bukv[j]++;
            //                break;
            //            }
            //        }
            //    }
            //}
            //count_bukv = Count();

            int NotNull = 0;
            for (int i = 0; i < count_bukv.Length; i++)
            {
                if (count_bukv[i] > 0)
                {
                    NotNull++;
                }
            }
            int k = 0;
            (char, double)[] result = new (char, double)[NotNull];
            for (int i = 0; i < rus_letters.Length; i++)
            {
                if (count_bukv[i] > 0)
                {
                    double shtuka = count_bukv[i] / total_letters;
                    result[k] = (rus_letters[i], shtuka);
                    k++;
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

            return string.Join("\n", _output.Select(x =>
            {
                //string val = x.Item2.ToString("F4").Replace('.', ',');
                return $"{x.Item1}:{x.Item2.ToString("F4").Replace('.', ',')}";
            }));
        }
    }
}









