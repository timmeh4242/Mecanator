using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[System.Serializable]
public class SetIntParameter : StateMachineAction
{
	public string ParameterName;

	public int Value;
    public RangedInt RangedValue;

	private int ParameterHash;

    private void OnEnable()
    {
       ParameterHash = Animator.StringToHash(ParameterName); 
    }

    public override void Execute (StateMachineActionObject smao)
	{
        var value = Random.Range(RangedValue.MinValue, RangedValue.MaxValue);
		smao.Animator.SetInteger (ParameterHash, value);
	}
}
