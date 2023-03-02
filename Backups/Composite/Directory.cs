namespace Backups.Composite;

public class Directory : Component
{
    private List<Component> _components;

    public Directory(string fullPath)
        : base(fullPath)
    {
        _components = new List<Component>();
    }

    public IReadOnlyCollection<Component> Components { get => _components; }

    public override void AddComponent(Component component)
    {
        if (component is null)
            throw new ArgumentException("component is null");
        _components.Add(component);
    }

    public override void RemoveComponent(Component component)
    {
        if (component is null)
            throw new ArgumentException("component is null");
        _components.Remove(component);
    }
}