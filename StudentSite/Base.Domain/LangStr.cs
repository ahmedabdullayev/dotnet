namespace Base.Domain;

public class LangStr : Dictionary<string, string>
{
    private const string DefaultCulture = "en-GB";
    public LangStr()
    {
    }
    public LangStr(string value) : this(value, Thread.CurrentThread.CurrentUICulture.Name)
    {
        
    }
    public LangStr(string str, string culture)
    {
        this[culture] = str;
    }

    public string? Translate(string? culture = null)
    {
        if (Count == 0) return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        if (ContainsKey(culture))
        {
            return this[culture];
        }
        // if en-GB -> en
        var neutralCulture = culture.Split("-")[0];
        if (ContainsKey(neutralCulture))
        {
            return this[neutralCulture];
        }
        if (ContainsKey(DefaultCulture))
        {
            return this[DefaultCulture];
        }

        return this[DefaultCulture]; // or return null
    }
    public void SetTranslation(string value)
    {
        this[Thread.CurrentThread.CurrentUICulture.Name] = value;
    }
    public override string ToString()
    {
        return Translate() ?? "???";
    }

    // string xxx = new LangStr("zzz")
    public static implicit operator string(LangStr? langStr) => langStr?.ToString() ?? "null";
    // LangStr lstr = "xxx"; // internally it will be lStr= new LangStr("xxx")
    public static implicit operator LangStr(string value) => new LangStr(value);
}