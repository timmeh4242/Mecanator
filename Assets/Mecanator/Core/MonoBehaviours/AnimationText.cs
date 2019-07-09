using UnityEngine;
using TMPro;

public class AnimationText : MonoBehaviour
{
    public TextMeshPro Text;

    public void UpdateText(string value)
    {
        Text.text = value;
    }
}
