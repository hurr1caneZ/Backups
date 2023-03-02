namespace Backups.Exceptions;

public class FileDontHaveRights : Exception
{
    public FileDontHaveRights()
        : base("FILE DONT HAVE RIGHTS")
    { }
}