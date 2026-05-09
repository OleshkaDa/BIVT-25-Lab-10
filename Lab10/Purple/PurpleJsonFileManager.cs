using Lab9.Purple;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name) { }

    public PurpleJsonFileManager(string name, string folderpath, string filename, string fileextension = "txt")
        : base(name, folderpath, filename, fileextension) { }

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
        if (string.IsNullOrWhiteSpace(extension) || extension != "json")
            return;

        ChangeFileFormat("json");
    }

    public override void Serialize(T obj)
    {
        if (obj == null)
            return;

        var dto = new Dictionary<string, object>
        {
            ["Type"] = obj.GetType().Name,
            ["Input"] = obj.Input
        };

        var table = GetTask4Table(obj);
        if (table.Length > 0)
        {
            dto["Items"] = table
                .Select(item => new Dictionary<string, object>
                {
                    ["Item1"] = item.Item1,
                    ["Item2"] = item.Item2.ToString()
                })
                .ToArray();
        }

        string convert = JsonConvert.SerializeObject(dto, Formatting.Indented);

        ChangeFileFormat("json");
        if (!File.Exists(FullPath))
        {
            CreateFile();
        }
        base.EditFile(convert);
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath) || FileExtension != "json")
            return null;

        try
        {
            var json = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(FullPath));
            if (json == null)
                return null;

            string typeName = json["Type"]?.ToString() ?? nameof(Task1);
            string input = json["Input"]?.ToString() ?? "";

            (string, char)[] table = Array.Empty<(string, char)>();
            if (json["Items"] is JArray items && items.Count > 0)
            {
                table = items
                    .Select(item => (
                        item["Item1"]?.ToString() ?? "",
                        string.IsNullOrEmpty(item["Item2"]?.ToString()) ? '\0' : item["Item2"]!.ToString()[0]))
                    .ToArray();
            }

            T obj = CreateTask(typeName, input, table);
            obj?.Review();
            return obj;
        }
        catch
        {
            return null;
        }
    }
}
