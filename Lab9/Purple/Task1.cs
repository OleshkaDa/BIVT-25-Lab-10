// namespace Lab9.Purple;

// public class Task1 : Purple
// {
//     private string _output;
//     public string Output => _output;

//     public Task1(string text) : base(text)
//     { 
//         _output = "";
        
//     }

//     public override void Review()
//     {
//         char[] chars = {'.', '!', '?', ',', ':', '\"', ';', '–', '(', ')', '[', ']', '{', '}', '/', ' '};
//         int n = _input.Length;
//         string word = "";
//         string revWord;
//         bool rev = true;


//         for (int i = 0; i < n; i++)
//         {
//             if (chars.Contains(_input[i]))
//             {
//                 if (char.IsDigit(_input[i - 1]) && i + 1 < n && char.IsDigit(_input[i + 1]) && _input[i] == ',')
//                 {
//                     word += _input[i];
//                 }
//                 else
//                 {
//                     if (rev == false)
//                     {
//                         _output += word + _input[i];
//                         word = "";
//                         rev = true;
//                     }
//                     else
//                     {
//                         revWord = new string(word.Reverse().ToArray());
//                         _output += revWord + _input[i];
//                         word = "";
//                     }
//                 }
//                 continue;

//             } 

//             if (char.IsDigit(_input[i]))
//             {
//                 rev = false;
//                 word += _input[i];
//                 continue;
//             }
            
//             word += _input[i];
            
//         }
//     }



   
    
    
//     public override string ToString()
//     {
//         return _output;
//     }
// }
