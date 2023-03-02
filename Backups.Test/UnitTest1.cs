using System.ComponentModel;
using Backups.Composite;
using Backups.Entities;
using Backups.Repository;
using Backups.Service;
using Backups.StorageAlgorithm;
using Xunit;
using File = Backups.Composite.File;

namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void Check()
    {
        InMemoryRepository repository = new InMemoryRepository("root");
        SplitStorageAlgorithm splitalgo = new SplitStorageAlgorithm();
        BackupObject beck1 = new BackupObject(repository.RootPath + "\\�������");
        BackupObject beck2 = new BackupObject(repository.RootPath + "\\�������");
        repository.AddFile(repository.RootPath + "\\�������");
        repository.AddFile(repository.RootPath + "\\�������");
        BackupTask taska = new BackupTask("tasochka", repository, splitalgo);
        taska.AddBackupObjectToTracking(beck1);
        taska.AddBackupObjectToTracking(beck2);
        taska.CreateBackup();
        Assert.Equal(2, repository.GetDirectory(taska.Backup.GetRestorePoints().First().Path).Components.Count);
        Thread.Sleep(3000);
        taska.CreateBackup();
        Assert.Equal(2, repository.GetDirectory(taska.Backup.GetRestorePoints().Last().Path).Components.Count);
    }
    
    //ss
}