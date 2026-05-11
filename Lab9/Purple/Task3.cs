// namespace Lab9.Purple;

// public class Task3 : Purple
//     {
//         private string _output;
//         private (string, char)[] _codes;

//         public string Output => _output;
//         public (string, char)[] Codes => _codes;

//         public Task3(string text) : base(text)
//         {
//             _output = default;
//             _codes = default;
//         }

//         private string[] SplitText()
//         {
//             return _input.Split(' ', '.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', '1', '2', '3', '4', '5', '6', '7', '8', '9');
//         }

//         private (string, char)[] CreatePairStringChar(string[] bestStrings, int actualCount)
//         {
//             (string, char)[] bestPairs = new (string, char)[actualCount];
            
//             bool[] usedChars = new bool[127];
//             for (int i = 0; i < _input.Length; i++)
//             {
//                 int code = (int)_input[i];
//                 if (code >= 32 && code <= 126)
//                     usedChars[code] = true;
//             }
            
//             int codeIndex = 32;
//             for (int i = 0; i < actualCount; i++)
//             {
//                 while (codeIndex < 127 && usedChars[codeIndex])
//                     codeIndex++;
                
//                 if (codeIndex < 127)
//                 {
//                     bestPairs[i] = (bestStrings[i], (char)codeIndex);
//                     usedChars[codeIndex] = true;
//                     codeIndex++;
//                 }
//             }
            
//             return bestPairs;
//         }

//         private string[] FindTheBestPairs()
//         {
//             Dictionary<string, int> dict = new Dictionary<string, int>();
//             Dictionary<string, int> firstOccurrence = new Dictionary<string, int>();
//             string[] words = SplitText();
            
//             for (int i = 0; i < words.Length; i++)
//             {
//                 string word = words[i];
//                 if (word.Length < 2) continue;
                
//                 for (int j = 1; j < word.Length; j++)
//                 {
//                     string pair = word[j - 1].ToString() + word[j].ToString();
                    
//                     if (dict.ContainsKey(pair))
//                     {
//                         dict[pair]++;
//                     }
//                     else
//                     {
//                         dict.Add(pair, 1);
//                         firstOccurrence.Add(pair, GetFirstOccurrence(pair));
//                     }
//                 }
//             }

//             return dict
//                 .Select(kvp => (Pair: kvp.Key, Count: kvp.Value, FirstIndex: firstOccurrence[kvp.Key]))
//                 .OrderByDescending(entry => entry.Count)
//                 .ThenBy(entry => entry.FirstIndex)
//                 .Take(5)
//                 .Select(entry => entry.Pair)
//                 .ToArray();
//         }
        
//         private int GetFirstOccurrence(string pair)
//         {
//             for (int i = 0; i < _input.Length - 1; i++)
//             {
//                 if (_input[i].ToString() + _input[i + 1].ToString() == pair)
//                     return i;
//             }
//             return int.MaxValue;
//         }

//         public override void Review()
//         {
//             string[] bestStrings = FindTheBestPairs();

//             if (bestStrings.Length == 0)
//             {
//                 _output = _input;
//                 _codes = new (string, char)[0];
//                 return;
//             }
            
//             _codes = CreatePairStringChar(bestStrings, bestStrings.Length);
//             _output = _input;
            
//             for (int i = 0; i < _codes.Length; i++)
//             {
//                 var (pair, code) = _codes[i];
//                 _output = _output.Replace(pair, code.ToString());
//             }
//         }

//         public override string ToString()
//         {
//             return _output ?? "";
//         }
//     }
