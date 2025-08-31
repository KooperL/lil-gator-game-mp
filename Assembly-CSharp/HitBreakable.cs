using System;
using UnityEngine;

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

	public BreakableObject breakableObject;
}
