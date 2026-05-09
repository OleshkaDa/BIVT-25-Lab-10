namespace Lab10;

public interface IFileLifeController
{
    void CreateFile();
    void DeleteFile();
    void EditFile(string text);
    void ChangeFileExtension(string extension);
}