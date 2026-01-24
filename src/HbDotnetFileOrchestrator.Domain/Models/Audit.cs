namespace HbDotnetFileOrchestrator.Domain.Models;

public record Audit
{
    private Dictionary<string, object> Properties { get; init; }

    public Audit() : this(new Dictionary<string, object>())
    {
    }
    
    public Audit(Dictionary<string, object> properties)
    {
        Properties = properties;
    }

    public Audit CreateScope() => new ()
    {
        Properties = new Dictionary<string, object>(Properties)
    };

    public Audit AuditAction(Action<Audit> action)
    {
        try
        {
            action(this);
        }
        catch (Exception e)
        {
            AddProperty("Error", e.Message);
        }
        return this;
    }
    
    public async Task<Audit> AuditActionAsync(Func<Audit, Task> action)
    {
        try
        {
            await action(this);
        }
        catch (Exception e)
        {
            AddProperty("Error", e.Message);
        }
        return this;
    }
    
    public Audit AddProperty(string key, object value)
    {
        Properties[key] = value;
        return this;
    }

    public object this[string key] => Properties[key];
    
    public Dictionary<string, object> GetProperties() => Properties;
} 