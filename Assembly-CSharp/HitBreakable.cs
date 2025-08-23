using System;
using UnityEngine;

public class HitBreakable : MonoBehaviour, IHit
{
	// Token: 0x060007CC RID: 1996 RVA: 0x00007BC6 File Offset: 0x00005DC6
	private void OnValidate()
	{
		if (this.breakableObject == null)
		{
			this.breakableObject = base.GetComponentInParent<BreakableObject>();
		}
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x00007BE2 File Offset: 0x00005DE2
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.breakableObject.Break(false, velocity, isHeavy);
	}

	public BreakableObject breakableObject;
}
