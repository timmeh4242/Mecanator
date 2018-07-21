#if AlphaECS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlphaECS;
using Zenject;
using UnityEngine.Serialization;

#if DOTWEEN
using DG.Tweening;
#endif

[System.Serializable]
public class SetLayerWeights : StateMachineAction
{
    public List<float> LayerWeights = new List<float>();
    [SerializeField]
    private float blendTime = 0f;

    public override void Execute(StateMachineActionObject smao)
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
            if (LayerWeights[index] < 0)
            { continue; }

#if DOTWEEN
            var target = LayerWeights [index];
            DOTween.To
            (
                () => smao.Animator.GetLayerWeight (index),
                x => smao.Animator.SetLayerWeight (index, x),
                target,
                blendTime
            ).OnComplete (() => smao.Animator.SetLayerWeight (index, target));
#else
            smao.Animator.SetLayerWeight(index, LayerWeights[index]);
#endif
		}
	}
}
#endif