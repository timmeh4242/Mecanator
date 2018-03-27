using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMessage : StateMachineAction
{
	public string Message;

	public override void Execute (StateMachineActionObject smao)
	{
		Debug.Log (Message);
	}
}
