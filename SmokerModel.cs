public enum Component
{
    Tobacco,
    Paper,
    Matches
}

public class Smoker
{
    public Component Component { get; set; }
    public string Name { get; set; }

    public Smoker(Component component, string name)
    {
        Component = component;
        Name = name;
    }
}
