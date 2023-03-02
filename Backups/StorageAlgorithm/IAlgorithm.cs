using Backups.Entities;
using Backups.Repository;
using Backups.Storages;

namespace Backups.StorageAlgorithm;

public interface IAlgorithm
{
     List<IStorage> MakeAndSaveStorages(string pathToSave, List<BackupObject> backupObjects);
}