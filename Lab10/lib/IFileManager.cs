namespace Lab10;


public interface IFileManager
{
    string FolderPath{get;}
    string FileName{get;}
    string FileExtension{get;}
    string FullPath{get;}

    void SelectFolder(string path_to_folder);
    void ChangeFileName(string new_file_name);
    void ChangeFileFormat(string new_file_format);
}