using Backups.StorageAlgorithm;
using Backups.Storages;

namespace Backups.Entities;

public class RestorePoint
{
    private List<IStorage> _storages;
    public RestorePoint(List<IStorage> storages)
    {
        _storages = storages;
        CreationDate = DateTime.Now;
        Path = GetPathFromFullPath(storages.First().PathToStorage);
    }

    public DateTime CreationDate { get; internal set; }
    public string Path { get; internal set; }
    private string GetPathFromFullPath(string fullPath)
    {
        string[] path = fullPath.Split("\\");
        string name = path[path.Length - 1];
        string pathToSave = string.Empty;
        foreach (var componentName in path)
        {
            if (componentName == name)
            {
                pathToSave = pathToSave.Substring(0, pathToSave.Length - 1);
                break;
            }

            pathToSave += componentName + "\\";
        }

        return pathToSave;
    }
}