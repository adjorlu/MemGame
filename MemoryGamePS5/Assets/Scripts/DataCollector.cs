using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector
{
    public string name;
    public float clicks;
    public float level;

    public DataCollector(string name, float clicks, int level)
    {
        this.name = name;
        this.clicks = clicks;
        this.level = level;
    }

    public override string ToString()
    {
        return $"{name} in level {level} has been clicked {clicks} times.";
    }

}
