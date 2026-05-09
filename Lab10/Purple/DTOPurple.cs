using System.Xml.Serialization;

namespace Lab10.Purple;

[XmlRoot("DTOPurple")]
public class DTOPurple
{
    [XmlElement("TypeName")]
    public string TypeName { get; set; }

    [XmlElement("Input")]
    public string Input { get; set; }

    [XmlArray("Items")]
    [XmlArrayItem("Item")]
    public DTOItem[] Items { get; set; }

    public DTOPurple() { }

    public DTOPurple(string typeName, string input, DTOItem[] items = null)
    {
        TypeName = typeName;
        Input = input;
        Items = items ?? Array.Empty<DTOItem>();
    }
}

[XmlType("Item")]
public class DTOItem
{
    [XmlElement("Key")]
    public string Key { get; set; }

    [XmlElement("Value")]
    public char Value { get; set; }

    public DTOItem() { }

    public DTOItem(string key, char value)
    {
        Key = key;
        Value = value;
    }
}
