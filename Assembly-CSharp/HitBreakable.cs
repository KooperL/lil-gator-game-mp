using System;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class HitBreakable : MonoBehaviour, IHit
{
	// Token: 0x0600078B RID: 1931 RVA: 0x000078B7 File Offset: 0x00005AB7
	private void OnValidate()
	{
		if (this.breakableObject == null)
		{
			this.breakableObject = base.GetComponentInParent<BreakableObject>();
		}
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x000078D3 File Offset: 0x00005AD3
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.breakableObject.Break(false, velocity, isHeavy);
	}

	// Token: 0x04000A0C RID: 2572
	public BreakableObject breakableObject;
}
