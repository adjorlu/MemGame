using System;

[Serializable]
public class DataCollector
{

    // Class used for saving interaction data of each level
    public string name;
    public float clicks;
    public float level;
    public string melodyName;
    public bool instr;
    public bool mel;
    public bool vibrMute;
    public string creationTime;

    // Constructor
    public DataCollector(string name, float clicks, int level, string melodyName, bool similarInstruments, bool sameMelody, bool vibrotactileFeedback, string creationTime)
    {
        this.name = name;
        this.clicks = clicks;
        this.level = level;
        this.melodyName = melodyName;
        this.instr = similarInstruments;
        this.mel = sameMelody;
        this.vibrMute = vibrotactileFeedback;
        this.creationTime = creationTime;
        this.creationTime = creationTime;
    }

}
