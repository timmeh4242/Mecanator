using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SetLayerWeights : StateMachineAction
{
	public List<float> LayerWeights = new List<float> ();

    public override void Execute (StateMachineActionObject smao)
	{
		for (var i = 0; i < LayerWeights.Count; i++)
		{
			var index = i;
			if (LayerWeights [index] < 0)
			{ continue; }
			smao.Animator.SetLayerWeight (index, LayerWeights[index]);
		}
	}
}
