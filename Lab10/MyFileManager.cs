using System;
using System.IO;
using System.Linq;

namespace Lab10
{
    public abstract class MyFileManager : IFileManager, IFileLifeController
    {
        private string _name;
        private string _folderPath;
        private string _fileName;
        private string _fileExtension;

        public string Name => _name;

        public string FolderPath
        {
            get => _folderPath;
            private set => _folderPath = value;
        }

        public string FileName
        {
            get => _fileName;
            private set => _fileName = value;
        }

        public string FileExtension
        {
            get => _fileExtension;
            private set => _fileExtension = value;
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(_folderPath, _fileName + "." + _fileExtension);
            }
        }

        public MyFileManager(string name) 
        { 
            _name = name; 
            _folderPath = string.Empty; 
            _fileName = string.Empty; 
            _fileExtension = "txt"; 
        }

        public MyFileManager(string name, string folderPath, string fileName, string fileExtension = "txt")
        {
            _name = name;
            _folderPath = folderPath;
            _fileName = fileName;
            _fileExtension = fileExtension;
        }

        public virtual void SelectFolder(string folderPath)
        {
            if (folderPath != null)
            {
                _folderPath = folderPath;
            }
        }

        public virtual void ChangeFileName(string fileName)
        {
            if (fileName != null)
            {
                _fileName = fileName;
            }
        }

        public virtual void ChangeFileFormat(string fileExtension)
        {
            if (fileExtension == null)
                return;

            _fileExtension = fileExtension;

            CreateFile();
        }

        public virtual void CreateFile()
        {
            if (FullPath == string.Empty)
                return;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(FullPath))
            {
                File.Create(FullPath).Close();
            }
        }

        public virtual void DeleteFile()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public virtual void EditFile(string content)
        {
            File.WriteAllText(FullPath, content);
        }

        public virtual void ChangeFileExtension(string newExtension)
        {
            if (newExtension == null)
                return;

            if (!File.Exists(FullPath))
            {
                _fileExtension = newExtension;
                return;
            }

            string oldPath = FullPath;

            string content = File.ReadAllText(oldPath);

            _fileExtension = newExtension;

            string newPath = FullPath;

            File.WriteAllText(newPath, content);

            File.Delete(oldPath);
        }
    }
}