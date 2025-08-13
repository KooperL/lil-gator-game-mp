using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
[RequireComponent(typeof(TrailRenderer))]
public class RandomTrailColor : MonoBehaviour
{
	// Token: 0x0600063A RID: 1594 RVA: 0x000067AE File Offset: 0x000049AE
	private void Awake()
	{
		this.trail = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x000067BC File Offset: 0x000049BC
	public void RandomizeColor()
	{
		this.trail.startColor = this.colorRange.Evaluate(Random.value);
		this.trail.endColor = this.trail.startColor;
	}

	// Token: 0x04000865 RID: 2149
	private TrailRenderer trail;

	// Token: 0x04000866 RID: 2150
	public Gradient colorRange;
}
