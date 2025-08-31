using System;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RandomTrailColor : MonoBehaviour
{
	// Token: 0x06000528 RID: 1320 RVA: 0x0001BC7B File Offset: 0x00019E7B
	private void Awake()
	{
		this.trail = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0001BC89 File Offset: 0x00019E89
	public void RandomizeColor()
	{
		this.trail.startColor = this.colorRange.Evaluate(Random.value);
		this.trail.endColor = this.trail.startColor;
	}

	private TrailRenderer trail;

	public Gradient colorRange;
}
