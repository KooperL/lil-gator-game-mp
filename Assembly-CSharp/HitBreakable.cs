using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class HitBreakable : MonoBehaviour, IHit
{
	// Token: 0x06000666 RID: 1638 RVA: 0x0002111D File Offset: 0x0001F31D
	private void OnValidate()
	{
		if (this.breakableObject == null)
		{
			this.breakableObject = base.GetComponentInParent<BreakableObject>();
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x00021139 File Offset: 0x0001F339
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.breakableObject.Break(false, velocity, isHeavy);
	}

	// Token: 0x040008A0 RID: 2208
	public BreakableObject breakableObject;
}
