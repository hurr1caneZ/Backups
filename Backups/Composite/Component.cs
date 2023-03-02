namespace Backups.Composite;

public abstract class Component
{
    public Component(string fullPath)
    {
        FullPath = fullPath;
    }

    public string FullPath { get; internal set; }

    public string GetName()
    {
        string[] path = FullPath.Split();
        return path[path.Length - 1];
    }

    public abstract void AddComponent(Component component);
    public abstract void RemoveComponent(Component component);
}