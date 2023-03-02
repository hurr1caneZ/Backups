using Backups.Entities;
using Backups.Repository;
using Backups.Storages;
using Ionic.Zip;

namespace Backups.StorageAlgorithm;

public class SingleStorageAlgorithm : IAlgorithm
{
    public List<IStorage> MakeAndSaveStorages(string pathToSave, List<BackupObject> backupObjects)
    {
        IStorage storage = new IStorage(pathToSave + "\\single");
        foreach (var obj in backupObjects)
        {
            storage.AddBackupObject(obj);
        }

        return new List<IStorage> { storage };
    }
}