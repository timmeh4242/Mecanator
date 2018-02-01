using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Repeat : StateMachineAction
{
    public float MinInterval;
	public float MaxInterval;

	public float Interval;
	public float Delay;

	public bool IsRandom;

	private IDisposable Repeater;

	public override void Execute (StateMachineActionObject smao)
    {
        TryRepeat(smao);
//		smao.Animator.OnStateExit(smao.StateInfo.fullPathHash).FirstOrDefault().ObserveOnMainThread().Subscribe(_ => 
//		{
//			if(Repeater != null)
//			{ Repeater.Dispose(); }
//		}).AddTo(smao.Animator);
	}

    private void TryRepeat(StateMachineActionObject smao)
    {
		Delay = IsRandom ? UnityEngine.Random.Range (MinInterval, MaxInterval) : Delay;
		var delay = TimeSpan.FromSeconds(smao.StateInfo.length + Delay);

		if(Repeater != null)
		{ Repeater.Dispose(); }

        Repeater = Observable.Timer(delay).Subscribe(_ =>
		{
			if(smao.Animator.State(smao.LayerIndex) == smao.StateInfo.fullPathHash)
			{
				smao.Animator.Play(smao.StateInfo.fullPathHash, smao.LayerIndex, 0f);
				TryRepeat(smao);
			}
			else
			{
				if(Repeater != null)
				{ Repeater.Dispose(); }
			}
		}).AddTo(smao.Animator);
    }
}
