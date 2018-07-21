using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameter
{
	public AnimatorControllerParameterType type;
	public int parameterHash;
	public object data;

	public AnimatorParameter(Animator animator, int parameterHash, AnimatorControllerParameterType type)
	{
		this.type = type;
		this.parameterHash = parameterHash;

		switch (type)
		{
		case AnimatorControllerParameterType.Int:
			this.data = (int)animator.GetInteger(parameterHash);
			break;
		case AnimatorControllerParameterType.Float:
			this.data = (float)animator.GetFloat(parameterHash);
			break;
		case AnimatorControllerParameterType.Bool:
			this.data = (bool)animator.GetBool(parameterHash);
			break;
		}
	}

	public AnimatorParameter(Animator animator, string parameterName, AnimatorControllerParameterType type)
	{
		this.type = type;
		this.parameterHash = Animator.StringToHash(parameterName);

		switch (type)
		{
		case AnimatorControllerParameterType.Int:
			this.data = (int)animator.GetInteger(parameterHash);
			break;
		case AnimatorControllerParameterType.Float:
			this.data = (float)animator.GetFloat(parameterHash);
			break;
		case AnimatorControllerParameterType.Bool:
			this.data = (bool)animator.GetBool(parameterHash);
			break;
		}
	}
}
