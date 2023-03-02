using Backups.Exceptions;

namespace Backups.Composite;

public class File : Component
{
    public File(string fullPath)
        : base(fullPath)
    {
    }

    public override void AddComponent(Component component)
    {
        throw new FileDontHaveRights();
    }

    public override void RemoveComponent(Component component)
    {
        throw new FileDontHaveRights();
    }
}