# Mecanator

<!-- [![Gitter](https://badges.gitter.im/AlphaECS/Lobby.svg)](https://gitter.im/AlphaECS/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=body_badge) -->

Mecanator is a simple set of tools that add visual scripting power to Mecanim. It uses [UniRx](https://github.com/neuecc/UniRx) for fully reactive state machines. Mecanator is currently in an early / experimental stage, so expect things to improve and also change and break from time to time.

- <a href="#introduction">Introduction</a>
- <a href="#reactive_state_machines">Reactive State Machines</a>
- <a href="#visual_scripting">Visual Scripting</a>


- <a href="#quick_start">Quick Start</a>
- <a href="#example_project">Example Project</a>
- <a href="#dependencies">Dependencies</a>


## <a id="introduction"></a>Introduction
Mecanim is a highly optimized and easy to use tool for prototyping and setting up simple animation logic for all sorts of different use cases. However as your project grows and as you require more complex logic, managing state between the visual scripting of Mecanim and actual code gets a bit painful. Adding simple things that really can and should live in a visual graph can often break your logic.

## <a id="reactive_state_machines"></a>Reactive State Machines
Often with Mecanim we see polling in code something like:

```
void Update()
{
  var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
  if(stateInfo.IsName("IdleState") && character.State == CharacterState.Idle)
  {
    // do some logic
  }
}
```

Here we are trying to keep our AI logic and animation logic synchronized. It works but when your entities start becoming more complex it becomes difficult to manage and bug prone.

UniRx provides us with glue to turn what mecanim gives us into `Reactive` state machines. And Mecanator gives us some simple extension methods for easy polling when that's the desired approach. For example, to achieve the above example of wanting to trigger some logic when our character has entered their idle state, we can:

```
animator.OnStateEnter("Base Layer.IdleState").Subscribe(_ =>
{
    // do some logic
});
```

This way we can start to experiement with packing some of our AI specific logic and state into the Mecanim graph itself.


## <a id="visual_scripting"></a>Visual Scripting
A hot topic these days. There are a number of good solutions with a number of different approaches to designing logic "without code". PlayMaker, Node Canvas, GameFlow, etc.

There's a great blog post here on [repurposing Unity's animator as a finite state machine](https://medium.com/the-unity-developers-handbook/dont-re-invent-finite-state-machines-how-to-repurpose-unity-s-animator-7c6c421e5785). Mecanator, at least at the start, won't try to do too many complex things. What it will aim for at the core is to start packing things that really don't need a lot of fancy logic into the animator itself. For example, one good pattern is to use nested states to hide complexity in the graph, especially in cases where you really need a de-coupled design. And also to use the default `Entry` and `Exit` nodes for going into and out of the substate. Maybe our human animator wants to set up a ShootingGun state machine with 3 nested substates, where each substate is a variation on a gunshot animation.

 ![image](https://gyazo.com/66c524c6cc6e78312e71fbe07ffc2b2a)
 ![image](https://gyazo.com/353b85ae731fb593902284af26ea7453)


We could make a bunch of different `StateMachineBehaviour` components for each type of action like this:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
//using System;

public class SubStateSelector : StateMachineBehaviour
{
	public int ChildStates = 0;
	public string ParameterName;

	public bool IsRandom = false;

	private int ParamterHash;
	private int State = 0;

	void Awake()
	{
		ParamterHash = Animator.StringToHash (ParameterName);
	}

	public override void OnStateMachineEnter (Animator animator, int stateMachinePathHash)
	{
		base.OnStateMachineEnter (animator, stateMachinePathHash);

		if (IsRandom == true)
		{
			State = Random.Range (0, ChildStates);
		}
		else
		{
			State += 1;
			if (State > ChildStates)
			{ State = 0; }
		}

		animator.SetInteger (ParamterHash, State);
	}
}
```

Add the SubStateSelector to any parent state machine, fill out the number of children and parameter name you want to set when the state is entered, and you get a nice, simple way for animators to set this up.

You'll notice that this script uses OnStateMachineEnter to trigger when the state machine node has been entered. We can imagine that there may be all sorts of things we might want to do when a state machine has been entered, exited, or some substate has been entered, exited, or updated. Any future scripts that use these same callbacks will lead to a `LOT` of duplicated code and a potentially really mess, difficult to understand inspector. With Mecanator we provide generic scripts for each one of the built-in Unity Mecanim callbacks (`OnStateMachineEnter`, `OnStateMachineExit`, `OnStateEnter`, `OnStateExit`, `OnStateUpdate`, etc). You then can add any number of included actions, or write your own simple actions, to tie into these events:

![image](https://gyazo.com/2675b9f16f5f00a08a8f1c11e7100508)

## <a id="example_project"></a>Example Projects
- [2D Roguelike](https://github.com/tbriley/Mecanator.2DRoguelike)


## <a id="dependencies"></a>Dependencies
- [UniRx](https://github.com/neuecc/UniRx)
- [Editor Extensions](https://github.com/tbriley/EditorExtensions)
- [Serializable Dictionary](https://github.com/azixMcAze/Unity-SerializableDictionary)
- [Ranged Values](https://github.com/tbriley/RangedValues)

## <a id="dependencies"></a>Optional
- [AlphaECS](https://github.com/tbriley/AlphaECS)
