using System;
using UnityEngine;

public class HitBreakable : MonoBehaviour, IHit
{
	// Token: 0x060007CB RID: 1995 RVA: 0x00007BB1 File Offset: 0x00005DB1
	private void OnValidate()
	{
		if (this.breakableObject == null)
		{
			this.breakableObject = base.GetComponentInParent<BreakableObject>();
		}
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x00007BCD File Offset: 0x00005DCD
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.breakableObject.Break(false, velocity, isHeavy);
	}

	public BreakableObject breakableObject;
}
