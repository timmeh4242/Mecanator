using System;
using UniRx;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		EditorGUILayout.LabelField (label, EditorStyles.boldLabel, GUILayout.MinWidth (0));
    }

    public static bool WithIconButton(this Editor editor, string icon)
    {
        return GUILayout.Button(icon, GUILayout.Width(20), GUILayout.Height(15));
    }

    public static void WithLabelField(this Editor editor, string label, string value)
    {
        EditorGUILayout.BeginHorizontal();
        editor.WithLabel(label);
//            EditorGUILayout.(value);
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
			value = EditorGUI.IntField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (int)value);
		}
		else if (type == typeof(IntReactiveProperty))
		{
			var reactiveProperty = value as IntReactiveProperty;
			reactiveProperty.Value = EditorGUI.IntField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(float))
		{
			value = EditorGUI.FloatField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (float)value);
		}
		else if (type == typeof(FloatReactiveProperty))
		{
			var reactiveProperty = value as FloatReactiveProperty;
			reactiveProperty.Value = EditorGUI.FloatField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(bool))
		{
			value = EditorGUI.Toggle(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (bool)value);
		}
		else if (type == typeof(BoolReactiveProperty))
		{
			var reactiveProperty = value as BoolReactiveProperty;
			reactiveProperty.Value = EditorGUI.Toggle(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(string))
		{
			value = EditorGUI.TextField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (string)value);
		}
		else if (type == typeof(StringReactiveProperty))
		{
			var reactiveProperty = value as StringReactiveProperty;
			reactiveProperty.Value = EditorGUI.TextField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(Vector2))
		{
			value = EditorGUI.Vector2Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Vector2)value);
		}
		else if (type == typeof(Vector2ReactiveProperty))
		{
			var reactiveProperty = value as Vector2ReactiveProperty;
			value = EditorGUI.Vector2Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(Vector3))
		{
			value = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Vector3)value);
		}
		else if (type == typeof(Vector3ReactiveProperty))
		{
			var reactiveProperty = value as Vector3ReactiveProperty;
			value = EditorGUI.Vector3Field(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(Color))
		{
			value = EditorGUI.ColorField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Color)value);
		}
		else if (type == typeof(ColorReactiveProperty))
		{
			var reactiveProperty = value as ColorReactiveProperty;
			reactiveProperty.Value = EditorGUI.ColorField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, reactiveProperty.Value);
		}
		else if (type == typeof(Bounds))
		{
			value = EditorGUI.BoundsField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Bounds)value);
		}
		else if (type == typeof(BoundsReactiveProperty))
		{
			var reactiveProperty = value as BoundsReactiveProperty;
			reactiveProperty.Value = EditorGUI.BoundsField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Bounds)reactiveProperty.Value);
		}
		else if (type == typeof(Rect))
		{
			value = EditorGUI.RectField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Rect)value);
		}
		else if (type == typeof(RectReactiveProperty))
		{
			var reactiveProperty = value as RectReactiveProperty;
			reactiveProperty.Value = EditorGUI.RectField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Rect)reactiveProperty.Value);
		}
		else if (type == typeof(Enum))
		{
			value = EditorGUI.EnumPopup(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (Enum)value);
		}
		else if (type == typeof(UnityEngine.GameObject))
		{
			value = EditorGUI.ObjectField(new Rect(rect.x, rect.y + (index * lineHeight) + (index * lineSpacing), rect.width, lineHeight), memberName, (UnityEngine.GameObject)value, type, true);
		}
		else
		{
			Debug.LogWarning("This type is not supported: " + type.Name + " - In member: " + memberName);
			//Debug.LogWarning("This type is not supported!");
			return false;
		}

		return true;
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