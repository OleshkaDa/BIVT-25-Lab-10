using Lab9.Green;

namespace Lab10.Green
{
    public abstract class GreenFileManager : MyFileManager, ISerializer
    {
        protected GreenFileManager(string name) : base(name)
        {
        }

        protected GreenFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
        {
        }

        public override void EditFile(string content)
        {
            base.EditFile(content);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            base.ChangeFileExtension(newExtension);
        }

        public abstract T Deserialize<T>() where T : Lab9.Green.Green;

        public abstract void Serialize<T>(T obj) where T : Lab9.Green.Green;
    }
}