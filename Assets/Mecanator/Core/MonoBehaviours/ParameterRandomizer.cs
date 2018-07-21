#if AlphaECS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using AlphaECS.Unity;

public class ParameterRandomizer : ComponentBehaviour
{
	public float Delay = 0f;
	public float Interval = 1f;
	public string ParameterName;

	public RangedInt Range;
}
#endif