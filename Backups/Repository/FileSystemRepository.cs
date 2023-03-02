using Backups.StorageAlgorithm;
using Backups.Storages;
using Ionic.Zip;
using Ionic.Zlib;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private string _backupTaskName;

    public FileSystemRepository(string rootPath)
    {
        RootPath = rootPath;
        _backupTaskName = string.Empty;
    }

    public string RootPath { get; init; }

    public void SaveStorage(IStorage storage)
    {
        using (ZipFile zipFile = new ZipFile())
            {
                foreach (var backupObject in storage.StorageBackupObjectsPaths)
                {
                    if (Directory.Exists(backupObject.PathToFile))
                    {
                        zipFile.AddDirectory(backupObject.PathToFile);
                    }
                    else if (File.Exists(backupObject.PathToFile))
                    {
                        zipFile.AddFile(backupObject.PathToFile, string.Empty);
                    }
                }

                zipFile.Save(storage.PathToStorage + ".zip");
            }
    }

    public void CreateBackupTaskFolder(string name)
    {
        Directory.CreateDirectory(RootPath + $"\\{name}");
        _backupTaskName = name;
    }

    public void CreateRestorePointFolder(string name, string restName)
    {
        Directory.CreateDirectory(RootPath + $"\\{name}\\{restName}");
    }
}