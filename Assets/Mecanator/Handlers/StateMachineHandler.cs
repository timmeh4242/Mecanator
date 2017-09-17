using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineHandler : StateMachineBehaviour
{
    [HideInInspector]
    public List<StateMachineAction> Actions = new List<StateMachineAction>();
}
