using Backups.Composite;
using Backups.Entities;
using Directory = Backups.Composite.Directory;

namespace Backups.Storages;

public class IStorage : Directory
{
    public IStorage(string fullpath)
        : base(fullpath)
    {
        PathToStorage = fullpath;
        StorageBackupObjectsPaths = new List<BackupObject>();
    }

    public List<BackupObject> StorageBackupObjectsPaths { get; init; }

    public string PathToStorage { get; init; }

    public void AddBackupObject(BackupObject backObj)
    {
        StorageBackupObjectsPaths.Add(backObj);
    }
}