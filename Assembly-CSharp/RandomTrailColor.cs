using System;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RandomTrailColor : MonoBehaviour
{
	// Token: 0x06000674 RID: 1652 RVA: 0x00006A74 File Offset: 0x00004C74
	private void Awake()
	{
		this.trail = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00006A82 File Offset: 0x00004C82
	public void RandomizeColor()
	{
		this.trail.startColor = this.colorRange.Evaluate(global::UnityEngine.Random.value);
		this.trail.endColor = this.trail.startColor;
	}

	private TrailRenderer trail;

	public Gradient colorRange;
}
