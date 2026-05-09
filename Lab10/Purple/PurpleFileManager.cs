using System.Reflection;
using Lab9.Purple;

namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name) { }

    public PurpleFileManager(string name, string folderpath, string filename, string fileextension = "txt")
        : base(name, folderpath, filename, fileextension) { }

    protected static T CreateTask(string kind, string input, (string, char)[] codes = null)
    {
        codes ??= Array.Empty<(string, char)>();

        return kind switch
        {
            nameof(Task1) => new Task1(input) as T,
            nameof(Task2) => new Task2(input) as T,
            nameof(Task3) => new Task3(input) as T,
            nameof(Task4) => new Task4(input, codes) as T,
            _ => null
        };
    }

    protected static (string, char)[] GetTask4Table(Lab9.Purple.Purple task)
    {
        if (task is not Task4 task4)
        {
            return Array.Empty<(string, char)>();
        }

        var field = typeof(Task4).GetField("_table", BindingFlags.NonPublic | BindingFlags.Instance);
        if (field?.GetValue(task4) is (string, char)[] table && table != null)
        {
            return table;
        }

        return Array.Empty<(string, char)>();
    }

    public override void EditFile(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || !File.Exists(FullPath))
            return;

        base.EditFile(text);
    }

    public override void ChangeFileExtension(string extension)
    {
        if (string.IsNullOrWhiteSpace(extension) || !File.Exists(FullPath))
            return;

        base.ChangeFileExtension(extension);
    }

    public abstract void Serialize(T obj);
    public abstract T Deserialize();
}
