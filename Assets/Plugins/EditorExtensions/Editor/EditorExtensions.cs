using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UniRx
using UniRx;
#endif

public static class EditorExtensions
{
    public static readonly RectOffset DefaultPadding = new RectOffset(5, 5, 5, 5);
    public static readonly GUIStyle DefaultBoxStyle = new GUIStyle(GUI.skin.box) { padding = DefaultPadding };

    public static float lineHeight = EditorGUIUtility.singleLineHeight;
    public static float lineSpacing = EditorGUIUtility.standardVerticalSpacing;
    public static float elementPadding = EditorGUIUtility.standardVerticalSpacing;

    public static void UseVerticalBoxLayout(this Editor editor, Action action)
    {
        EditorGUILayout.BeginVertical(DefaultBoxStyle);
        action();
        EditorGUILayout.EndVertical();
    }

    public static void WithVerticalLayout(this Editor editor, Action action)
    {
        EditorGUILayout.BeginVertical();
        action();
        EditorGUILayout.EndVertical();
    }

    public static void UseHorizontalBoxLayout(this Editor editor, Action action)
    {
        EditorGUILayout.BeginHorizontal(DefaultBoxStyle);
        action();
        EditorGUILayout.EndHorizontal();
    }

    public static void WithHorizontalLayout(this Editor editor, Action action)
    {
        EditorGUILayout.BeginHorizontal();
        action();
        EditorGUILayout.EndHorizontal();
    }

    public static void WithLabel(this Editor editor, string label)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.MinWidth(0));
    }

    public static bool WithIconButton(this Editor editor, string icon)
    {
        return GUILayout.Button(icon, GUILayout.Width(20), GUILayout.Height(15));
    }

    public static void WithLabelField(this Editor editor, string label, string value)
    {
        EditorGUILayout.BeginHorizontal();
        editor.WithLabel(label);
        //EditorGUILayout.(value);
        editor.WithLabel(value);
        EditorGUILayout.EndHorizontal();
    }

    public static string WithTextField(this Editor editor, string label, string value)
    {
        EditorGUILayout.BeginHorizontal();
        editor.WithLabel(label);
        var result = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();
        return result;
    }

    public static bool TryDrawValue(this Editor editor, Rect rect, Type type, ref object value, string memberName, int index)
    {
        if (memberName == "hideFlags" || memberName == "name" || type.Name == "CompositeDisposable" || type.Name == "IDisposable")
        {
            return false;
        }

        if (type == typeof(int))
        {
            value = DrawInt(rect, value, memberName, index);
        }
        else if (type == typeof(float))
        {
            value = DrawFloat(rect, value, memberName, index);
        }
        else if (type == typeof(bool))
        {
            value = DrawBool(rect, value, memberName, index);
        }
        else if (type == typeof(string))
        {
            value = DrawString(rect, value, memberName, index);
        }
        else if (type == typeof(Vector2))
        {
            value = DrawVector2(rect, value, memberName, index);
        }
        else if (type == typeof(Vector3))
        {
            value = DrawVector3(rect, value, memberName, index);
        }
        else if (type == typeof(Color))
        {
            value = DrawColor(rect, value, memberName, index);
        }
        else if (type == typeof(Bounds))
        {
            value = DrawBounds(rect, value, memberName, index);
        }
        else if (type == typeof(Rect))
        {
            value = DrawRect(rect, value, memberName, index);
        }
        else if (type == typeof(Enum))
        {
            value = EditorGUI.EnumPopup(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Enum)value);
        }
        else if (type == typeof(UnityEngine.GameObject))
        {
            value = EditorGUI.ObjectField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (UnityEngine.GameObject)value, type, true);
        }
#if UniRx
        else if (type == typeof(IntReactiveProperty))
        {
            var reactiveProperty = value as IntReactiveProperty;
            reactiveProperty.Value = DrawInt(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(FloatReactiveProperty))
        {
            var reactiveProperty = value as FloatReactiveProperty;
            reactiveProperty.Value = DrawFloat(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(BoolReactiveProperty))
        {
            var reactiveProperty = value as BoolReactiveProperty;
            reactiveProperty.Value = DrawBool(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(StringReactiveProperty))
        {
            var reactiveProperty = value as StringReactiveProperty;
            reactiveProperty.Value = DrawString(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(Vector2ReactiveProperty))
        {
            var reactiveProperty = value as Vector2ReactiveProperty;
            value = DrawVector2(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(Vector3ReactiveProperty))
        {
            var reactiveProperty = value as Vector3ReactiveProperty;
            value = DrawVector3(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(ColorReactiveProperty))
        {
            var reactiveProperty = value as ColorReactiveProperty;
            reactiveProperty.Value = DrawColor(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(BoundsReactiveProperty))
        {
            var reactiveProperty = value as BoundsReactiveProperty;
            reactiveProperty.Value = DrawBounds(rect, reactiveProperty.Value, memberName, index);
        }
        else if (type == typeof(RectReactiveProperty))
        {
            var reactiveProperty = value as RectReactiveProperty;
            reactiveProperty.Value = DrawRect(rect, reactiveProperty.Value, memberName, index);
        }
#endif
        else
		{
			Debug.LogWarning("This type is not supported: " + type.Name + " - In member: " + memberName);
			//Debug.LogWarning("This type is not supported!");
			return false;
		}

		return true;
	}

    private static int DrawInt(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.IntField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (int)value);
    }

    private static float DrawFloat(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.FloatField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (float)value);
    }

    private static bool DrawBool(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.Toggle(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (bool)value);
    }

    private static string DrawString(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.TextField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (string)value);
    }

    private static Vector2 DrawVector2(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.Vector2Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Vector2)value);
    }

    private static Vector3 DrawVector3(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.Vector3Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Vector3)value);
    }

    private static Color DrawColor(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.ColorField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Color)value);
    }

    private static Bounds DrawBounds(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.BoundsField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Bounds)value);
    }

    private static Rect DrawRect(Rect rect, object value, string memberName, int index)
    {
        return EditorGUI.RectField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Rect)value);
    }

    public static void SetHideFlags(this Editor editor, PrefabType prefabType)
	{
		if (prefabType == PrefabType.None)
		{
			editor.hideFlags = HideFlags.HideAndDontSave;
		}
		else
		{
			editor.hideFlags = HideFlags.None;
		}
	}

    // Only works with unity 5.3+
    public static void SaveActiveSceneChanges(this Editor editor)
    {
        var activeScene = SceneManager.GetActiveScene();
        EditorSceneManager.MarkSceneDirty(activeScene);
    }
}