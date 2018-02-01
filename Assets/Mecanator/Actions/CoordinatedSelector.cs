using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using UniRx.Triggers;
using System;

public class CoordinatedSelector : StateMachineAction
{
    public List<string> Parameters = new List<string>();
	private Dictionary<int, int> ParameterTable = new Dictionary<int, int> ();

	private List<int> PreviousParameters = new List<int>();
	private IDisposable WaitForExit; 

    private void OnEnable()
    {
		for (var i = 0; i < Parameters.Count; i++)
		{
            var hash = Animator.StringToHash(Parameters[i]);
			ParameterTable.Add(hash, 0);
		}
		PreviousParameters.Clear();
    }

    public override void Execute(StateMachineActionObject smao)
    {
		base.Execute(smao);

		if (Parameters.Count <= 0)
        { return;  }

		var keys = ParameterTable.Keys.ToList ();
        foreach (var key in keys)
		{
            ParameterTable[key] = smao.Animator.GetInteger(key);
		}

		var scoreGrouping = ParameterTable.GroupBy(kvp => kvp.Value).OrderByDescending(grouping => grouping.Key);
		var highScores = scoreGrouping.First();
        var parameterValue = highScores.Key;

		var selectedParameter = 0;
		foreach (var kvp in highScores)
		{
            smao.Animator.SetInteger(kvp.Key, 0);

			if (PreviousParameters.Contains(kvp.Key))
			{
                continue;
			}
            else
            {
                selectedParameter = kvp.Key;
                break;
            }
		}

        if(selectedParameter == 0)
        {
            PreviousParameters.Clear();
            selectedParameter = highScores.First().Key;
        }
			
		PreviousParameters.Add(selectedParameter);

		if (WaitForExit != null)
		{ WaitForExit.Dispose (); }

		WaitForExit = Observable.EveryUpdate ().Where(_ => smao.Animator.State(smao.LayerIndex) == smao.PathHash).FirstOrDefault()
			.SelectMany(Observable.EveryUpdate ().Where(_ => smao.Animator.State(smao.LayerIndex) != smao.PathHash)).FirstOrDefault()
		.Subscribe (_ =>
		{
			foreach (var kvp in highScores)
			{
				//only reset if the value is still zero
				var currentParameterValue = smao.Animator.GetInteger(kvp.Key);
				if(currentParameterValue == 0 || currentParameterValue == parameterValue + 1)
				{
					smao.Animator.SetInteger(kvp.Key, parameterValue);
				}
			}
		}).AddTo (smao.Animator);
		smao.Animator.SetInteger(selectedParameter, parameterValue + 1);
	}
}
