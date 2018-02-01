using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UniRx;

[System.Serializable]
public class SetLayerWeights : StateMachineAction
{
	public List<float> LayerWeights = new List<float> ();

    public override void Execute (StateMachineActionObject smao)
	{
//		for (var i = 0; i < smao.Animator.layerCount; i++)
//		{
//			smao.Animator.SetLayerWeight (i, 0f);
//		}
//		smao.Animator.SetLayerWeight (smao.LayerIndex, 1f);


		//HACK tweening those to remove jitters
		for (var i = 0; i < LayerWeights.Count; i++)
		{
			var index = i;
			if (LayerWeights [index] < 0)
			{ continue; }
//			smao.Animator.SetLayerWeight (index, LayerWeights[index]);

			var target = LayerWeights [index];
			smao.Animator.SetLayerWeight (index, target);
		}
	}
}
