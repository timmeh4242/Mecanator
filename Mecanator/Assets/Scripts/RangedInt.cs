using System;

[Serializable]
public struct RangedInt
{
	public RangedInt(int min, int max)
	{
		MinValue = min;
		MaxValue = max;
	}

    public int MinValue;
    public int MaxValue;
}