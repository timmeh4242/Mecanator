using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedInt), true)]
public class RangedIntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty minProp = property.FindPropertyRelative("MinValue");
        SerializedProperty maxProp = property.FindPropertyRelative("MaxValue");

		float minValue = minProp.intValue;
		float maxValue = maxProp.intValue;

        float rangeMin = 0;
        float rangeMax = 100;

        var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
        if (ranges.Length > 0)
        {
            rangeMin = ranges[0].Min;
            rangeMax = ranges[0].Max;
        }

        const float rangeBoundsLabelWidth = 40f;

        EditorGUI.BeginChangeCheck();

        var rangeBoundsLabel1Rect = new Rect(position);
        rangeBoundsLabel1Rect.width = rangeBoundsLabelWidth;
		minProp.intValue = (int)EditorGUI.FloatField(rangeBoundsLabel1Rect, minValue);
        position.xMin += rangeBoundsLabelWidth;

        var rangeBoundsLabel2Rect = new Rect(position);
        rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
		maxProp.intValue = (int)EditorGUI.FloatField(rangeBoundsLabel2Rect, maxValue);
        position.xMax -= rangeBoundsLabelWidth;

        EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
        if (EditorGUI.EndChangeCheck())
        {
			minProp.intValue = (int)minValue;
			maxProp.intValue = (int)maxValue;
        }

        EditorGUI.EndProperty();
    }
}
