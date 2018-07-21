using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy
{
	public Animator Animator;
	public AnimationEvent AnimationEvent;

	public AnimationEventProxy(Animator animator, AnimationEvent animationEvent)
	{
		this.Animator = animator;
		this.AnimationEvent = animationEvent;
	}
}
