namespace WebApplicationTestCaseEM.Database.Entity;

public class District
{
    public District(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException("District id must be positive number");

        ArgumentException.ThrowIfNullOrEmpty(name);

        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}