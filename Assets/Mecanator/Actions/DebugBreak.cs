using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBreak : StateMachineAction
{
    public override void Execute(StateMachineActionObject smao)
    {
        Debug.Break();
    }
}
