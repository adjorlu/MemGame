using System;

[Serializable]
public class DataCollector
{
    public string name;
    public float clicks;
    public float level;
    public string melodyName;
    public bool instr;
    public bool mel;

    // Constructor
    public DataCollector(string name, float clicks, int level, string melodyName, bool similarInstruments, bool sameMelody)
    {
        this.name = name;
        this.clicks = clicks;
        this.level = level;
        this.melodyName = melodyName;
        this.instr = similarInstruments;
        this.mel = sameMelody;
    }

}
