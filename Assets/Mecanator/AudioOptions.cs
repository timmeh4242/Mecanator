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