using Backups.StorageAlgorithm;
using Backups.Storages;

namespace Backups.Repository;

public interface IRepository
{
    public string RootPath { get; init; }
    void SaveStorage(IStorage storage);
    void CreateBackupTaskFolder(string name);
    void CreateRestorePointFolder(string name, string restName);
}