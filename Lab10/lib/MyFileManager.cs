namespace Lab10;

public abstract class MyFileManager : IFileManager, IFileLifeController
{
    private string _name;
    private string _folderPath;
    private string _fileName;
    private string _fileExtension;

    public string Name => _name;
    public string FolderPath => _folderPath;
    public string FileName => _fileName;
    public string FileExtension => _fileExtension;
    public string FullPath => string.IsNullOrEmpty(_folderPath) || string.IsNullOrEmpty(_fileName) 
        ? "" 
        : Path.Combine(_folderPath, _fileName) + "." + _fileExtension;

    public MyFileManager(string name)
    {
        _name = name ?? "";
        _folderPath = "";
        _fileName = "";
        _fileExtension = "";
    }

    public MyFileManager(string name, string folderpath, string filename, string fileextension = "txt")
    {
        _name = name ?? "";
        _folderPath = folderpath ?? "";
        _fileName = filename ?? "";
        _fileExtension = fileextension ?? "txt";
    }

    public void SelectFolder(string folder)
    {
        if (string.IsNullOrEmpty(folder) || string.IsNullOrWhiteSpace(folder)) 
            return;
        if (!Directory.Exists(folder)) 
            return;
        _folderPath = folder;
    }

    public void ChangeFileName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) 
            return;
        _fileName = name;
    }

    public void ChangeFileFormat(string format)
    {
        if (string.IsNullOrEmpty(format) || string.IsNullOrWhiteSpace(format)) 
            return;
        _fileExtension = format;
        if (!File.Exists(FullPath))
        {
            CreateFile();
        }
    }

    public void CreateFile()
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        if (!File.Exists(FullPath))
        {
            File.Create(FullPath).Close();
        }
    }

    public void DeleteFile()
    {
        if (File.Exists(FullPath))
        {
            File.Delete(FullPath);
        }
    }

    public virtual void EditFile(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) 
            return;
        if (!File.Exists(FullPath)) 
            return;
        File.WriteAllText(FullPath, text);
    }

    public virtual void ChangeFileExtension(string extension)
    {
        if (string.IsNullOrEmpty(extension) || string.IsNullOrWhiteSpace(extension)) 
            return;
        if (!(extension == "txt" || extension == "json" || extension == "xml")) 
            return;
        if (!File.Exists(FullPath)) 
            return;
        string content = File.ReadAllText(FullPath);
        DeleteFile();
        ChangeFileFormat(extension);
        EditFile(content);
    }
}