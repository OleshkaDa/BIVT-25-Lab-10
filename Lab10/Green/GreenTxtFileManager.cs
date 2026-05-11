

using Lab9.Green;

namespace Lab10.Green
{
    public class GreenTxtFileManager : GreenFileManager
    {
        public GreenTxtFileManager(string name)
            : base(name)
        {
        }

        public GreenTxtFileManager(
            string name,
            string folderPath,
            string fileName,
            string fileExtension = "txt")
            : base(name, folderPath, fileName, fileExtension)
        {
        }

        public override void EditFile(string text)
        {
            if (!File.Exists(FullPath))
                return;

            Lab9.Green.Green filik = Deserialize<Lab9.Green.Green>();

            if (filik == null)
                return;

            filik.ChangeText(text);
            filik.Review();

            Serialize(filik);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (extension != "txt")
                return;

            ChangeFileFormat("txt");
        }

        public override void Serialize<T>(T textik)
        {
            if (textik == null)
                return;

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }


            string text = "";

            text += "Type:" + textik.GetType().Name + "\n";
            text += "Input:" + textik.Input + "\n";

            if (textik is Lab9.Green.Task3 task3)
            {
                text += "Pattern:" + task3.Sequence + "\n";
            }

            File.WriteAllText(FullPath, text);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            string type = null;
            string input = null;
            string pattern = null;

            string[] lines = File.ReadAllLines(FullPath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Type:"))
                {
                    type = lines[i].Substring(5);
                }

                if (lines[i].StartsWith("Input:"))
                {
                    input = lines[i].Substring(6);
                }

                if (lines[i].StartsWith("Pattern:"))
                {
                    pattern = lines[i].Substring(8);
                }
            }

            if (type == null || input == null)
                return null;

            Lab9.Green.Green obj = null;

            switch (type)
            {
                case "Task1":
                    obj = new Lab9.Green.Task1(input);
                    break;

                case "Task2":
                    obj = new Lab9.Green.Task2(input);
                    break;

                case "Task3":
                    obj = new Lab9.Green.Task3(input, pattern);
                    break;

                case "Task4":
                    obj = new Lab9.Green.Task4(input);
                    break;
            }

            if (obj == null)
                return null;

            obj.Review();

            return (T)(object)obj;
        }
    }
}