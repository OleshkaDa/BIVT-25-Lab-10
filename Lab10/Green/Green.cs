using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab10.Green
{
    public class Green
    {
        private Lab9.Green.Green[] _tasks;
        private GreenFileManager _manager;

        public GreenFileManager Manager => _manager;


        public Lab9.Green.Green[] Tasks => _tasks;
        
        // конструкторы (разница тупо в параметрах)
        public Green(params Lab9.Green.Green[] tasks)
        {
            if (tasks != null)
            {
                _tasks = tasks.ToArray();
            }
            else
            {
                _tasks = Array.Empty<Lab9.Green.Green>();
            }
        }

        public Green(GreenFileManager manager, params Lab9.Green.Green[] tasks)
        {
            _manager = manager;

            if (tasks != null)
            {
                _tasks = tasks.ToArray();
            }
            else
            {
                _tasks = Array.Empty<Lab9.Green.Green>();
            }
        }

        public Green(Lab9.Green.Green[] tasks, GreenFileManager manager)
        {
            _manager = manager;

            if (tasks != null)
            {
                _tasks = tasks.ToArray();
            }
            else
            {
                _tasks = Array.Empty<Lab9.Green.Green>();
            }
        }
        




        public void Add(Lab9.Green.Green task)
        {
            if (task == null)
            {
                return;
            }

            Array.Resize(ref _tasks, _tasks.Length + 1);

            _tasks[_tasks.Length - 1] = task;
        }

        public void Add(Lab9.Green.Green[] tasks)
        {
            if (tasks == null)
            {
                return;
            }

            foreach (Lab9.Green.Green task in tasks)
            {
                Add(task);
            }
        }



        public void Remove(Lab9.Green.Green task)
        {
            if (task == null)
            {
                return;
            }

            _tasks = _tasks.Where(t => t != task).ToArray();
        }

        public void Clear()
        {
            _tasks = Array.Empty<Lab9.Green.Green>();

            if (_manager != null && Directory.Exists(_manager.FolderPath))
            {
                Directory.Delete(_manager.FolderPath, true);
            }
        }

        public void SaveTasks()
        {
            if (_manager == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task_{i}");

                _manager.Serialize(_tasks[i]);
            }
        }

        public void LoadTasks()
        {
            if (_manager == null)
            {
                return;
            }

            for (int i = 0; i < _tasks.Length; i++)
            {
                _manager.ChangeFileName($"task_{i}");

                _tasks[i] = _manager.Deserialize<Lab9.Green.Green>();
            }
        }

        public void ChangeManager(GreenFileManager manager)
        {
            if (manager == null)
            {
                return;
            }

            _manager = manager;

            string folder =
                Path.Combine(Directory.GetCurrentDirectory(), manager.Name);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            manager.SelectFolder(folder);
        }
    }
}
