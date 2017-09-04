# Mecantor

<!-- [![Gitter](https://badges.gitter.im/AlphaECS/Lobby.svg)](https://gitter.im/AlphaECS/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=body_badge) -->

Mecanator is a simple set of tools that add visual scripting power to Mecanim. It uses [UniRx](https://github.com/neuecc/UniRx) for fully reactive state machines. Mecanator is currently in an early / experimental stage, so expect things to improve and also change and break from time to time.

- <a href="#introduction">Introduction</a>
- <a href="#reactive_state_machines">Reactive State Machines</a>
- <a href="#visual_scripting">Visual Scripting</a>


- <a href="#quick_start">Quick Start</a>
- <a href="#example_project">Example Project</a>
- <a href="#dependencies">Dependencies</a>
- <a href="#final_thoughts">Final Thoughts</a>


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
...

## <a id="example_project"></a>Example Project
...
 <!-- - [Survival Shooter](https://github.com/tbriley/AlphaECS.SurvivalShooter) -->


## <a id="dependencies"></a>Dependencies

 - UniRx (required)
 <!-- - Zenject (optional) -->


## <a id="final_thoughts"></a>Final Thoughts

This was not designed with performance in mind. However, it should be performant enough for most scenarios, and given its reactive nature and decoupled design you can easily replace implementations at will. Lots of people love performance metrics, but I have none and have put performance secondary to functionality.
