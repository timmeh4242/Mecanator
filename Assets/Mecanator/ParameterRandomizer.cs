using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class ParameterRandomizer : MonoBehaviour, IDisposable, IDisposableContainer
{
	public float Delay = 0f;
	public float Interval = 1f;
	public string ParameterName;

	public RangedInt Range;

	private Animator Animator;
	private int ParamterHash;
	private IDisposable IntervalRoutine;

	private CompositeDisposable _disposer = new CompositeDisposable();
	public CompositeDisposable Disposer
	{
		get { return _disposer; }
		set { _disposer = value; }
	}

	void Awake()
	{
		ParamterHash = Animator.StringToHash (ParameterName);
	}

	void Start()
	{
		if (this.Animator == null)
		{ this.Animator = this.gameObject.GetComponent<Animator> (); }

		StartRoutine (this.Animator);
	}

	private void StartRoutine(Animator animator)
	{
		if (IntervalRoutine != null)
		{ IntervalRoutine.Dispose (); }

		var delay = TimeSpan.FromSeconds (Delay);
		var interval = TimeSpan.FromSeconds (Interval);
		IntervalRoutine = Observable.Timer (delay, interval).Subscribe (_ =>
		{
			var value = UnityEngine.Random.Range(Range.MinValue, Range.MaxValue);
			this.Animator.SetInteger (ParamterHash, value);
		}).AddTo(this.Disposer);
	}

	public virtual void OnDestroy()
	{
		Dispose ();
	}

	public virtual void Dispose()
	{
		Disposer.Dispose();
	}
}
