using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class SetLayerWeights : StateMachineAction
{
    public List<float> LayerWeights = new List<float>();
    [SerializeField]
    private float blendTime = 0f;

    public override void Execute(StateMachineActionObject smao)
    {
        for (var i = 0; i < LayerWeights.Count; i++)
        {
            var index = i;
            if (LayerWeights[index] < 0)
            { continue; }

            //HACK tweening those to remove jitters
            //smao.Animator.SetLayerWeight(index, LayerWeights[index]);
            var target = LayerWeights [index];
            DOTween.To
            (
                () => smao.Animator.GetLayerWeight (index),
                x => smao.Animator.SetLayerWeight (index, x),
                target,
                blendTime
            ).OnComplete (() => smao.Animator.SetLayerWeight (index, target));
		}
	}
}