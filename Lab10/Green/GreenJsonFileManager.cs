//using System;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text.Json;
//using System.Collections.Generic;

//namespace Lab10.Green
//{
//    public class GreenJsonFileManager : GreenFileManager
//    {
//        public GreenJsonFileManager(string name) : base(name) { }

//        public GreenJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json") : base(name, folderPath, fileName, fileExtension)
//        {

//        }


//        public override void EditFile(string content)
//        {
//            var obj = Deserialize<Lab9.Green.Green>();

//            if (obj != null)
//            {
//                obj.ChangeText(content);
//                Serialize(obj);
//            }

//        }

//        public override void ChangeFileExtension(string newExtension)
//        {
//            ChangeFileFormat("json");
//        }

//        public override void Serialize<T>(T obj)
//        {
//            if (obj == null) return;

//            CreateFile();

//            var type = obj.GetType();

//            var data = new Dictionary<string, object>
//            {
//                ["Type"] = type.AssemblyQualifiedName// эта штука дает фулл инфу о типе, возвращает полное имя типа + информацию о сборке
//            };

//            foreach (var prop in type.GetProperties())
//            {
//                data[prop.Name] = prop.GetValue(obj);
//            }

//            File.WriteAllText(FullPath, JsonSerializer.Serialize(data));
//        }

//        public override T Deserialize<T>()
//        {
//            if (!File.Exists(FullPath))
//                return null;

//            var data = JsonSerializer.Deserialize             //превращает json-строку в объект
//                <Dictionary<string, JsonElement>>         //JSON сохраняется как словарь где ключ это строка а занчение это элемент json
//                (File.ReadAllText(FullPath));        //читает весь json как строку



//            Type type = Type.GetType(data["Type"].GetString());//берет знаечние TYPE как строку


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
//                    string s = k.ToLower();

//                    if (s == name || s.Contains(name) || name.Contains(s))
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

//                string value = data[key].GetString();

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


using System;
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
            string fileextension = "json")
            : base(name, folderpath, filename, fileextension)
        {
        }

        public override void EditFile(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            var obj = Deserialize<Lab9.Green.Green>();

            if (obj == null)
                return;

            obj.ChangeText(text);
            obj.Review();

            Serialize(obj);
        }
        public override void ChangeFileExtension(string extension)
        {
            if (extension != "json")
                return;

            ChangeFileFormat("json");
        }

        public override void Serialize<T>(T obj)
        {
            if (obj == null)
                return;

            var dto = new Dictionary<string, object>();

            dto["Type"] = obj.GetType().Name;
            dto["Input"] = obj.Input;

            if (obj is Lab9.Green.Task3 task3)
            {
                dto["pattern"] = task3.Sequence;
            }

            string json = JsonSerializer.Serialize(dto);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.WriteAllText(FullPath, json);
        }

        public override T Deserialize<T>()
        {
            if (!File.Exists(FullPath))
                return null;

            try
            {
                string jsonText = File.ReadAllText(FullPath);

                var json = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonText);

                if (json == null)
                    return null;

                string typeName = json["Type"].GetString();
                string input = json["Input"].GetString();

                object obj = null;

                switch (typeName)
                {
                    case "Task1":
                        obj = new Lab9.Green.Task1(input);
                        break;

                    case "Task2":
                        obj = new Lab9.Green.Task2(input);
                        break;

                    case "Task3":

                        string pattern = json["pattern"].GetString();

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

                return obj as T;
                //return (T)obj;
            }
            catch
            {
                return null;
            }
        }
    }
}