using Backups.Entities;
using Backups.Repository;
using Backups.Storages;

namespace Backups.StorageAlgorithm;

public class SplitStorageAlgorithm : IAlgorithm
{
    public List<IStorage> MakeAndSaveStorages(string pathToSave, List<BackupObject> backupObjects)
    {
        List<IStorage> storages = new List<IStorage>();
        foreach (var obj in backupObjects)
        {
            IStorage newStorage = new IStorage(pathToSave + $"\\split_{obj.PathToFile.Split("\\").Last()}");
            newStorage.AddBackupObject(obj);
            storages.Add(newStorage);
        }

        return storages;
    }
}