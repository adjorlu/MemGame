using System;

[Serializable]
public class DataCollector
{
    public string name;
    public float clicks;
    public float level;

    // Constructor
    public DataCollector(string name, float clicks, int level)
    {
        this.name = name;
        this.clicks = clicks;
        this.level = level;
    }

}
