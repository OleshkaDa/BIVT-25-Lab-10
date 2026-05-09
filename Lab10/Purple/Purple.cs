namespace Lab10.Purple;

public class Purple<T> where T : Lab9.Purple.Purple
{
    private T[] _tasks;
    private PurpleFileManager<T> _manager;

    public PurpleFileManager<T> Manager => _manager;
    public T[] Tasks => _tasks;

    private static T[] CopyTasks(T[] tasks)
    {
        if (tasks == null || tasks.Length == 0)
            return Array.Empty<T>();

        T[] copy = new T[tasks.Length];
        Array.Copy(tasks, copy, tasks.Length);
        return copy;
    }

    public Purple()
    {
        _tasks = Array.Empty<T>();
        _manager = null;
    }

    public Purple(T[] tasks)
    {
        _tasks = CopyTasks(tasks);
        _manager = null;
    }

    public Purple(PurpleFileManager<T> manager, T[] tasks = null)
    {
        _manager = manager;
        _tasks = CopyTasks(tasks);
    }

    public Purple(T[] tasks, PurpleFileManager<T> manager)
    {
        _manager = manager;
        _tasks = CopyTasks(tasks);
    }

    public void Add(T task)
    {
        if (task == null) 
            return;
        Array.Resize(ref _tasks, _tasks.Length + 1);
        _tasks[^1] = task;
    }

    public void Add(T[] tasks)
    {
        if (tasks == null) 
            return;
        foreach (var task in tasks)
        {
            Add(task);
        }
    }

    public void Remove(T task)
    {
        if (task == null) 
            return;

        int index = Array.FindIndex(_tasks, current => current != null && current.ToString() == task.ToString());
        if (index < 0)
            return;

        T[] nextTasks = new T[_tasks.Length - 1];
        if (index > 0)
            Array.Copy(_tasks, 0, nextTasks, 0, index);
        if (index < _tasks.Length - 1)
            Array.Copy(_tasks, index + 1, nextTasks, index, _tasks.Length - index - 1);

        _tasks = nextTasks;
    }

    public void Clear()
    {
        _tasks = Array.Empty<T>();
        if (_manager != null && !string.IsNullOrEmpty(_manager.FolderPath) && Directory.Exists(_manager.FolderPath))
        {
            Directory.Delete(_manager.FolderPath, true);
        }
    }

    public void SaveTasks()
    {
        if (_manager == null)
            return;
        for (int i = 0; i < _tasks.Length; i++)
        {
            if (_tasks[i] == null)
                continue;
            _manager.ChangeFileName($"task{i}");
            _manager.Serialize(_tasks[i]);
        }
    }

    public void LoadTasks()
    {
        if (_manager == null)
            return;

        T[] loadedTasks = new T[_tasks.Length];
        for (int i = 0; i < loadedTasks.Length; i++)
        {
            _manager.ChangeFileName($"task{i}");
            T loaded = _manager.Deserialize();
            if (loaded != null)
            {
                loadedTasks[i] = loaded;
            }
        }

        _tasks = loadedTasks;
    }

    public void ChangeManager(PurpleFileManager<T> manager)
    {
        if (manager == null)
            return;

        string parentFolder = _manager != null && !string.IsNullOrEmpty(_manager.FolderPath)
            ? _manager.FolderPath
            : Directory.GetCurrentDirectory();
        string folder = Path.Combine(parentFolder, manager.Name);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        manager.SelectFolder(folder);
        _manager = manager;
    }
}
