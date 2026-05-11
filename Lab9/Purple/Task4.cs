// namespace Lab9.Purple;

// public class Task4 : Purple
// {
//     private string _output;
//     private (string, char)[] _table;

//     public string Output => _output;
//     public Task4(string input, (string, char)[] table) : base(input)
//     {
//         _output = default;
//         _table = table;
//     }


//     public override string ToString()
//     {
//         return _output;
//     }

//     public override void Review()
//     {
//         if (_table == null || _table.Length == 0)
//         {
//             return;
//         }

//         var codes = _table.ToDictionary(item => item.Item2, item => item.Item1);
//         _output = string.Concat(_input.Select(ch => codes.TryGetValue(ch, out var pair) ? pair : ch.ToString()));
//     }
// }
