namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string pathToFile)
    {
        PathToFile = pathToFile;
    }

    public string PathToFile { get; internal set; }
}