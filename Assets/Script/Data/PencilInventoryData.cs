using System;
using UnityEngine;

[Serializable]
public class PencilInventoryData
{
    public PencilType pencilType;
    
    public int unSharpenedCount;
    public int sharpeningCount;
    public int sharpenedCount;
    public int graphiteCount;

    public PencilData pencilData;
}