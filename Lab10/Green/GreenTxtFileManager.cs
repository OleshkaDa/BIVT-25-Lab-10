//using System;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Collections.Generic;

//namespace Lab10.Green
//{
//    public class GreenTxtFileManager : GreenFileManager
//    {
//        public GreenTxtFileManager(string name) : base(name) { }

//        public GreenTxtFileManager(string name, string folderPath, string fileName, string fileExtension = "txt") : base(name, folderPath, fileName, fileExtension)
//        {

//        }

//        public override void EditFile(string content)
//        {
//            var obj = Deserialize<Lab9.Green.Green>();

//            if (obj != null)
//            {
//                obj.ChangeText(content);
//            }

//            if (obj != null)
//                Serialize(obj);
//        }

//        public override void ChangeFileExtension(string newExtension)
//        {
//            ChangeFileFormat("txt");
//        }

//        public override void Serialize<T>(T obj)
//        {
//            CreateFile();

//            var lines = new List<string>();

//            lines.Add("Type=" + obj.GetType().AssemblyQualifiedName);

//            foreach (var prop in obj.GetType().GetProperties())
//            {
//                lines.Add(prop.Name + "=" + prop.GetValue(obj));
//            }

//            File.WriteAllLines(FullPath, lines);
//        }

//        public override T Deserialize<T>()
//        {
//            if (!File.Exists(FullPath))
//                return null;

//            var lines = File.ReadAllLines(FullPath);

//            var data = new Dictionary<string, string>();

//            foreach (var line in lines)
//            {
//                string[] parts = line.Split('=', 2);

//                if (parts.Length == 2)
//                {
//                    data[parts[0]] = parts[1];
//                }
//            }

//            Type type = Type.GetType(data["Type"]);

//            var constructors = type.GetConstructors();

//            var ctor = constructors[0];

//            for (int i = 1; i < constructors.Length; i++)
//            {
//                if (constructors[i].GetParameters().Length > ctor.GetParameters().Length)
//                {
//                    ctor = constructors[i];
//                }
//            }

//            var parameters = ctor.GetParameters();

//            object[] args = new object[parameters.Length];

//            for (int i = 0; i < parameters.Length; i++)
//            {
//                var p = parameters[i];

//                string name = p.Name.ToLower();

//                string key = null;

//                foreach (var k in data.Keys)
//                {
//                    string s = k.ToLower().Replace("_", "");

//                    if (s == name ||
//                        s.Contains(name) ||
//                        name.Contains(s))
//                    {
//                        key = k;
//                        break;
//                    }
//                }

//                if (key == null && (name == "text" || name == "str"))
//                {
//                    foreach (var k in data.Keys)
//                    {
//                        if (k.ToLower().Contains("input"))
//                        {
//                            key = k;
//                            break;
//                        }
//                    }
//                }

//                if (key == null)
//                {
//                    args[i] = null;
//                    continue;
//                }

//                string value = data[key];

//                args[i] = Convert.ChangeType(value, p.ParameterType);
//            }

//            T obj = (T)ctor.Invoke(args);

//            if (obj != null)
//            {
//                obj.Review();
//            }

//            return obj;
//        }

//    }
//}


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
            if (string.IsNullOrWhiteSpace(text) || !File.Exists(FullPath))
                return;

            Lab9.Green.Green obj = Deserialize<Lab9.Green.Green>();

            if (obj == null)
                return;

            obj.ChangeText(text);
            obj.Review();

            Serialize(obj);
        }

        public override void ChangeFileExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension) || extension != "txt")
                return;

            ChangeFileFormat("txt");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null)
                return;

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            try
            {
                using (StreamWriter sw = new StreamWriter(FullPath))
                {
                    sw.WriteLine($"Type:{obj.GetType().Name}");
                    sw.WriteLine($"Input:{obj.Input}");

                    if (obj is Lab9.Green.Task3 task3)
                    {
                        sw.WriteLine($"Pattern:{task3.Sequence}");
                    }
                }
            }
            catch
            {
            }
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            string type = null;
            string input = null;
            string pattern = null;

            try
            {
                using (StreamReader sr = new StreamReader(FullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        if (line.StartsWith("Type:"))
                        {
                            type = line.Substring("Type:".Length).Trim();
                        }
                        else if (line.StartsWith("Input:"))
                        {
                            input = line.Substring("Input:".Length).Trim();
                        }
                        else if (line.StartsWith("Pattern:"))
                        {
                            pattern = line.Substring("Pattern:".Length).Trim();
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(type) ||
                string.IsNullOrWhiteSpace(input))
                return null;

            object obj = null;

            try
            {
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

                if (obj != null)
                {
                    ((Lab9.Green.Green)obj).Review();
                }

                return (T)obj;
            }
            catch
            {
                return null;
            }
        }
    }
}