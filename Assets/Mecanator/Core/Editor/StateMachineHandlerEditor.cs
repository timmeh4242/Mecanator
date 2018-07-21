using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using UniRx;
using UnityEditorInternal;

[CustomEditor(typeof(StateMachineHandler), true)]
public class StateMachineHandlerEditor : Editor
{
    private StateMachineHandler handler;

    private ReorderableList reorderableActions;

    private bool showActions = true;

    private readonly IEnumerable<Type> allComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(StateMachineAction).IsAssignableFrom(p) && p.IsClass);

    int lineHeight = 15;
    int lineSpacing = 18;
   
    private class ActionInfo
    {
        public Type type;
    }

	void OnEnable()
	{
		//hideFlags = HideFlags.HideAndDontSave;

		if (handler == null)
		{ handler = (StateMachineHandler)target; }

		if (handler == null)
		{ return; }

        reorderableActions = new ReorderableList(serializedObject, serializedObject.FindProperty("Actions"), true, true, true, true);

        reorderableActions.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Actions", EditorStyles.boldLabel);
        };

        reorderableActions.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
			var element = reorderableActions.serializedProperty.GetArrayElementAtIndex(index);
            var so = new SerializedObject(element.objectReferenceValue);
            so.Update();

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, lineHeight), handler.Actions[index].GetType().ToString(), EditorStyles.boldLabel);

			var iterator = so.GetIterator();
            iterator.NextVisible(true); // skip the script reference

			var i = 1;
            var showChildren = true;
            while (iterator.NextVisible(showChildren))
			{
				EditorGUI.PropertyField(new Rect(rect.x, rect.y + (lineSpacing * i), rect.width, lineHeight), iterator);
				i++;
//                if(iterator.isArray)
//                {
//                    showChildren = iterator.isExpanded;
//                }
			}

            so.ApplyModifiedProperties();
        };

        reorderableActions.elementHeightCallback = (int index) =>
        {
            float height = 0;

			var element = reorderableActions.serializedProperty.GetArrayElementAtIndex(index);
			var elementObj = new SerializedObject(element.objectReferenceValue);

			var iterator = elementObj.GetIterator();
			var i = 1;
			var showChildren = true;
            while (iterator.NextVisible(showChildren))
			{
				i++;
//				if (iterator.isArray)
//				{
//					showChildren = iterator.isExpanded;
//				}
			}

            height = lineSpacing * i;
            return height;
        };

        reorderableActions.onAddDropdownCallback = (Rect rect, ReorderableList list) =>
		{
            var dropdownMenu = new GenericMenu();
			var types = allComponentTypes.Select(x => x.Name).ToArray();

            for (var i = 0; i < types.Length; i++)
            {
                dropdownMenu.AddItem(new GUIContent(types[i]), false, AddAction, new ActionInfo() { type = allComponentTypes.ElementAt(i) });
            }

            dropdownMenu.ShowAsContext();
		};

        reorderableActions.onRemoveCallback = (list) => 
        {
            var action = handler.Actions[list.index];
            DestroyImmediate(action, true);
            handler.Actions.RemoveAt(list.index);
        };
	}

	public override void OnInspectorGUI()
	{
		if (handler == null)
		{ handler = (StateMachineHandler)target; }

		base.OnInspectorGUI();

		if (handler == null) { return; }

		//EditorGUI.LabelField(rect, "Actions", EditorStyles.boldLabel);

		if (showActions)
		{
            if (this.WithIconButton("▾"))
            {
              showActions = false;
            }
		}
		else
		{
            if (this.WithIconButton("▸"))
            {
                showActions = true;
            }
        }

		if (showActions)
		{
			serializedObject.Update();
			Undo.RecordObject(handler, "Added Action");
			reorderableActions.DoLayoutList();
			PersistChanges();
			serializedObject.ApplyModifiedProperties();
		}
	}

    private void AddAction(object info)
    {
        var actionInfo = (ActionInfo)info;
        var action = (StateMachineAction)ScriptableObject.CreateInstance(actionInfo.type);
		action.name = action.GetType ().ToString();
		AssetDatabase.AddObjectToAsset(action, handler);
		handler.Actions.Add(action);
	}

	private void PersistChanges()
	{
		if (GUI.changed && !Application.isPlaying)
		{
            this.SaveActiveSceneChanges();
        }
	}
}
