using Backups.Entities;
using Backups.Repository;
using Backups.Service;
using Backups.StorageAlgorithm;

namespace BackupConsole;

public class Program
{
    public static void Main(string[] args)
    {
        var desktopRep = new FileSystemRepository(@"C:\Users\sdc_a\OneDrive\Рабочий стол\БЭКИТЕСТЫ");
        var single = new SingleStorageAlgorithm();
        var split = new SplitStorageAlgorithm();
        var task = new BackupTask("название бэкаптаски", desktopRep, split);
        BackupObject b1 = new BackupObject(@"C:\Users\sdc_a\OneDrive\Рабочий стол\БЭКИТЕСТЫ\\название бэкап объекта1");
        BackupObject b2 = new BackupObject(@"C:\Users\sdc_a\OneDrive\Рабочий стол\БЭКИТЕСТЫ\\название бэкап объекта2");
        BackupObject b3 = new BackupObject(@"C:\Users\sdc_a\OneDrive\Рабочий стол\БЭКИТЕСТЫ\\название бэкап объекта3");

        task.AddBackupObjectToTracking(b1);
        task.AddBackupObjectToTracking(b2);
        task.AddBackupObjectToTracking(b3);
        task.CreateBackup();
        Thread.Sleep(2000);
        task.CreateBackup();
        task.RemoveBackupObject(b3);
        Thread.Sleep(2000);
        task.CreateBackup();
    }
}