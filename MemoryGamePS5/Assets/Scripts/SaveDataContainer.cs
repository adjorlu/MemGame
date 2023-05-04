using System;

[Serializable]
public class SaveDataContainer
{

    // Class used for saving the progress level
    public int level;

    public SaveDataContainer(int level)
    {
        this.level = level;
    }
}