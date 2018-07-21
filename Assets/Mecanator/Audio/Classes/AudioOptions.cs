using System.Collections.Generic;

[System.Serializable]
public enum PlayType
{
	None,
	FadeIn,
	Play,
	Stop,
	FadeOut,
}

[System.Serializable]
public struct AudioOptions
{
	public PlayType PlayType;
	public float Delay;
	public bool Is3D;
    public bool PlayDuringFade;
}

public static class AudioOptionsData
{
    private static Dictionary<PlayType, string> StringTable = new Dictionary<PlayType, string>
    {
        { PlayType.None, "None" },
        { PlayType.FadeIn, "FadeIn" },
        { PlayType.Play, "Play" },
        { PlayType.Stop, "Stop" },
        { PlayType.FadeOut, "FadeOut" },
    };

    public static string AsString(this PlayType command)
    {
        return StringTable[command];
    }
}