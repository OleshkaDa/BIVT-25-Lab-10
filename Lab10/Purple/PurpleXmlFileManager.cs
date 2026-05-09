using System.Xml.Serialization;
using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T> : PurpleFileManager<T>
    where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name) { }

    public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "xml")
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
        if (string.IsNullOrWhiteSpace(extension) || extension != "xml")
            return;

        if (!File.Exists(FullPath))
        {
            ChangeFileFormat("xml");
            return;
        }

        T obj = Deserialize();
        ChangeFileFormat("xml");
        if (obj != null)
        {
            Serialize(obj);
        }
    }

    public override void Serialize(T obj)
    {
        if (obj == null)
            return;

        if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

        try
        {
            var table = GetTask4Table(obj);
            DTOItem[] items = table.Length == 0
                ? Array.Empty<DTOItem>()
                : table.Select(item => new DTOItem(item.Item1, item.Item2)).ToArray();

            DTOPurple dto = new DTOPurple(obj.GetType().Name, obj.Input, items);
            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

            using (StreamWriter sw = new StreamWriter(FullPath))
            {
                serializer.Serialize(sw, dto);
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

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));
            DTOPurple dto = null;

            using (StreamReader sr = new StreamReader(FullPath))
            {
                object deserialized = serializer.Deserialize(sr);
                dto = deserialized as DTOPurple;
            }

            if (dto == null || string.IsNullOrEmpty(dto.TypeName) || string.IsNullOrEmpty(dto.Input))
                return null;

            var codes = dto.Items == null || dto.Items.Length == 0
                ? Array.Empty<(string, char)>()
                : dto.Items.Select(item => (item.Key, item.Value)).ToArray();

            var obj = CreateTask(dto.TypeName, dto.Input, codes);
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
