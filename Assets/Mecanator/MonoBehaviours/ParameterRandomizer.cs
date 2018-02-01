using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class ParameterRandomizer : MonoBehaviour
{
	public float Delay = 0f;
	public float Interval = 1f;
	public string ParameterName;

	public RangedInt Range;
}
