using Backups.Entities;
using Backups.Repository;
using Backups.StorageAlgorithm;
using Backups.Storages;

namespace Backups.Service;

public class BackupTask
{
    private Backup _backup;
    private List<BackupObject> _backupObjects;
    private IRepository _repository;
    private IAlgorithm _algorithm;

    public BackupTask(string name, IRepository repository, IAlgorithm algorithm)
    {
        Name = name;
        _backup = new Backup();
        _backupObjects = new List<BackupObject>();
        _repository = repository;
        _algorithm = algorithm;
        CreateBackupTaskFolder(repository);
    }

    public string Name { get; internal set; }
    public Backup Backup { get => _backup; }

    public RestorePoint CreateBackup()
    {
        string pathToSave = GetPathToSaveRestorePoint();
        List<IStorage> storages = _algorithm.MakeAndSaveStorages(pathToSave, _backupObjects);
        _repository.CreateRestorePointFolder(Name, GetNameFromFullPath(pathToSave));
        storages.ForEach(x => _repository.SaveStorage(x));
        RestorePoint rp = new RestorePoint(storages);
        _backup.AddRestorePoint(rp);
        return rp;
    }

    public BackupObject AddBackupObjectToTracking(BackupObject backupObject)
    {
        if (backupObject is null)
            throw new ArgumentException("backupObject is null");
        _backupObjects.Add(backupObject);
        return backupObject;
    }

    public BackupObject RemoveBackupObject(BackupObject backupObject)
    {
        if (backupObject is null)
            throw new ArgumentException("backupObject is null");
        _backupObjects.Remove(backupObject);
        return backupObject;
    }

    private void AddRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint is null)
            throw new ArgumentException("restorePoint is null");
        _backup.AddRestorePoint(restorePoint);
    }

    private void CreateBackupTaskFolder(IRepository repository)
    {
        repository.CreateBackupTaskFolder(Name);
    }

    private string GetPathToSaveRestorePoint()
    {
        return _repository.RootPath + $"\\{Name}\\{DateTime.Now.ToString("h-mm-ss")}";
    }

    private string GetNameFromFullPath(string fullPath)
    {
        string[] path = fullPath.Split("\\");
        return path[path.Length - 1];
    }
}