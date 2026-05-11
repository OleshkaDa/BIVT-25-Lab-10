using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab9.Green;

namespace Lab10.Green
{
    public class GreenJsonFileManager : GreenFileManager
    {
        public GreenJsonFileManager(string name)
            : base(name)
        {
        }

        public GreenJsonFileManager(
            string name,
            string folderpath,
            string filename,
            string filesurname = "json")
            : base(name, folderpath, filename, filesurname)
        {
        }

        public override void EditFile(string text)
        {
            if (text == null || text == "")
                return;

            var filik = Deserialize<Lab9.Green.Green>();

            if (filik == null)
                return;

            filik.ChangeText(text);

            Serialize(filik);
        }

        public override void ChangeFileExtension(string chtuka)
        {
            if (chtuka != "json")
                return;

            ChangeFileFormat("json");
        }

        public override void Serialize<T>(T filik)
        {
            if (filik == null)
                return;

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            Dictionary<string, string> savingStructure =
                new Dictionary<string, string>();

            savingStructure["Type"] = filik.GetType().Name;
            savingStructure["Input"] = filik.Input;

            if (filik is Lab9.Green.Task3 task3)
            {
                savingStructure["Pattern"] = task3.Sequence;
            }

            string json = JsonSerializer.Serialize(savingStructure);

            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            string jsonText = File.ReadAllText(FullPath);

            Dictionary<string, string> json =
                JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText);

            if (json == null)
                return null;

            string typeName = json["Type"];
            string input = json["Input"];

            object filik = null;

            switch (typeName)
            {
                case "Task1":
                    filik = new Lab9.Green.Task1(input);
                    break;

                case "Task2":
                    filik = new Lab9.Green.Task2(input);
                    break;

                case "Task3":

                    string pattern = json["Pattern"];

                    filik = new Lab9.Green.Task3(input, pattern);
                    break;

                case "Task4":
                    filik = new Lab9.Green.Task4(input);
                    break;
            }

            if (filik != null)
            {
                ((Lab9.Green.Green)filik).Review();
            }

            return filik as T;
        }
    }
}