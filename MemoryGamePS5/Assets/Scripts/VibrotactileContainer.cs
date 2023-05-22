using System;
using System.Collections;

[Serializable]
public class VibrotactileContainer
{

    // Array with status of vibrotactile feedback for each level
    //public BitArray randomVibrotactileLevels = new BitArray(16, false);
    public bool[] randomVibrotactileLevels = new bool[16];
    public VibrotactileContainer(bool[] randomVibrotactileLevels)
    {
        this.randomVibrotactileLevels = randomVibrotactileLevels;
    }
}