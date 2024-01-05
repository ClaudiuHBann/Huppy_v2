using System;
using System.IO;

using Shared.Utilities;

namespace Huppy.Services
{
public class StorageService : BaseService
{
    private string AppDataDirectory = "";
    private string AppDataHuppyDirectory = "";

    public StorageService()
    {
        FindOrCreateDirectories();
    }

    private void FindOrCreateDirectories()
    {
        // TODO: fix this ervice for browser: https://github.com/AvaloniaUI/Avalonia/discussions/14119
        AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        AppDataHuppyDirectory = Path.Combine(AppDataDirectory, "Huppy");
        if (!Directory.Exists(AppDataHuppyDirectory))
        {
            Directory.CreateDirectory(AppDataHuppyDirectory);
        }
    }

    public T? Load<T>(string key)
        where T : class
    {
        var path = Path.Combine(AppDataHuppyDirectory, key);
        if (!File.Exists(path))
        {
            return null;
        }

        var json = File.ReadAllText(path);
        return json.FromJSON<T>();
    }

    public void Save(string key, object obj)
    {
        var path = Path.Combine(AppDataHuppyDirectory, key);
        File.WriteAllText(path, obj.ToJSON());
    }
}
}
