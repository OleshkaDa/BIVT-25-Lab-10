using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name) { }

    public PurpleTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        : base(name, folderPath, fileName, fileExtension) { }

    public override void EditFile(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || !File.Exists(FullPath))
            return;

        T obj = Deserialize();
        if (obj == null)
            return;

        obj.ChangeText(text);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension) || extension != "txt")
            return;

        ChangeFileFormat("txt");
    }

    public override void Serialize(T obj)
    {
        if (obj == null)
            return;

        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        try
        {
            using (StreamWriter sw = new StreamWriter(FullPath))
            {
                sw.WriteLine($"Type:{obj.GetType().Name}");
                sw.WriteLine($"Input:{obj.Input}");

                var table = GetTask4Table(obj);
                if (table.Length > 0)
                {
                    sw.WriteLine("Codes:");
                    foreach (var code in table)
                    {
                        sw.WriteLine($"{code.Item1}|{code.Item2}");
                    }
                }
            }
        }
        catch
        {
        }
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath))
            return null;

        string txt_object_type = null;
        string txt_object_input = null;
        bool need_codes = false;
        var codes = new List<(string, char)>();

        try
        {
            using (var sr = new StreamReader(FullPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.StartsWith("Type:"))
                    {
                        txt_object_type = line.Substring("Type:".Length).Trim();
                        need_codes = false;
                    }
                    else if (line.StartsWith("Input:"))
                    {
                        txt_object_input = line.Substring("Input:".Length).Trim();
                        need_codes = false;
                    }
                    else if (line.StartsWith("Codes:"))
                    {
                        need_codes = true;
                    }
                    else if (need_codes && line.Contains("|"))
                    {
                        string[] pair = line.Split('|');
                        if (pair.Length == 2 && pair[1].Length > 0)
                        {
                            codes.Add((pair[0], pair[1][0]));
                        }
                    }
                }
            }
        }
        catch
        {
            return null;
        }

        if (string.IsNullOrEmpty(txt_object_type) || string.IsNullOrEmpty(txt_object_input))
            return null;

        try
        {
            var obj = CreateTask(txt_object_type, txt_object_input, codes.ToArray());
            if (obj != null)
            {
                obj.Review();
                return (T)obj;
            }
        }
        catch
        {
            return null;
        }

        return null;
    }
}
