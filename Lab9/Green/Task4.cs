using System;


namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _output = new string[100];
        public string[] Output => _output; 
        public Task4(string text) : base(text)
        {
            
        }

        protected string[] Resenie()
        {
            char[] simbols = new char[]
            {
        ' ', '.', ',', '!', '?', ';', ':', '\n', '\r', '\t',
        '-', '—', '(', ')', '[', ']', '{', '}', '"', '\''
            };

            string[] Familii = Input.Split(simbols, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < Familii.Length; i++)
            {
                for (int j = 0; j < Familii.Length - i-1; j++)
                {
                    string schas = Familii[j];
                    string potom = Familii[j + 1];

                    bool swap = false;
                    bool equal = true;

                    int Len_Min = Math.Min(schas.Length, potom.Length);

                    for (int k = 0; k < Len_Min; k++)
                    {
                        char schas_1 = char.ToLower(schas[k]);
                        char potom_1 = char.ToLower(potom[k]);

                        if (schas_1 > potom_1)
                        {
                            swap = true;
                            equal = false;
                            break;
                        }
                        else if (schas_1 < potom_1)
                        {
                            equal = false;
                            break;
                        }
                    }

                    // только если полностью совпали
                    if (equal && schas.Length > potom.Length)
                    {
                        swap = true;
                    }

                    if (swap)
                    {
                        string temp = Familii[j];
                        Familii[j] = Familii[j + 1];
                        Familii[j + 1] = temp;
                    }

                }
            }


            return Familii;
        }

        public override void Review()
        {
            _output = Resenie();
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0)
                return "";

            string result = _output[0];

            for (int i = 1; i < _output.Length; i++)
            {
                result += "\r\n" + _output[i];
            }

            return result;
        }
    }
}
