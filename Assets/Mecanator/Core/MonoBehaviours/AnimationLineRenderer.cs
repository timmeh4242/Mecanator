using UnityEngine;

[ExecuteAlways]
public class AnimationLineRenderer : MonoBehaviour
{
    public LineRenderer LineRenderer;
    public float Alpha;

    const string TintColorName = "_Color";

    void Update()
    {
        var tintColor = LineRenderer.sharedMaterial.GetColor(TintColorName);
        tintColor.a = Alpha;
        LineRenderer.startColor = tintColor;
        LineRenderer.endColor = tintColor;
    }
}
