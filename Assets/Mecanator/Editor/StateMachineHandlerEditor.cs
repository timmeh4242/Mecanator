using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using UniRx;

[CustomEditor(typeof(StateMachineHandler), true)]
public class StateMachineHandlerEditor : Editor
{
	private StateMachineHandler handler;

	private bool showActions = true;

	private readonly IEnumerable<Type> allComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
						.SelectMany(s => s.GetTypes())
						.Where(p => typeof(StateMachineAction).IsAssignableFrom(p) && p.IsClass);

	void OnEnable()
	{
		hideFlags = HideFlags.HideAndDontSave;

		if (handler == null)
		{ handler = (StateMachineHandler)target; }
	}

	public override void OnInspectorGUI()
	{
		if (handler == null)
		{ handler = (StateMachineHandler)target; }

		base.OnInspectorGUI();

		if (handler == null) { return; }

		DrawAddActions();
		DrawActions();
		PersistChanges();
	}

	private void DrawAddActions()
	{
		this.UseVerticalBoxLayout(() =>
		{
			var types = allComponentTypes.Select(x => string.Format("{0} [{1}]", x.Name, x.Namespace)).ToArray();
			var index = -1;
			index = EditorGUILayout.Popup("Add Action", index, types);

			if (index >= 0)
			{
                var action = (StateMachineAction)ScriptableObject.CreateInstance(allComponentTypes.ElementAt(index));
				handler.Actions.Add(action);
			}
		});
	}

	private void DrawActions()
	{
		EditorGUILayout.BeginVertical(EditorExtensions.DefaultBoxStyle);
		int numberOfActions = handler.Actions.Count;

		this.WithHorizontalLayout(() =>
		{
			this.WithLabel("Actions (" + numberOfActions + ")");
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
		});

		var actionsToRemove = new List<int>();
		if (showActions)
		{
			for (var i = 0; i < handler.Actions.Count; i++)
			{
				this.UseVerticalBoxLayout(() =>
				{
					var actionType = handler.Actions[i].GetType();
					//var actionType = handler.Actions[i].GetTypeWithAssembly();

					var typeName = actionType == null ? "" : actionType.Name;
					var typeNamespace = actionType == null ? "" : actionType.Namespace;

					this.WithVerticalLayout(() =>
					{
						this.WithHorizontalLayout(() =>
						{
							if (this.WithIconButton("-"))
							{
								actionsToRemove.Add(i);
							}

							this.WithLabel(typeName);
						});

						EditorGUILayout.LabelField(typeNamespace);
						EditorGUILayout.Space();
					});

					if (actionType == null)
					{
						//if (GUILayout.Button("TYPE NOT FOUND. TRY TO CONVERT TO BEST MATCH?"))
						//{
						//                   actionType = handler.Actions[i].TryGetConvertedType();
						//  if (componentType == null)
						//  {
						//      Debug.LogWarning("UNABLE TO CONVERT " + _view.CachedComponents[i]);
						//      return;
						//  }
						//  else
						//  {
						//      Debug.LogWarning("CONVERTED " + _view.CachedComponents[i] + " to " + componentType.ToString());
						//  }
						//}
						//else
						//{
						//  return;
						//}
					}

					DrawActionFields(handler.Actions[i]);
					DrawActionProperties(handler.Actions[i]);
				});
			}

			for (var i = 0; i < actionsToRemove.Count(); i++)
			{
				handler.Actions.RemoveAt(actionsToRemove[i]);
			}
		}

		EditorGUILayout.EndVertical();
	}

	private void DrawActionFields(StateMachineAction action)
	{
		foreach (var field in action.GetType().GetFields())
		{
			EditorGUILayout.BeginHorizontal();

			var _type = field.FieldType;
			var _value = field.GetValue(action);
            var isTypeSupported = TryDrawValue(_type, ref _value, field.Name);

			if (isTypeSupported == true)
			{
				field.SetValue(action, _value);
			}

			EditorGUILayout.EndHorizontal();
		}
	}

	private void DrawActionProperties(StateMachineAction action)
	{
		foreach (var property in action.GetType().GetProperties())
		{
			EditorGUILayout.BeginHorizontal();

			var _type = property.PropertyType;
			var _value = property.GetValue(action, null);
			var isTypeSupported = TryDrawValue(_type, ref _value, property.Name);

			if (isTypeSupported == true)
			{
				property.SetValue(action, _value, null);
			}

			EditorGUILayout.EndHorizontal();
		}
	}

	private bool TryDrawValue(Type _type, ref object _value, string _name)
	{
        if(_name == "hideFlags" || _name == "name")
        {
            return false;
        }

		if (_type == typeof(int))
		{
			_value = EditorGUILayout.IntField(_name, (int)_value);
		}
		else if (_type == typeof(IntReactiveProperty))
		{
			var reactiveProperty = _value as IntReactiveProperty;
			reactiveProperty.Value = EditorGUILayout.IntField(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(float))
		{
			_value = EditorGUILayout.FloatField(_name, (float)_value);
		}
		else if (_type == typeof(FloatReactiveProperty))
		{
			var reactiveProperty = _value as FloatReactiveProperty;
			reactiveProperty.Value = EditorGUILayout.FloatField(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(bool))
		{
			_value = EditorGUILayout.Toggle(_name, (bool)_value);
		}
		else if (_type == typeof(BoolReactiveProperty))
		{
			var reactiveProperty = _value as BoolReactiveProperty;
			reactiveProperty.Value = EditorGUILayout.Toggle(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(string))
		{
			_value = EditorGUILayout.TextField(_name, (string)_value);
		}
		else if (_type == typeof(StringReactiveProperty))
		{
			var reactiveProperty = _value as StringReactiveProperty;
			reactiveProperty.Value = EditorGUILayout.TextField(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(Vector2))
		{
			_value = EditorGUILayout.Vector2Field(_name, (Vector2)_value);
		}
		else if (_type == typeof(Vector2ReactiveProperty))
		{
			var reactiveProperty = _value as Vector2ReactiveProperty;
			_value = EditorGUILayout.Vector2Field(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(Vector3))
		{
			_value = EditorGUILayout.Vector3Field(_name, (Vector3)_value);
		}
		else if (_type == typeof(Vector3ReactiveProperty))
		{
			var reactiveProperty = _value as Vector3ReactiveProperty;
			_value = EditorGUILayout.Vector2Field(_name, reactiveProperty.Value);
		}
		else if (_type == typeof(Color))
		{
			_value = EditorGUILayout.ColorField(_name, (Color)_value);
		}
		else if (_type == typeof(ColorReactiveProperty))
		{
			var reactiveProperty = _value as ColorReactiveProperty;
			reactiveProperty.Value = EditorGUILayout.ColorField(_name, reactiveProperty.Value);
		}
		//else if (_type == typeof(Bounds))
		//{
		//  _value = EditorGUILayout.BoundsField(name, (Bounds)_value);
		//}
		//else if (_type == typeof(BoundsReactiveProperty))
		//{
		//  var reactiveProperty = _value as BoundsReactiveProperty;
		//  reactiveProperty.Value = EditorGUILayout.BoundsField(name, reactiveProperty.Value);
		//}
		//else if (_type == typeof(Rect))
		//{
		//  _value = EditorGUILayout.RectField(name, (Rect)_value);
		//}
		//else if (_type == typeof(RectReactiveProperty))
		//{
		//  var reactiveProperty = _value as RectReactiveProperty;
		//  reactiveProperty.Value = EditorGUILayout.RectField(name, reactiveProperty.Value);
		//}
		else if (_type == typeof(Enum))
		{
			_value = EditorGUILayout.EnumPopup(_name, (Enum)_value);
		}
		// else if (_type == typeof(Object))
		// {
		//  _value = EditorGUILayout.ObjectField(name, (Object)property.GetValue(), Object);
		// }
		else
		{
			Debug.LogWarning("This type is not supported: " + _type.Name + " - In action: " + _name);
			//Debug.LogWarning("This type is not supported!");
			return false;
		}

		return true;
	}

	private void PersistChanges()
	{
		if (GUI.changed && !Application.isPlaying)
		{ this.SaveActiveSceneChanges(); }
	}
}
