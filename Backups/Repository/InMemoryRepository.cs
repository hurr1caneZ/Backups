using Backups.Composite;
using Backups.StorageAlgorithm;
using Backups.Storages;
using Directory = Backups.Composite.Directory;
using File = Backups.Composite.File;

namespace Backups.Repository;

public class InMemoryRepository : IRepository
{
    private Dictionary<string, Component> _components;

    public InMemoryRepository(string fullPath)
    {
        _components = new Dictionary<string, Component>();
        RootDirectory = new Directory(fullPath);
        _components.Add(fullPath, RootDirectory);
        RootPath = fullPath;
    }

    public Directory RootDirectory { get; internal set; }

    public string RootPath { get; init; }

    public void SaveStorage(IStorage storage)
    {
        Directory dir = new Directory(storage.PathToStorage);
        foreach (var obj in storage.StorageBackupObjectsPaths)
        {
            dir.AddComponent(_components[obj.PathToFile]);
        }

        _components[GetPathFromFullPath(storage.PathToStorage)].AddComponent(dir);
        _components.Add(storage.PathToStorage, storage);
    }

    public Directory GetDirectory(string fullPath)
         {
             return (Directory)_components[fullPath];
         }

    public void CreateBackupTaskFolder(string name)
    {
        Directory backDir = new Directory(RootDirectory.FullPath + "\\" + name);
        _components.Add(backDir.FullPath, backDir);
        RootDirectory.AddComponent(backDir);
    }

    public void CreateRestorePointFolder(string name, string restName)
    {
        Directory backDir = new Directory(RootDirectory.FullPath + "\\" + name + "\\" + restName);
        _components.Add(backDir.FullPath, backDir);
        _components[RootDirectory.FullPath + "\\" + name].AddComponent(backDir);
    }

    public Directory AddDirectory(string fullPath)
    {
        var dir = new Directory(fullPath);
        var dirToSave = _components[GetPathFromFullPath(fullPath)];
        dirToSave.AddComponent(dir);
        _components.Add(fullPath, dir);
        return dir;
    }

    public File AddFile(string fullPath)
    {
        var dirToSave = _components[GetPathFromFullPath(fullPath)];
        var file = new File(fullPath);
        dirToSave.AddComponent(file);
        _components.Add(fullPath, file);
        return file;
    }

    private File FindFile(string fullPath)
    {
        return (File)_components[fullPath];
    }

    private string GetNameFromFullPath(string fullPath)
    {
        string[] path = fullPath.Split("\\");
        return path[path.Length - 1];
    }

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