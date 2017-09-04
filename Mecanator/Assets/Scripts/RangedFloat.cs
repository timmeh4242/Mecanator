using System;

[Serializable]
public struct RangedFloat
{
    public RangedFloat(float min, float max)
    {
        MinValue = min;
        MaxValue = max;
    }

    public float MinValue;
    public float MaxValue;
}